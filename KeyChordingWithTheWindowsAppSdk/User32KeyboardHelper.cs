using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Windows.System;

namespace KeyChordingWithTheWindowsAppSdk
{
	/// <summary>
	/// Note, the Win32 nVirtKey values used in GetKeyState and the array indexing used in
	/// GetKeyboardState in USER32.dll map exactly to the Windows.System.VirtualKey values.
	/// </summary>
	internal static class User32KeyboardHelper
	{
		#region Imports

		/// <summary>
		/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeystate
		/// </summary>
		[DllImport("USER32.dll", SetLastError = true)]
		private static extern short GetKeyState(int nVirtKey);

		/// <summary>
		/// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardstate
		/// </summary>
		[DllImport("USER32.dll", SetLastError = true)]
		private static extern bool GetKeyboardState(byte[] lpKeyState);

		#endregion Imports

		/// <summary>
		/// If the high-order bit of the value returned by GetKeyState is 1,
		/// the key is down; otherwise, it is up.
		/// </summary>
		public static bool IsKeyPressed(VirtualKey key) => GetKeyState((int)key) < 0;

		/// <summary>
		/// This might be useful if chording more than two keys.
		/// </summary>
		public static bool AreKeysPressed(VirtualKey[] keys)
		{
			var keyboardState = new byte[256];

			// GetKeyboardState returns 'true' iff successful.
			if (GetKeyboardState(keyboardState))
			{
				// Return 'true' iff all the requested keys are pressed.
				return keys.All(key => keyboardState[(int) key] >= 128);
			}

			return false;
		}

		/// <summary>
		/// Useful if you have a qualifier and a value you want to test, for example
		/// A and B  pressed together means do something, but it is the next key tells
		/// you what.
		/// </summary>
		public static bool AreKeysPressed(VirtualKey[] qualifiers, VirtualKey key)
		{
			if (qualifiers == null) { return false; }

			// This only returns 'true' when the qualifiers are pressed and key is 
			// not one of the qualifiers.
			return AreKeysPressed(qualifiers) && IsKeyPressed(key) && !qualifiers.Contains(key);
		}

		/// <summary>
		/// Useful when handling mouse clicks.  Note that the VirtualKey enumeration
		/// does not define values for every int less than 256.
		/// </summary>
		public static VirtualKey[] GetPressedKeys()
		{
			var list = new List<VirtualKey>();

			var keyboardState = new byte[256];

			// GetKeyboardState returns 'true' iff successful.
			if (GetKeyboardState(keyboardState))
			{
				for (var i = 0; i < keyboardState.Length; i++)
				{
					if (keyboardState[i] > 127) { list.Add((VirtualKey)i); }
				}
			}

			return list.ToArray();
		}
	}
}
