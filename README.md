
# Bitter
*.NET Standard 2.0 Library for manipulating binary data*

**The library supports reading and writing**
 - Induvidual bits
 - Integers of variable bit length
 - Signed Byte
 - Signed Short
 - Signed Int
 - Signed Long
 - Unsigned Byte
 - Unsigned Short
 - Unsigned Int
 - Unsigned Long
 - Half-precision floating-point numbers
 - Float
 - Double
 - Char
 - String
 - Arrays and Lists of everything above.

You can read and write to the **same stream**, making it easy to descramble data without the need of multiple streams.

### Quick usage guide, more will follow 
```c#
BinaryStream stream = new BinaryStream();       // if you dont provide it with a stream it'll use a MemoryStream internally.

stream.BitOrder = Endianness.LittleEndian;      // you can set both bit and byte order like this
stream.ByteOrder = Endianness.LittleEndian;     // or through the constructor
stream.DefaultTextEncoding = TextEncoding.UTF8; // what text encoding should we use as default, 
                                                // this can be set through the constructor.

BinaryReader read = stream.Read;                      // you can still use stream.Read.Type
BinaryWriter write = stream.Write;                    // i just think this makes my code easier to read

byte[] bytes = new byte[] { 1, 2, 4, 7 };
int[] ints = new int[] { -1, 200, 7600, 984, -999 };
List<float> floats = new List<float>(new float[] { 1f, -2f, 0.55f });

// write
write.ULong(123456);
write.ByteArray(bytes);
write.IntArrayAsBits(ints, 14);
write.HalfList(floats);
write.Bit(1);                   // a bit is represented by a byte
write.Bit(0);
write.Bit(1);
write.Bit(0);
write.String("test");

// reset pos
stream.Flush();                 // it's important to flush if you write bits
stream.ByteOffset = 0;          // changing offset will also flush dirty bits
stream.BitOffset = 0;           // writing data that is not aligned to a byte is a lot slower

// read
read.ULong();
read.ByteArray(4);
read.ShortListFromBits(ints.Length, 14);    // we wrote ints, but can read shorts
read.HalfArray(floats.Count);               // halfs are converted and returned as floats
read.BitArray(4);
read.String(4);

// seek
stream.BitOffset += 200;
stream.BitOffset--;
stream.ByteOffset = 55;
stream.ByteOffset++;


bool end = stream.EndOfStream;
long len = stream.Length;
```