
namespace MercadoLivre.Core.Models.MLProduct
{
    public class MLProductResponse
    {
        public long Code { get; set; }

        public MLProduct Body { get; set; } = new();
    }
}
