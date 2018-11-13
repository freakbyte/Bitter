using System.IO;
using static Bitter.BinaryStream;

namespace Bitter
{
    public class BinaryWrapper
    {
        private Endianness bitOrder = Endianness.LittleEndian;
        private Endianness byteOrder = Endianness.LittleEndian;
        private TextEncoding defaultTextEncoding = TextEncoding.DEFAULT;
        protected bool keepReadOpen = false;
        protected bool keepWriteOpen = false;

        /// <summary>
        /// Order of the bits to be read & written in.
        /// </summary>
        public Endianness BitOrder
        {
            get { return bitOrder; }
            set { bitOrder = value; }
        }

        /// <summary>
        /// Order of the bytes to be read & written in.
        /// </summary>
        public Endianness ByteOrder
        {
            get { return byteOrder; }
            set { byteOrder = value; }
        }

        /// <summary>
        /// Default encoding to be used on string.
        /// </summary>
        public TextEncoding DefaultTextEncoding
        {
            get { return defaultTextEncoding; }
            set
            {
                defaultTextEncoding = value;
            }
        }

        /// <summary>
        /// Reads a binary structure from a file.
        /// </summary>
        public void Read(string file)
        {
            BinaryStream bs = new BinaryStream(File.Open(file, FileMode.Open), byteOrder, bitOrder, defaultTextEncoding);
            Read(bs);
            if(!keepReadOpen)
            {
                bs.Dispose();
            }
        }

        /// <summary>
        /// Reads a binary structure from a stream.
        /// </summary>
        public void Read(Stream stream)
        {
            BinaryStream bs = new BinaryStream(stream, byteOrder, bitOrder, defaultTextEncoding);
            Read(bs);
            if (!keepReadOpen)
            {
                bs.Dispose();
            }
        }

        /// <summary>
        /// Reads a binary structure from a byte array.
        /// </summary>
        public void Read(byte[] bytes)
        {
            BinaryStream bs = new BinaryStream(new MemoryStream(bytes), byteOrder, bitOrder, defaultTextEncoding);
            Read(bs);
            if (!keepReadOpen)
            {
                bs.Dispose();
            }
        }

        /// <summary>
        /// Reads a binary structure from a BinaryStream. Override this one.
        /// </summary>
        public virtual void Read(BinaryStream bs)
        {

        }

        /// <summary>
        /// Writes a binary structure to a file.
        /// </summary>
        public void Write(string file)
        {
            BinaryStream bs = new BinaryStream(File.Open(file, FileMode.OpenOrCreate), byteOrder, bitOrder, defaultTextEncoding);
            Write(bs);
            if (!keepWriteOpen)
            {
                bs.Dispose();
            }
        }

        /// <summary>
        /// Writes a binary structure to a stream.
        /// </summary>
        public void Write(Stream stream)
        {
            BinaryStream bs = new BinaryStream(stream, byteOrder, bitOrder, defaultTextEncoding);
            Write(bs);
            if (!keepWriteOpen)
            {
                bs.Dispose();
            }
        }

        /// <summary>
        /// Writes a binary structure to a byte array.
        /// </summary>
        public void Write(out byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryStream bs = new BinaryStream(ms, byteOrder, bitOrder, defaultTextEncoding))
            {
                Write(bs);
                bytes = ms.ToArray();
            }
        }

        /// <summary>
        /// Writes a binary structure to a BinaryStream. Override this one.
        /// </summary>
        public virtual void Write(BinaryStream bs)
        {

        }

        public interface ReadWrite
        {
            void Read(BinaryStream bs);
            void Write(BinaryStream bs);
        }

        public interface ReadWriteInitialize : ReadWrite
        {
            void Init(object parameters = null);
        }

    }
}
