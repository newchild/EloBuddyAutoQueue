using System;

namespace EloBuddyAutoQueuer
{
	internal class Logging
	{
		public static void Log(string input)
		{
			WindowHandler.Instance.getLoggerInstance().Log(input + Environment.NewLine);
		}

		public static void Error(string input)
		{
			WindowHandler.Instance.getLoggerInstance().Error(input + Environment.NewLine);
		}

		public static void Warning(string input)
		{
			WindowHandler.Instance.getLoggerInstance().Warning(input + Environment.NewLine);
		}
	}
}