namespace WebSite.Entities;


public class Store
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Owner { get; private set; } = string.Empty;
    public IList<Transaction> Transactions { get; private set; } = [];

    private Store() { }

    public Store(int id, string name, string owner)
    {
        Id = id;
        Name = name;
        Owner = owner;
    }
}
