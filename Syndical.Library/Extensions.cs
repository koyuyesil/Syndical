using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Syndical.Library
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Get string out of input stream
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>Input stream data</returns>
        public static string GetString(this HttpWebResponse source)
        {
            using var stream = new StreamReader(source.GetResponseStream(), Encoding.UTF8);
            return stream.ReadToEnd();
        }

        /// <summary>
        /// Normalizes version string
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>Normalized version string</returns>
        public static string NormalizeVersion(this string source)
        {
            var split = source.Split('/').ToList();
            if (split.Count == 3)
                split.Add(split[0]);
            if (split[2] == "")
                split[2] = split[0];
            return string.Join('/', split);
        }
        
        /// <summary>
        /// Get MD5 hashsum digest
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>MD5 hashsum digest</returns>
        public static string GetMd5Hash(this string source)
        {
            // Use input string to calculate MD5 hash
            using var md5 = MD5.Create();
            byte[] inputBytes = source.ToUtf8Bytes();
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (var t in hashBytes)
                sb.Append(t.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>
        /// Convert string to UTF-8 byte sequence
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>UTF-8 byte sequence</returns>
        public static byte[] ToUtf8Bytes(this string source)
            => Encoding.UTF8.GetBytes(source);
        
        /// <summary>
        /// Convert UTF-8 byte sequence to string
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>String</returns>
        public static string ToUtf8String(this byte[] source)
            => Encoding.UTF8.GetString(source);
    }
}