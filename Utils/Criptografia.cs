using System.Security.Cryptography;
using System.Text;

public class Criptografia
{
    public string Criptografar(string input, bool RSA)
    {
        ASCIIEncoding ByteConverter = new ASCIIEncoding();
        RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
        byte[] criptografar = ByteConverter.GetBytes(input);
        byte[] cripotografado = RSAalg.Encrypt(criptografar, RSA);
        return ByteConverter.GetString(cripotografado);
    }

    public string Descriptografar(string input, bool RSA)
    {
        ASCIIEncoding ByteConverter = new ASCIIEncoding();
        RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
        byte[] descriptografar = ByteConverter.GetBytes(input);
        byte[] descripotografado = RSAalg.Decrypt(descriptografar, RSA);
        return ByteConverter.GetString(descripotografado);
    }
}
