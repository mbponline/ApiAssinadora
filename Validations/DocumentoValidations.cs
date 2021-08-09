using FluentValidation;
public class DocumentoInputPostXMLDTOValidator : AbstractValidator<DocumentoInputPostXMLDTO>
{
    public DocumentoInputPostXMLDTOValidator()
    {
        RuleFor(d => d.CertId).NotNull().NotEmpty().GreaterThan(0).WithMessage("ID do certificado necessaria!");
        RuleFor(d => d.Arquivo.ContentType).NotNull().Must(x => x.Equals("text/xml")).WithMessage(c => c.Arquivo.ContentType.ToString() + " Arquivo não é XML");
    }
}

public class DocumentoInputPostDTOValidator : AbstractValidator<DocumentoInputPostDTO>
{
    public DocumentoInputPostDTOValidator()
    {
        RuleFor(d => d.CertId).NotNull().NotEmpty().GreaterThan(0).WithMessage("ID do certificado necessaria!");
        RuleFor(d => d.Arquivo.ContentType).NotNull().Must(x => x.Equals("application/pdf")).WithMessage(c => c.Arquivo.ContentType.ToString() + " Arquivo não é PDF");
    }
}

