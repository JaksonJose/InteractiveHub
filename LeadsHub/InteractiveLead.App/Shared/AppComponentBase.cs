using AdaptiveKitCore.Enums;
using AdaptiveKitCore.Requests;
using InteractiveLead.App.Data;
using InteractiveLead.Core.Enums;
using InteractiveLead.Core.Interfaces.IBac;
using InteractiveLead.Core.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using InteractiveLead.App.Providers;

namespace InteractiveLead.App.Services
{
    public class AppComponentBase : ComponentBase, IDisposable
    {
        [Inject]
        [CascadingParameter]
        private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

        [Inject]
        public TimeProvider TimeProvider { get; set; } = default!;

        [Inject]
        public UserManager<ApplicationUser>? UserManager { get; set; }

        [Inject]
        private IConsultantBac ConsultantBac { get; set; } = default!;

        protected ClaimsPrincipal User { get; private set; } = default!;

        protected bool IsSysAdmin => User?.IsInRole(RolesEnum.SysAdmin.Name) ?? false;

        protected bool IsManager => User?.IsInRole(RolesEnum.Manager.Name) ?? false;

        protected bool IsConsultant => User?.IsInRole(RolesEnum.NormalUser.Name) ?? false;

        protected Consultant CurrentConsultant { get; private set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (TimeProvider is BrowserTimeProvider browserTimeProvider)
            {
                browserTimeProvider.LocalTimeZoneChanged += LocalTimeZoneChanged;
            }

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            User = authState.User;

            await LoadCurrentConsultantAsync();
        }

        private async Task LoadCurrentConsultantAsync()
        {
            const string consultantIdKey = "ConsultantId";

            if (User.HasClaim(c => c.Type == consultantIdKey))
            {
                string? consultantId = User.FindFirst(c => c.Type == consultantIdKey)?.Value;

                if (consultantId != null) 
                    CurrentConsultant = new Consultant { AspNetUserId = consultantId };

                return;
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                CurrentConsultant = new Consultant();
                return;
            }

            FilterRequest filterRequest = new();
            filterRequest.AddFilter(nameof(Consultant.AspNetUserId), FilterOperatorEnum.EqualTo, userId);

            var response = await ConsultantBac.FetchConsultantsByRequestAsync(filterRequest);
            if (response.ResponseData.Count == 0)
            {
                CurrentConsultant = new Consultant();
                return;
            }

            CurrentConsultant = response.ResponseData[0];
        }

        /// <summary>
        /// Covert utc time to client time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime LocalDateTime(DateTime dateTime) => TimeProvider.ToLocalDateTime(dateTime);

        public void LocalTimeZoneChanged(object? sender, EventArgs e)
        {
            _ = InvokeAsync(StateHasChanged);
        }

        public virtual void Dispose()
        {
            if (TimeProvider is BrowserTimeProvider browserTimeProvider)
            {
                browserTimeProvider.LocalTimeZoneChanged -= LocalTimeZoneChanged;
            }
        }
    }
}
