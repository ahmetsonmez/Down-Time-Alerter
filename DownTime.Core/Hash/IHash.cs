using System.Text;

namespace DownTime.Core.Hash
{
    public abstract class IHash
    {
        public abstract string HashText(string value);
        public abstract string HashFile(string filePath);
        public abstract string HashFile(byte[] bytes);

        protected string GetHashes(byte[] computedHashes)
        {
            var sBuilder = new StringBuilder();

            foreach (var computedHash in computedHashes)
                sBuilder.Append(computedHash.ToString("x2").ToLower());

            return sBuilder.ToString();
        }
    }
}
