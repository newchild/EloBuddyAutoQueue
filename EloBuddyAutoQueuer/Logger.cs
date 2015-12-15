using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	///     Interaktionslogik für Logger.xaml
	/// </summary>
	public partial class Logger : Window
	{
		public Logger()
		{
			InitializeComponent();
		}


		public void reset()
		{
			richTextBox.Document.Blocks.Clear();
		}

		public void Log(string text)
		{
			Dispatcher.Invoke(delegate
			{
				var textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
				textRange.Text = "[LOG]: ";
				textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
				textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
				var rangeOfWord = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
				rangeOfWord.Text = text;
				rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
				rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
			});
		}

		public void Error(string Error)
		{
			Dispatcher.Invoke(delegate
			{
				var textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
				textRange.Text = "[ERROR]: ";
				textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
				textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
				var rangeOfWord = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
				rangeOfWord.Text = Error;
				rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
				rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
			});
		}

		public void Warning(string Warning)
		{
			Dispatcher.Invoke(delegate
			{
				var textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
				textRange.Text = "[LOG]: ";
				textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green);
				textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
				var rangeOfWord = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
				rangeOfWord.Text = Warning;
				rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
				rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
			});
		}
	}
}