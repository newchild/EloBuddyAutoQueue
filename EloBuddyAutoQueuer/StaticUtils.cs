using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EloBuddyAutoQueuer
{
	class StaticUtils
	{
		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteObject(IntPtr value);

		public static BitmapSource GetImageStream(Image myImage)
		{
			var bitmap = new Bitmap(myImage);
			IntPtr bmpPt = bitmap.GetHbitmap();
			BitmapSource bitmapSource =
			 System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
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
			
			byte[] bytes = Convert.FromBase64String(Input);

			Image image;
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				image = Image.FromStream(ms);
			}

			return image;
		}
	}
}
