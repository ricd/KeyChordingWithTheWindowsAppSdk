# Key-Chording with the Windows App SDK
Key-chording, simultaneously pressing multiple keys to create a UI gesture, has been 
a part of the Windows UI language since the early days. It is a powerful way to pack 
a load of functionality into a sparse UI. 

The Windows App SDK supports limited chording with both keyboard and pointer events.  
When a keyboard key is pressed or released you also have direct access to the state 
of the Menu key (KeyRoutedEventArgs.KeyStatus.IsMenuKeyDown).  When a pointer is 
pressed or released you also have access to the state of the Control, Menu, Shift 
and Windows keys via the KeyModifiers property of the PointerRoutedEventArgs.

This repo contains small chunk of reusable C# code, User32KeyboardHelper that allows 
you to get the state of any keyboard key at any time.

In the Win32 world, the USE32.dll contains two functions that provide direct keyboard access:
•	GetKeyState(int nVirtKey) returns the state of the single specified key.
•	GetKeyboardState([out] lpKeyState) returns a byte array describing the state of every key.

The VirtualKey enumeration exists in the Windows App SDK (WinRT) world.  It defines unique 
integer values for the commonly used keyboard, mouse, and gamepad keys.  Cast it to an int, 
and a VirtualKey value exactly maps to the int needed for GetKeyState. And that int can also 
be used as an index into the array of key state values returned by GetKeyboardState.

User32KeyboardHelper wraps GetKeyState and GetKeyboardState
