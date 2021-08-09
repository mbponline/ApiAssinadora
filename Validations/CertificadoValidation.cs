using FluentValidation;
public class CertificadoInputPostDTOValidator : AbstractValidator<CertificadoInputPostDTO>
{
    public CertificadoInputPostDTOValidator()
    {
        RuleFor(c=> c.Password).NotNull().NotEmpty().WithMessage("Senha necessaria!");
        RuleFor(c => c.Arquivo.ContentType).NotNull().Must(x => x.Equals("application/x-pkcs12")).WithMessage(c => c.Arquivo.ContentType.ToString() + " Arquivo não é pfx");
    }
}
