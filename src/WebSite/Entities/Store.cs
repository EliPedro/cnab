namespace WebSite.Entities;


public class Store
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Owner { get; private set; } = string.Empty;
    public IList<TransactionStore> Transactions { get; private set; } = [];

    private Store() { }

    public Store(string name, string owner)
    {
        Name = name;
        Owner = owner;
    }
}
