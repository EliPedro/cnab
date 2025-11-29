
namespace EP.AspireApp.Cnab.AppHost.ValueObject;

public record TransactionType(int Code, string Description, TransactionNature Nature, int Sign)
{
    public static TransactionType FromCode(int code) => code switch
    {
        1 => new TransactionType(1, "Debit", TransactionNature.Income, +1),
        2 => new TransactionType(2, "Boleto", TransactionNature.Expense, -1),
        3 => new TransactionType(3, "Financing", TransactionNature.Expense, -1),
        4 => new TransactionType(4, "Credit", TransactionNature.Income, +1),
        5 => new TransactionType(5, "Loan Receipt", TransactionNature.Income, +1),
        6 => new TransactionType(6, "Sales", TransactionNature.Income, +1),
        7 => new TransactionType(7, "TED Receipt", TransactionNature.Income, +1),
        8 => new TransactionType(8, "DOC Receipt", TransactionNature.Income, +1),
        9 => new TransactionType(9, "Rent", TransactionNature.Expense, -1),
        _ => throw new ArgumentOutOfRangeException(nameof(code), "Unknown transaction code.")

    };
}