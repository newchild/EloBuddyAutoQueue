namespace EloBuddyAutoQueuer
{
	internal class TempPatcher
	{
		public static void Patch()
		{
			VersionHandler.CopyEBFiles();
		}
	}
}