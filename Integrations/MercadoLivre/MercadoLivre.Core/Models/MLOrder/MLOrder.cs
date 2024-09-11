

namespace MercadoLivre.Core.Models.MLOrder
{
    public sealed class MLOrder
    {
        public long Id { get; set; }

        public MLBuyer Buyer { get; set; } = new();
    }
}
