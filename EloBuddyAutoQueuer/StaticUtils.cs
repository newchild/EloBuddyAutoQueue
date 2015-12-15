using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace EloBuddyAutoQueuer
{
	internal static class StaticUtils
	{
		public static void Shuffle<T>(this IList<T> list)
		{
			Random rng = new Random();
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteObject(IntPtr value);

		public static BitmapSource GetImageStream(Image myImage)
		{
			var bitmap = new Bitmap(myImage);
			var bmpPt = bitmap.GetHbitmap();
			var bitmapSource =
				Imaging.CreateBitmapSourceFromHBitmap(
					bmpPt,
					IntPtr.Zero,
					Int32Rect.Empty,
					BitmapSizeOptions.FromEmptyOptions());

			//freeze bitmapSource and clear memory to avoid memory leaks
			bitmapSource.Freeze();
			DeleteObject(bmpPt);

			return bitmapSource;
		}

		public static Image LoadImage(string Input)
		{
			var bytes = Convert.FromBase64String(Input);

			Image image;
			using (var ms = new MemoryStream(bytes))
			{
				image = Image.FromStream(ms);
			}

			return image;
		}
	}
}