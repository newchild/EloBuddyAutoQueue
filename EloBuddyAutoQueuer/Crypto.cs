using System;
using System.Security.Cryptography;
using System.Text;

namespace EloBuddyAutoQueuer
{
	internal class Crypto
	{
		public static string GetMD5Hash(string TextToHash)
		{
			if ((TextToHash == null) || (TextToHash.Length == 0))
			{
				return string.Empty;
			}


			MD5 md5 = new MD5CryptoServiceProvider();
			var textToHash = Encoding.Default.GetBytes(TextToHash);
			var result = md5.ComputeHash(textToHash);

			return BitConverter.ToString(result).ToLower().Replace("-", "").Replace(" ", "");
		}
	}
}