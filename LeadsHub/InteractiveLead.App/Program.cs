using InteractiveLead.Data.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Radzen;
using System.Data;
using InteractiveLead.App.Adapters;
using InteractiveLead.App.Components;
using InteractiveLead.App.Components.Account;
using InteractiveLead.App.Data;
using InteractiveLead.App.Pages.Manager.Adapter;
using InteractiveLead.App.Providers;
using InteractiveLead.Core.Bac;
using InteractiveLead.Core.Broker;
using InteractiveLead.Core.Enums;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Interfaces.IRepository;
using InteractiveLead.Core.Interfaces.IServices;
using InteractiveLead.Core.NotifyServices;
using InteractiveLead.Core.Utility;
using InteractiveLead.Data.Repository.CentralHub;
using InteractiveLead.Core.Bac.CentralHub;
using InteractiveLead.App.Components.Pages.Manager.Adapters;
using InteractiveLead.Core.Interfaces.CentralHub;
using InteractiveLead.Core.Services;
using InteractiveLead.App.Components.Pages.Admin.Adapter;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

string connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

SD.ConnectString = connectionString;

// Add services to the container.
#region Database config
// Configuration for dapper and postgres.
builder.Services.AddTransient<IDbConnection>(config => 
  new NpgsqlConnection(connectionString));

// Configuration for Entity framework
builder.Services.AddDbContext<ApplicationDbContext>(options => 
  options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = IdentityConstants.ApplicationScheme;
  options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
  options.SignIn.RequireConfirmedAccount = false;
  options.SignIn.RequireConfirmedPhoneNumber = false;
  options.SignIn.RequireConfirmedEmail = false;
  options.SignIn.RequireConfirmedPhoneNumber = false;
  options.Lockout.AllowedForNewUsers = false;
}).AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddSignInManager()
  .AddDefaultTokenProviders();
#endregion

builder.Services.AddRazorComponents()
  .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.Services.AddHttpClient();

// DI to get utc time and converts to the client time
builder.Services.AddBrowserTimeProvider();

//builder.Services.AddSingleton<PostgresListenerService>(sp => 
    //new PostgresListenerService(connectionString));

// TODO: Usefull later 
//builder.Services.AddHostedService<PostgresListenerService>();
builder.Services.AddHostedService<MessageConsumer>();
builder.Services.AddHostedService<QuestionAnswerConsumer>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Adapter -- frontend
builder.Services.AddScoped<ChatMessageAdapter>();
builder.Services.AddScoped<ConsultantAdminAdapter>();
builder.Services.AddScoped<LeadListAdapter>();
builder.Services.AddScoped<LeadsManagerHomeAdapter>();
builder.Services.AddScoped<ManagerAdapter>();
builder.Services.AddScoped<QuestionAdapter>();

// Services
builder.Services.AddSingleton<MessageNotifyService>();
builder.Services.AddSingleton<IBaseService, BaseService>();
builder.Services.AddSingleton<IWhatsappService, WhatsappService>();

// BAC
builder.Services.AddSingleton<IChatMessageBac, ChatMessageBac>();
builder.Services.AddSingleton<IConsultantBac, ConsultantBac>();
builder.Services.AddSingleton<IDistribuitionBac, DistribuitionBac>();
builder.Services.AddSingleton<ILeadBac, LeadBac>();
builder.Services.AddSingleton<ILeadHubCentralBac, LeadHubCentralBac>();
builder.Services.AddScoped<IQuestionBac, QuestionBac>();
builder.Services.AddSingleton<IQuestionCentralHubBac, QuestionHubCentral>();

// Repositories
builder.Services.AddSingleton<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddSingleton<IConsultantRepository, ConsultantRepository>();
builder.Services.AddSingleton<ILeadCentralHubRepository, LeadCentralRepository>();
builder.Services.AddSingleton<ILeadRepository, LeadRepository>();
builder.Services.AddSingleton<IQuestionCentralHubRepository, QuestionCentralRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

ApplyMigration();
ApplyUserAndRoles();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();


// Apply migration pending migrations
void ApplyMigration()
{
    using IServiceScope scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (_db.Database.GetPendingMigrations().Any())
    {
      _db.Database.Migrate();
    }
}

async void ApplyUserAndRoles()
{
    // Scope for creating default roles
    using (IServiceScope scope = app.Services.CreateScope())
    {
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = RolesEnum.List.Select(r => r.Name).ToArray();

        foreach (string role in roles)
        {
          if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Scope for creating default roles
    using (IServiceScope scope = app.Services.CreateScope())
    {
        UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string user = "sysadmin@admin.com";
        string email = "sysadmin@admin.com";
        string password = "SysAdmin#1234";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            ApplicationUser appUser = new()
            {
              UserName = user,
              Email = email,
            };

            IdentityResult result = await userManager.CreateAsync(appUser, password);

            if (result.Succeeded)
              await userManager.AddToRoleAsync(appUser, RolesEnum.SysAdmin.Name);
        }
    }
}
