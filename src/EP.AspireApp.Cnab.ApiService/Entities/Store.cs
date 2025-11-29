namespace EP.AspireApp.Cnab.AppHost.Entities;

public class Store(Guid id, string name, string owner, IList<Transaction> transactions)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Owner { get; private set; } = owner;
    public IList<Transaction> Transactions { get; private set; } = transactions;
}
