using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AsteTweak
{
    /// <summary>
    /// Really light PE parser for really basic info
    /// </summary>
    class PeLite
    {
        Stream inStream;
        BinaryReader br;
        BinaryWriter bw;
        long mzOffset;
        long peOffset;
        long optHeaderOffset;
        long dataDirOffset;
        long sectionsOffset;

        public PeLite(Stream stream)
        {
            if (!stream.CanSeek) throw new ArgumentException("Stream is not seekable.");
            inStream = stream;
            br = new BinaryReader(stream);
            if (stream.CanWrite) bw = new BinaryWriter(stream);

            mzOffset = stream.Position;
            if (br.ReadUInt16() != 0x5a4d) throw new ArgumentException("EXE file does not start at current stream position.");
            stream.Seek(0x3a, SeekOrigin.Current);
            int extOffset = br.ReadInt32();
            peOffset = mzOffset + extOffset;
            stream.Seek(peOffset, SeekOrigin.Begin);
            if (br.ReadUInt32() != 0x4550) throw new ArgumentException("EXE is not a PE.");
            stream.Seek(16, SeekOrigin.Current);
            ushort sizeOfOptHeader = br.ReadUInt16();
            stream.Seek(2, SeekOrigin.Current);
            optHeaderOffset = stream.Position;
            if (br.ReadUInt16() != 0x10b) throw new NotSupportedException("Only PE32 is supported.");
            dataDirOffset = optHeaderOffset + 60;
            sectionsOffset = optHeaderOffset + sizeOfOptHeader;
        }

        /*public uint GetImageBase()
        {
            inStream.Seek(optHeaderOffset + 0x1c, SeekOrigin.Begin);
            return br.ReadUInt32();
        }

        public uint GetPeLength()
        {
            inStream.Seek(peOffset + 6, SeekOrigin.Begin);
            ushort numSections = br.ReadUInt16();

            inStream.Seek(sectionsOffset + 16, SeekOrigin.Begin);
            uint maxoffset = 0, maxsize = 0;
            for (int i = 0; i < numSections; ++i)
            {
                uint s = br.ReadUInt32();
                uint o = br.ReadUInt32();
                if (o > maxoffset)
                {
                    maxoffset = o;
                    maxsize = s;
                }
                inStream.Seek(32, SeekOrigin.Current);
            }

            return maxoffset + maxsize;
        }

        public uint ConvertRvaToOffset(uint rva)
        {
            inStream.Seek(peOffset + 6, SeekOrigin.Begin);
            ushort numSections = br.ReadUInt16();

            inStream.Seek(sectionsOffset + 12, SeekOrigin.Begin);
            List<Tuple<uint, uint>> offsets = new List<Tuple<uint, uint>>();
            for (int i = 0; i < numSections; ++i)
            {
                uint va = br.ReadUInt32();
                inStream.Seek(4, SeekOrigin.Current);
                uint raw = br.ReadUInt32();
                offsets.Add(new Tuple<uint, uint>(va, raw));
                inStream.Seek(0x1c, SeekOrigin.Current);
            }

            offsets.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            for (int i = 0; i < numSections - 1; ++i)
            {
                if (rva >= offsets[i].Item1 && rva < offsets[i + 1].Item1) return offsets[i].Item2 + (rva - offsets[i].Item1);
            }

            // Assume offset isn't way past the PE
            return offsets[numSections - 1].Item2 + (rva - offsets[numSections - 1].Item1);
        }*/

        // Returns uint instead of DateTime for ease of use in this project
        public uint GetImageTimestamp()
        {
            inStream.Seek(peOffset + 8, SeekOrigin.Begin);
            return br.ReadUInt32();
        }
    }
}
