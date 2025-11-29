using FluentValidation;

namespace EP.AspireApp.Cnab.AppHost.Features.Upload;

public class UploadValidator : AbstractValidator<UploadCommand>
{
    public UploadValidator()
    {
        RuleFor(c => c.Cpf).NotEmpty();
    }
}
