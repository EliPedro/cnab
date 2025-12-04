using FluentValidation;

namespace WebSite.Features.Upload;

public class UploadValidator : AbstractValidator<UploadCommand>
{
    public UploadValidator()
    {
        RuleFor(c => c.Cpf).NotEmpty();
    }
}
