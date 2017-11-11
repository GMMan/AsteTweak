using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteTweak
{
    static class Offsets
    {
        public interface IKeyMapping
        {
            uint TableBegin { get; }
            string[] TableNames { get; }
        }

        public class Resolution
        {
            public uint TableBegin { get; set; }
        }

        public class AstebreedKeyMapping : IKeyMapping
        {
            [InputKey("Up", 0)]
            public uint Up { get; set; }
            [InputKey("Down", 1)]
            public uint Down { get; set; }
            [InputKey("Left", 2)]
            public uint Left { get; set; }
            [InputKey("Right", 3)]
            public uint Right { get; set; }
            [InputKey("NumPad 8", 4)]
            public uint Numpad8 { get; set; }
            [InputKey("NumPad 2", 5)]
            public uint Numpad2 { get; set; }
            [InputKey("NumPad 4", 6)]
            public uint Numpad4 { get; set; }
            [InputKey("NumPad 6", 7)]
            public uint Numpad6 { get; set; }
            [InputKey("OEM 1", 8, true)]
            public uint Oem1 { get; set; }
            [InputKey("OEM 102", 9, true)]
            public uint Oem102 { get; set; }
            [InputKey("OEM 2", 10, true)]
            public uint Oem2 { get; set; }
            [InputKey("OEM 6", 11, true)]
            public uint Oem6 { get; set; }
            [InputKey(null, 12)]
            public uint TableBegin { get; set; }
            [InputKey("Return", 24)]
            public uint Return { get; set; }
            [InputKey("Left Shift", 25)]
            public uint LShift { get; set; }
            [InputKey("Right Shift", 26)]
            public uint RShift { get; set; }
            [InputKey("Escape", 27)]
            public uint Escape { get; set; }

            public string[] TableNames { get; } = new string[]
            {
                "0Button (Z)",
                "1Button (X)",
                "2Button (C)",
                "3Button (V)",
                "4Button (A)",
                "5Button (S)",
                "6Button (D)",
                "7Button (F)",
                "8Button (Q)",
                "9Button (W)",
                "10Button (E)",
                "11Button (R)",
            };
        }

        public class AstebreedDefinitiveKeyMapping : IKeyMapping
        {
            [InputKey("Up", 0)]
            public uint Up { get; set; }
            [InputKey("Down", 1)]
            public uint Down { get; set; }
            [InputKey("Left", 2)]
            public uint Left { get; set; }
            [InputKey("Right", 3)]
            public uint Right { get; set; }
            [InputKey("W", 4)]
            public uint W { get; set; }
            [InputKey("S", 5)]
            public uint S { get; set; }
            [InputKey("A", 6)]
            public uint A { get; set; }
            [InputKey("D", 7)]
            public uint D { get; set; }
            [InputKey(null, 8)]
            public uint TableBegin { get; set; }
            [InputKey("Return", 20)]
            public uint Return { get; set; }
            [InputKey("Left Shift", 21)]
            public uint LShift { get; set; }
            [InputKey("Right Shift", 22)]
            public uint RShift { get; set; }
            [InputKey("Escape", 23)]
            public uint Escape { get; set; }

            public string[] TableNames { get; } = new string[]
            {
                "0Button (V)",
                "1Button (B)",
                "2Button (N)",
                "3Button (M)",
                "4Button (G)",
                "5Button (H)",
                "6Button (J)",
                "7Button (K)",
                "8Button (Y)",
                "9Button (U)",
                "10Button (I)",
                "11Button (O)",
            };
        }

        static Dictionary<uint, Resolution> resolutions = new Dictionary<uint, Resolution>
        {
			// Steam 2.04
            { 0x548E89B5, new Resolution { TableBegin = 0x00281ED0 } },
            // Trial 1.10
            { 0x52BE559A, new Resolution { TableBegin = 0x0027CD50 } },
            // Trial 1.12
            { 0x52C7EB1B, new Resolution { TableBegin = 0x0027D1B0 } },
            // Steam 3.00
            { 0x5A02629E, new Resolution { TableBegin = 0x0031C170} },
            // DRM-free 3.00
            { 0x59F2F999, new Resolution { TableBegin = 0x0031B920} }
        };

        static Dictionary<uint, IKeyMapping> keyMappings = new Dictionary<uint, IKeyMapping>
        {
			// Steam 2.04
            { 0x548E89B5, new AstebreedKeyMapping {
                Up = 0x001FED94, Down = 0x001FEDAE, Left = 0x001FEDC4, Right = 0x001FEDDE,
                Numpad8 = 0x001FEDF4, Numpad2 = 0x001FEE0E, Numpad4 = 0x001FEE24, Numpad6 = 0x001FEE3E,
                Oem1 = 0x001FEE54, Oem102 = 0x001FEE71, Oem2 = 0x001FEE8A, Oem6 = 0x001FEEA7,
                TableBegin = 0x00290758,
                Return = 0x001FEEFD, LShift = 0x001FEF1A, RShift = 0x001FEF2B, Escape = 0x001FEF49
            } },
            // Trial 1.10
            { 0x52BE559A, new AstebreedKeyMapping {
                Up = 0x001ED564, Down = 0x001ED57E, Left = 0x001ED594, Right = 0x001ED5AE,
                Numpad8 = 0x001ED5C4, Numpad2 = 0x001ED5DE, Numpad4 = 0x001ED5F4, Numpad6 = 0x001ED60E,
                Oem1 = 0x001ED624, Oem102 = 0x001ED641, Oem2 = 0x001ED65A, Oem6 = 0x001ED677,
                TableBegin = 0x0028AF70,
                Return = 0x001ED6CD, LShift = 0x001ED6EA, RShift = 0x001ED6FB, Escape = 0x001ED719
            } },
            // Trial 1.12
            { 0x52C7EB1B, new AstebreedKeyMapping {
                Up = 0x001EDA34, Down = 0x001EDA4E, Left = 0x001EDA64, Right = 0x001EDA7E,
                Numpad8 = 0x001EDA94, Numpad2 = 0x001EDAAE, Numpad4 = 0x001EDAC4, Numpad6 = 0x001EDADE,
                Oem1 = 0x001EDAF4, Oem102 = 0x001EDB11, Oem2 = 0x001EDB2A, Oem6 = 0x001EDB47,
                TableBegin = 0x0028B3D0,
                Return = 0x001EDB9D, LShift = 0x001EDBBA, RShift = 0x001EDBCB, Escape = 0x001EDBE9
            } },
            // Steam 3.00
            { 0x5A02629E, new AstebreedDefinitiveKeyMapping {
                Up = 0x0022EAFF, Down = 0x0022EB1B, Left = 0x0022EB37, Right = 0x0022EB53,
                W = 0x0022EAF1, S = 0x0022EB12, A = 0x0022EB2E, D = 0x0022EB4A,
                TableBegin = 0x003291C8,
                Return = 0x0022EBA2, LShift = 0x0022EBBA, RShift = 0x0022EBC6, Escape = 0x0022EBDC
            } },
            // DRM-free 3.00
            { 0x59F2F999, new AstebreedDefinitiveKeyMapping {
                Up = 0x0022E3DF, Down = 0x0022E3FB, Left = 0x0022E417, Right = 0x0022E433,
                W = 0x0022E3D1, S = 0x0022E3F2, A = 0x0022E40E, D = 0x0022E42A,
                TableBegin = 0x003286E8,
                Return = 0x0022E482, LShift = 0x0022E49A, RShift = 0x0022E4A6, Escape = 0x0022E4BC
            } }
            /* Template:
            { 0x, new KeyMapping {
                Up = 0x, Down = 0x, Left = 0x, Right = 0x,
                Numpad8 = 0x, Numpad2 = 0x, Numpad4 = 0x, Numpad6 = 0x,
                Oem1 = 0x, Oem102 = 0x, Oem2 = 0x, Oem6 = 0x,
                TableBegin = 0x,
                Return = 0x, LShift = 0x, RShift = 0x, Escape = 0x
            } }
            */
        };

        public static Dictionary<uint, Resolution> Resolutions { get { return resolutions; } }
        public static Dictionary<uint, IKeyMapping> KeyMappings { get { return keyMappings; } }
    }
}
