using System.Security.Cryptography;
using System.Text;

namespace CurrencyExсhanger.Web.Extensions
{
    public static class StringExtensions
    {
        public static string GetHash(this string item)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(item);

            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
