using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

public class XMLSigner
{

    public byte[] SingXML(byte[] certificado, string password, byte[] xml)
    {
        SecureString key = new NetworkCredential("", password).SecurePassword;
        X509Certificate2 cert = new X509Certificate2(certificado, key);

        XmlDocument doc = new XmlDocument();
        //doc.PreserveWhitespace = true;
        doc.Load(new MemoryStream(xml));

        SignedXml signedXml = new SignedXml(doc);
        signedXml.SigningKey = cert.PrivateKey;

        //
        // Add a signing reference, the uri is empty and so the whole document
        // is signed.
        Reference reference = new Reference();
        reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
        reference.AddTransform(new XmlDsigExcC14NTransform());
        reference.Uri = "";
        signedXml.AddReference(reference);

        //
        // Add the certificate as key info, because of this the certificate
        // with the public key will be added in the signature part.
        KeyInfo keyInfo = new KeyInfo();
        keyInfo.AddClause(new KeyInfoX509Data(cert));
        signedXml.KeyInfo = keyInfo;

        // Generate the signature.
        signedXml.ComputeSignature();

        // Appends the sigbature at the end of the xml document.
        doc.DocumentElement.AppendChild(signedXml.GetXml());

        byte[] xmlsig;
        using (var ms = new MemoryStream())
        {
            doc.Save(ms);
            xmlsig = ms.ToArray();
        }
        return xmlsig;
    }
}