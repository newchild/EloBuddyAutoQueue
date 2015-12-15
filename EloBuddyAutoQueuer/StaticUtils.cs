using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace EloBuddyAutoQueuer
{
	internal class StaticUtils
	{
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