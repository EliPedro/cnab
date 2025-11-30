namespace WebSite.Entities;
public class Transaction
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public Store? Store { get; set; } = null!;
    public int TypeCode { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Card { get; set; } = string.Empty;
}
