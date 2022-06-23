using System.Text;
using Microsoft.UI.Xaml.Input;

using Windows.System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace KeyChordingWithTheWindowsAppSdk
{

	public sealed partial class MainWindow // : Window
	{
		private readonly VirtualKey[] ChangeBackground = {VirtualKey.A, VirtualKey.S};

		private readonly SolidColorBrush _redBrush = new(Colors.DarkRed);
		private readonly SolidColorBrush _bludBrush = new(Colors.DarkBlue);

		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnRootGridKeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (User32KeyboardHelper.AreKeysPressed(ChangeBackground, e.Key))
			{
				if (e.Key == VirtualKey.R)
				{
					_rootGrid.Background = _redBrush;
				}
				else if (e.Key == VirtualKey.B)
				{
					_rootGrid.Background = _bludBrush;
				}
			}

			if (e.KeyStatus.IsMenuKeyDown)
			{
				_textBlock.Text = " Note: the Menu key is pressed.";
			}
		}

		private void OnRootGridKeyUp(object sender, KeyRoutedEventArgs e)
		{
			_textBlock.Text = string.Empty;
		}

		private void OnRootGridPointerPressed(object sender, PointerRoutedEventArgs e)
		{
			var pressedKeys = User32KeyboardHelper.GetPressedKeys();

			var builder = new StringBuilder();

			builder.AppendLine("Pointer Pressed. The following keyboard keys were also pressed:");

			foreach (var pressedKey in pressedKeys)
			{
				builder.AppendLine(pressedKey.ToString());
			}

			_textBlock.Text = builder.ToString();
		}
	}
}
