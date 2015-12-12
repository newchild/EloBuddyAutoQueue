using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	class Crypto
	{
		public static string GetMD5Hash(string TextToHash)
		{
			
			
			if ((TextToHash == null) || (TextToHash.Length == 0))
			{
				return string.Empty;
			}


			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
			byte[] result = md5.ComputeHash(textToHash);

			return System.BitConverter.ToString(result).ToLower().Replace("-", "").Replace(" ", "");
		}
	}
}
