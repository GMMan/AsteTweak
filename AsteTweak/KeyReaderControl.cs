using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsteTweak
{
    public partial class KeyReaderControl : UserControl
    {
        public KeyReaderControl()
        {
            InitializeComponent();
        }

        public string Text
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
            }
        }

        public Keys LastPressedKey { get; set; }

        public event KeyEventHandler KeyPressed = delegate { };

        private void KeyReaderControl_KeyDown(object sender, KeyEventArgs e)
        {
            LastPressedKey = e.KeyCode;
            KeyPressed(this, e);
            Visible = false;
        }

        private void KeyReaderControl_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    LastPressedKey = Keys.LButton;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    LastPressedKey = Keys.MButton;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    LastPressedKey = Keys.RButton;
                    break;
                case System.Windows.Forms.MouseButtons.XButton1:
                    LastPressedKey = Keys.XButton1;
                    break;
                case System.Windows.Forms.MouseButtons.XButton2:
                    LastPressedKey = Keys.XButton2;
                    break;
            }
            KeyPressed(this, new KeyEventArgs(LastPressedKey));
            Visible = false;
        }

        // From https://stackoverflow.com/a/27698458/1180879, for differentiation between left keys and right keys, with additional code for Alt
        protected override bool ProcessKeyMessage(ref Message m)
        {
            if ((m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP || m.Msg == WM_SYSKEYUP || m.Msg == WM_SYSKEYDOWN) && ((int)m.WParam == VK_CONTROL || (int)m.WParam == VK_SHIFT || (int)m.WParam == VK_MENU))
            {
                KeyEventArgs e = new KeyEventArgs(Keys.None);
                switch ((OemScanCode)(((long)m.LParam >> 16) & 0x1FF))
                {
                    case OemScanCode.LControl:
                        e = new KeyEventArgs(Keys.LControlKey);
                        break;
                    case OemScanCode.RControl:
                        e = new KeyEventArgs(Keys.RControlKey);
                        break;
                    case OemScanCode.LShift:
                        e = new KeyEventArgs(Keys.LShiftKey);
                        break;
                    case OemScanCode.RShift:
                        e = new KeyEventArgs(Keys.RShiftKey);
                        break;
                    case OemScanCode.LAlternate:
                        e = new KeyEventArgs(Keys.LMenu);
                        break;
                    case OemScanCode.RAlternate:
                        e = new KeyEventArgs(Keys.RMenu);
                        break;
                    default:
                        if ((int)m.WParam == VK_SHIFT)
                            e = new KeyEventArgs(Keys.ShiftKey);
                        else if ((int)m.WParam == VK_CONTROL)
                            e = new KeyEventArgs(Keys.ControlKey);
                        break;
                }
                if (e.KeyData != Keys.None)
                {
                    if (m.Msg == WM_KEYDOWN || m.Msg == WM_SYSKEYDOWN)
                        OnKeyDown(e);
                    else
                        OnKeyUp(e);
                    return true;
                }
            }
            return base.ProcessKeyMessage(ref m);
        }
        
        private void label_MouseUp(object sender, MouseEventArgs e)
        {
            KeyReaderControl_MouseUp(sender, e);
        }

        #region Scan code & window message stuff
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x0104;
        const int WM_SYSKEYUP = 0x0105;

        const int VK_SHIFT = 0x10;
        const int VK_CONTROL = 0x11;
        /// <summary>
        /// Alt key
        /// </summary>
        const int VK_MENU = 0x12;

        /// <summary>
        /// List of scan codes for standard 104-key keyboard US English keyboard
        /// </summary>
        enum OemScanCode
        {
            /// <summary>
            /// ` ~
            /// </summary>
            BacktickTilde = 0x29,
            /// <summary>
            /// 1 !
            /// </summary>
            N1 = 2,
            /// <summary>
            /// 2 @
            /// </summary>
            N2 = 3,
            /// <summary>
            /// 3 #
            /// </summary>
            N3 = 4,
            /// <summary>
            /// 4 $
            /// </summary>
            N4 = 5,
            /// <summary>
            /// 5 %
            /// </summary>
            N5 = 6,
            /// <summary>
            /// 6 ^
            /// </summary>
            N6 = 7,
            /// <summary>
            /// 7 &
            /// </summary>
            N7 = 8,
            /// <summary>
            /// 8 *
            /// </summary>
            N8 = 9,
            /// <summary>
            /// 9 (
            /// </summary>
            N9 = 0x0A,
            /// <summary>
            /// 0 )
            /// </summary>
            N0 = 0x0B,
            /// <summary>
            /// - _
            /// </summary>
            MinusDash = 0x0C,
            /// <summary>
            /// = +
            /// </summary>
            Equals = 0x0D,
            Backspace = 0x0E,
            Tab = 0x0F,
            Q = 0x10,
            W = 0x11,
            E = 0x12,
            R = 0x13,
            T = 0x14,
            Y = 0x15,
            U = 0x16,
            I = 0x17,
            O = 0x18,
            P = 0x19,
            /// <summary>
            /// [ {
            /// </summary>
            LBracket = 0x1A,
            /// <summary>
            /// ] }
            /// </summary>
            RBracket = 0x1B,
            /// <summary>
            /// | \ (same as pipe)
            /// </summary>
            VerticalBar = 0x2B,
            /// <summary>
            /// | \ (same as vertical bar)
            /// </summary>
            Pipe = 0x2B,
            CapsLock = 0x3A,
            A = 0x1E,
            S = 0x1F,
            D = 0x20,
            F = 0x21,
            G = 0x22,
            H = 0x23,
            J = 0x24,
            K = 0x25,
            L = 0x26,
            /// <summary>
            /// ; :
            /// </summary>
            SemiColon = 0x27,
            /// <summary>
            /// ' "
            /// </summary>
            Quotes = 0x28,
            // Unused
            Enter = 0x1C,
            LShift = 0x2A,
            Z = 0x2C,
            X = 0x2D,
            C = 0x2E,
            V = 0x2F,
            B = 0x30,
            N = 0x31,
            M = 0x32,
            /// <summary>
            /// , <
            /// </summary>
            Comma = 0x33,
            /// <summary>
            /// . >
            /// </summary>
            Period = 0x34,
            /// <summary>
            /// / ?
            /// </summary>
            Slash = 0x35,
            RShift = 0x36,
            LControl = 0x1D,
            LAlternate = 0x38,
            SpaceBar = 0x39,
            RAlternate = 0x138,
            RControl = 0x11D,
            /// <summary>
            /// The menu key thingy
            /// </summary>
            Application = 0x15D,
            Insert = 0x152,
            Delete = 0x153,
            Home = 0x147,
            End = 0x14F,
            PageUp = 0x149,
            PageDown = 0x151,
            UpArrow = 0x148,
            DownArrow = 0x150,
            LeftArrow = 0x14B,
            RightArrow = 0x14D,
            NumLock = 0x145,
            NumPad0 = 0x52,
            NumPad1 = 0x4F,
            NumPad2 = 0x50,
            NumPad3 = 0x51,
            NumPad4 = 0x4B,
            NumPad5 = 0x4C,
            NumPad6 = 0x4D,
            NumPad7 = 0x47,
            NumPad8 = 0x48,
            NumPad9 = 0x49,
            NumPadDecimal = 0x53,
            NumPadEnter = 0x11C,
            NumPadPlus = 0x4E,
            NumPadMinus = 0x4A,
            NumPadAsterisk = 0x37,
            NumPadSlash = 0x135,
            Escape = 1,
            PrintScreen = 0x137,
            ScrollLock = 0x46,
            PauseBreak = 0x45,
            LeftWindows = 0x15B,
            RightWindows = 0x15C,
            F1 = 0x3B,
            F2 = 0x3C,
            F3 = 0x3D,
            F4 = 0x3E,
            F5 = 0x3F,
            F6 = 0x40,
            F7 = 0x41,
            F8 = 0x42,
            F9 = 0x43,
            F10 = 0x44,
            F11 = 0x57,
            F12 = 0x58,
        }
        #endregion
    }
}
