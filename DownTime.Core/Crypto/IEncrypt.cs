namespace DownTime.Core.Crypto
{
    public interface IEncrypt
    {
        string Encrypt(string value);
        string Encrypt(byte[] bytes);
    }
}
