namespace DownTime.Core.Crypto
{
    public class KeyProvider : IKeyProvider
    {
        private readonly IKeyProvider _crypto;

        public KeyProvider(IKeyProvider crypto)
        {
            _crypto = crypto;
        }

        public string Decrypt(string value)
        {
            return _crypto.Decrypt(value);
        }

        public string Decrypt(byte[] bytes)
        {
            return _crypto.Decrypt(bytes);
        }

        public string Encrypt(string value)
        {
            return _crypto.Encrypt(value);
        }

        public string Encrypt(byte[] bytes)
        {
            return _crypto.Encrypt(bytes);
        }
    }
}
