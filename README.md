# Key-Chording with the Windows App SDK
Key-chording, simultaneously pressing multiple keys to create a UI gesture, has been 
a part of the Windows UI language since the early days. It is a powerful way to pack 
a load of functionality into a sparse UI. 

The Windows App SDK supports limited chording with both keyboard and pointer events.  
When a keyboard key is pressed or released you also have direct access to the state 
of the Menu key (KeyRoutedEventArgs.KeyStatus.IsMenuKeyDown).  When a pointer is 
pressed or released you can also get the state of the Control, Menu, Shift 
and Windows keys via the KeyModifiers property of the PointerRoutedEventArgs.

This repo contains a small chunk of reusable C# code, User32KeyboardHelper that extends
these build-in capabilities by allowing you to get the state of any key at any time.

## The bits and pieces:

In the Win32 world, the USE32.dll contains two functions that provide direct keyboard access:
•	GetKeyState(int nVirtKey) returns the state of the single specified key.
•	GetKeyboardState([out] lpKeyState) returns a byte array describing the state of every key.

The VirtualKey enumeration exists in the Windows App SDK (WinRT) world.  It defines unique 
integer values for the commonly used keyboard, mouse, and gamepad keys.  Cast it to an int, 
and a VirtualKey value exactly maps to the int needed for GetKeyState. And that int can also 
be used as an index into the array of key state values returned by GetKeyboardState.

## Implementation

User32KeyboardHelper wraps GetKeyState and GetKeyboardState in methods that accept (GetKeyState)
or return (GetKeyboardState) VirtualKey values. It also provides convienece functions for chording 
multiple keys.

## The Sample Application

To tryout keyboard chording, press A and S and then either B, to turn the window background blue,
or R to turn it red.

Tryout mouse chording by first pressing one or more keyboard keys and then one of your mouse button.
