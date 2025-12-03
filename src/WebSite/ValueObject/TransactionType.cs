
namespace WebSite.ValueObject;

public record TransactionType(int Code, string Description, TransactionNature Nature, bool Positive)
{
    public static TransactionType FromCode(int code) => code switch
    {
        1 => new TransactionType(1, "Debit", TransactionNature.Income, true),
        2 => new TransactionType(2, "Boleto", TransactionNature.Expense, false),
        3 => new TransactionType(3, "Financing", TransactionNature.Expense, false),
        4 => new TransactionType(4, "Credit", TransactionNature.Income, true),
        5 => new TransactionType(5, "Loan Receipt", TransactionNature.Income, true),
        6 => new TransactionType(6, "Sales", TransactionNature.Income, true),
        7 => new TransactionType(7, "TED Receipt", TransactionNature.Income, true),
        8 => new TransactionType(8, "DOC Receipt", TransactionNature.Income, true),
        9 => new TransactionType(9, "Rent", TransactionNature.Expense, false),
        _ => throw new ArgumentOutOfRangeException(nameof(code), "Unknown transaction code.")

    };
}