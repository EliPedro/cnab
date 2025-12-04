namespace WebSite.Features.Store
{
    public class StoreResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public IEnumerable<TransactionsStoreResponse> Transactions { get; set; } = [];
    }

    public class TransactionsStoreResponse
    {
        public int Id { get; set; }
        public required string TypeDescription { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Card { get; set; } = string.Empty;
    }
}
