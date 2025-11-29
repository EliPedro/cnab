using EP.AspireApp.Cnab.AppHost.Shared;
using EP.AspireApp.Cnab.AppHost.ValueObject;
using MediatR;

namespace EP.AspireApp.Cnab.AppHost.Features.Upload;

public class UploadCommand : IRequest<Result<Guid>>
{
    public int TypeCode { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Card { get; set; } = string.Empty;
    public string StoreOwner { get; set; } = string.Empty;
    public string StoreName { get; set; } = string.Empty;
    public TransactionType TransactionType => TransactionType.FromCode(TypeCode);
}
