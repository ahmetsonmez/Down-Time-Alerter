namespace DownTime.Core.Crypto
{
    public interface IDecrypt
    {
        string Decrypt(string value);
        string Decrypt(byte[] bytes);
    }
}
