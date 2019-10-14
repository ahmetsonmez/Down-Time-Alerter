namespace DownTime.Core.Hash
{
    public interface ISaltHash
    {
        byte[] GetSalt(int maximumSaltLength = 16);
        string GetHash(string value);
        string GetHash(string value, byte[] salt);
        bool MatchHash(string value, string newValue, byte[] salt);
    }
}
