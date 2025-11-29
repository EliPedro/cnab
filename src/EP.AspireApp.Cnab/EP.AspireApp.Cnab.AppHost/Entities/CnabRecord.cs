using EP.AspireApp.Cnab.AppHost.ValueObject;

namespace EP.AspireApp.Cnab.AppHost.Entities;

public class CnabRecord
{
    /// <summary>
    /// Transaction type
    /// </summary>
    public int TypeCode { get; set; }

    /// <summary>
    /// Date of occurrence
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Transaction amount (divide by 100.00)
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Beneficiary's CPF
    /// </summary>
    public string Cpf { get; set; } = string.Empty;

    /// <summary>
    /// Card used in the transaction
    /// </summary>
    public string Card { get; set; } = string.Empty;

    /// <summary>
    /// Store representative name
    /// </summary>
    public string StoreOwner { get; set; } = string.Empty;

    /// <summary>
    /// Store name
    /// </summary>
    public string StoreName { get; set; } = string.Empty;

    /// <summary>
    /// Transaction Types
    /// </summary>
    public TransactionType TransactionType => TransactionType.FromCode(TypeCode);

}
