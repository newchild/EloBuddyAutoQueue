using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	/// Interaktionslogik für Logger.xaml
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
			TextRange textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
			textRange.Text = "[LOG]: ";
			textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
			textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
			TextRange rangeOfWord = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
			rangeOfWord.Text = text;
			rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
			rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
			
		}

		public void Error(string Error)
		{
			TextRange textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
			textRange.Text = "[ERROR]: ";
			textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
			textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
			TextRange rangeOfWord = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
			rangeOfWord.Text = Error;
			rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
			rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
			
		}

		public void Warning(string Warning)
		{
			TextRange textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
			textRange.Text = "[LOG]: ";
			textRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Yellow);
			textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
			TextRange rangeOfWord = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
			rangeOfWord.Text = Warning;
			rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
			rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
			
		}
	}
}
