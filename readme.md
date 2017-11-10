Astebreed Tweaker
=================

This program allows you to adjust some hard-coded settings in the game Astebreed, such as screen resolution and key mappings.

Usage
=====
Run the program. If you have the Steam version of the game installed, it will be automatically loaded for editing.
Otherwise, click on the "Select EXE…" button to locate and load the EXE. The list of current settings in the EXE
will be populated. Click on a value to edit it. Click outside of the edit box to validate. Once you are done with
your modifications, click on the "Save" button to write them to the EXE. If not already done, the EXE will be backed
up with the "_orig" appended to the file name. If you want to restore the backup, click on "Restore".

About Key Mapping
=================
There's something special to note about the key mappings. Notably, you cannot assign certain keys to certain options.
This is due to compiler optimization only leaving enough space for the constant value used at compile time. The keys
affected are Up, Down, Left, Right, NumPad (8, 2, 4, 6), Return, and Escape. For these bindings, the substitiute must
have a virtual-key value less than or equal to 0x7f. See [here](https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx)
for a list of virtual-key codes.

Note for keys, you can also rebind them to your mouse, if you wish. Just click within the "Press a key" box to assign.

Click outside the box to cancel assignment.

Version Compatibility
=====================
This program is compatible with the following versions of the game:

* Steam 2.04
* Trial 1.10
* Trial 1.12
* Steam 3.00 (Definitive Edition)

Other versions will be added as I find them and when I have time to do so.

I do not have a DRM-free copy of the game, and the patches released on Edelweiss' site do not include complete files,
so I'll add support for DRM-free versions whenever I happen upon a copy.
