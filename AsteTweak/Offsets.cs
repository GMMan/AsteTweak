using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteTweak
{
    static class Offsets
    {
        public class Resolution
        {
            public uint TableBegin { get; set; }
        }

        public class KeyMapping
        {
            public uint Up { get; set; }
            public uint Down { get; set; }
            public uint Left { get; set; }
            public uint Right { get; set; }
            public uint Numpad8 { get; set; }
            public uint Numpad2 { get; set; }
            public uint Numpad4 { get; set; }
            public uint Numpad6 { get; set; }
            public uint Oem1 { get; set; }
            public uint Oem102 { get; set; }
            public uint Oem2 { get; set; }
            public uint Oem6 { get; set; }
            public uint TableBegin { get; set; }
            public uint Return { get; set; }
            public uint LShift { get; set; }
            public uint RShift { get; set; }
            public uint Escape { get; set; }
        }

        static Dictionary<uint, Resolution> resolutions = new Dictionary<uint, Resolution>
        {
			// Steam 2.04
            { 0x548E89B5, new Resolution { TableBegin = 0x00281ED0 } },
            // Trial 1.10
            { 0x52BE559A, new Resolution { TableBegin = 0x0027CD50 } },
            // Trial 1.12
            { 0x52C7EB1B, new Resolution { TableBegin = 0x0027D1B0 } }
        };

        static Dictionary<uint, KeyMapping> keyMappings = new Dictionary<uint, KeyMapping>
        {
			// Steam 2.04
            { 0x548E89B5, new KeyMapping {
                Up = 0x001FED94, Down = 0x001FEDAE, Left = 0x001FEDC4, Right = 0x001FEDDE,
                Numpad8 = 0x001FEDF4, Numpad2 = 0x001FEE0E, Numpad4 = 0x001FEE24, Numpad6 = 0x001FEE3E,
                Oem1 = 0x001FEE54, Oem102 = 0x001FEE71, Oem2 = 0x001FEE8A, Oem6 = 0x001FEEA7,
                TableBegin = 0x00290758,
                Return = 0x001FEEFD, LShift = 0x001FEF1A, RShift = 0x001FEF2B, Escape = 0x001FEF49
            } },
            // Trial 1.10
            { 0x52BE559A, new KeyMapping {
                Up = 0x001ED564, Down = 0x001ED57E, Left = 0x001ED594, Right = 0x001ED5AE,
                Numpad8 = 0x001ED5C4, Numpad2 = 0x001ED5DE, Numpad4 = 0x001ED5F4, Numpad6 = 0x001ED60E,
                Oem1 = 0x001ED624, Oem102 = 0x001ED641, Oem2 = 0x001ED65A, Oem6 = 0x001ED677,
                TableBegin = 0x0028AF70,
                Return = 0x001ED6CD, LShift = 0x001ED6EA, RShift = 0x001ED6FB, Escape = 0x001ED719
            } },
            // Trial 1.12
            { 0x52C7EB1B, new KeyMapping {
                Up = 0x001EDA34, Down = 0x001EDA4E, Left = 0x001EDA64, Right = 0x001EDA7E,
                Numpad8 = 0x001EDA94, Numpad2 = 0x001EDAAE, Numpad4 = 0x001EDAC4, Numpad6 = 0x001EDADE,
                Oem1 = 0x001EDAF4, Oem102 = 0x001EDB11, Oem2 = 0x001EDB2A, Oem6 = 0x001EDB47,
                TableBegin = 0x0028B3D0,
                Return = 0x001EDB9D, LShift = 0x001EDBBA, RShift = 0x001EDBCB, Escape = 0x001EDBE9
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
        public static Dictionary<uint, KeyMapping> KeyMappings { get { return keyMappings; } }
    }
}
