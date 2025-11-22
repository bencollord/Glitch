using System.Diagnostics;
using System.Text;

namespace Glitch
{
    public class Base64String : IEquatable<Base64String>, IFormattable
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        private const char PaddingChar = '=';
        private const int OctetMask = 0xFF;
        private const int SextetMask = 0x3F;

        private static readonly IReadOnlyDictionary<char, char> UrlMap = new Dictionary<char, char>()
        {
            ['+'] = '-',
            ['/'] = '_'
        };

        private string encoded;

        public Base64String(string encoded)
        {
            if (encoded.Length % CharChunk.Size != 0)
            {
                Debug.Fail("Not a multiple of 4");
                throw new FormatException("Input was not valid base64");
            }

            if (!encoded.All(e => Alphabet.Contains(e) || e == PaddingChar))
            {
                var badChars = encoded.Except(encoded.Where(e => Alphabet.Contains(e) || e == PaddingChar));
                Debug.Fail("Invalid characters: " + badChars.Join(", "));
                throw new FormatException("Input was not valid base64");
            }

            this.encoded = encoded;
        }

        public static Base64String FromUrlEncoded(string encoded)
        {
            var paddingChars = encoded.Length % 4;

            if (paddingChars == 3)
            {
                throw new ArgumentException("Invalid base64 input");
            }

            var buffer = new StringBuilder(encoded);

            foreach (var (basic, url) in UrlMap)
            {
                buffer.Replace(url, basic);
            }

            buffer.Append(PaddingChar, paddingChars);

            return new Base64String(buffer.ToString());
        }

        public static Base64String Encode(string plainText) => Encode(plainText, Encoding.UTF8);

        public static Base64String Encode(string plainText, Encoding encoding) => Encode(encoding.GetBytes(plainText));

        public static Base64String Encode(byte[] bytes) => Encode(bytes.AsSpan());

        public static Base64String Encode(ReadOnlySpan<byte> bytes)
        {
            ReadOnlySpan<byte> remainingBytes = bytes;
            Span<char> buffer = stackalloc char[CharChunk.Size];
            StringBuilder output = new(bytes.Length + bytes.Length / 2);

            while (remainingBytes.Length > 0)
            {
                var chunk = ByteChunk.Next(remainingBytes);

                chunk.Encode(buffer);
                output.Append(buffer);
                remainingBytes = remainingBytes.Slice(chunk.ByteCount);
            }

            return new Base64String(output.ToString());
        }

        public string DecodeString() => DecodeString(Encoding.UTF8);

        public string DecodeString(Encoding encoding) => encoding.GetString(DecodeBytes());

        public byte[] DecodeBytes()
        {
            using var stream = new MemoryStream();

            DecodeTo(stream);

            return stream.ToArray();
        }

        public void DecodeTo(Stream stream)
        {
            var remainingChars = encoded.AsSpan();

            Span<byte> buffer = stackalloc byte[ByteChunk.Size];

            while (remainingChars.Length > 0)
            {
                var chunk = CharChunk.Next(remainingChars);

                chunk.Decode(buffer);
                stream.Write(buffer.Slice(0, chunk.ByteCount));
                remainingChars = remainingChars.Slice(CharChunk.Size);
            }

            //while (remainingChars.Length > 0)
            //{
            //    var a = Alphabet.IndexOf(remainingChars[0]); // 00AAAAAA
            //    var b = Alphabet.IndexOf(remainingChars[1]); // 00BBBBBB
            //    var c = Alphabet.IndexOf(remainingChars[2]); // 00CCCCCC
            //    var d = Alphabet.IndexOf(remainingChars[3]); // 00DDDDDD

            //    var value = (a << 18) | (b << 12) | ((c & SextetMask) << 6) | (d & SextetMask);

            //    buffer[0] = (byte)(value >> 16);

            //    if (c < 0 && d < 0)
            //    {
            //        stream.WriteByte(buffer[0]);
            //        break;
            //    }

            //    buffer[1] = (byte)(value >> 8 & 0xFF);

            //    if (d < 0)
            //    {
            //        stream.Write(buffer.Slice(0, 2));
            //        break;
            //    }

            //    buffer[2] = (byte)(value & 0xFF);

            //    stream.Write(buffer);
            //    remainingChars = remainingChars.Slice(CharChunk.Size);
            //}
        }

        public bool Equals(Base64String? other) => other?.encoded == encoded;

        public override bool Equals(object? obj) => Equals(obj as Base64String);

        public override int GetHashCode() => encoded.GetHashCode();

        public override string ToString() => encoded;

        public string ToUrlSafeString()
        {
            var buffer = new StringBuilder(encoded.Length);

            for (int i = 0; i < encoded.Length; i++)
            {
                switch (encoded[i])
                {
                    case PaddingChar:
                        continue;

                    case char c when UrlMap.ContainsKey(c):
                        buffer.Append(UrlMap[c]);
                        break;

                    case char c:
                        buffer.Append(c);
                        break;
                }
            }

            return buffer.ToString();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return (format?.ToUpperInvariant()) switch
            {
                "G" or null => ToString(),
                "U" => ToUrlSafeString(),
                "B" => DecodeBytes().Select(b => b.ToString("X", formatProvider)).Join(' '),
                _ => throw new FormatException($"Invalid format string '{format}'"),
            };
        }

        private readonly record struct ByteChunk(byte? X, byte? Y, byte? Z)
        {
            internal const int Size = 3;

            internal static readonly ByteChunk Empty = new();

            internal int ByteCount => !X.HasValue ? 0 : !Y.HasValue ? 1 : !Z.HasValue ? 2 : 3;

            internal static ByteChunk Next(ReadOnlySpan<byte> buffer)
            {
                return buffer.Length switch
                {
                    0 => Empty,
                    1 => new(buffer[0], null, null),
                    2 => new(buffer[0], buffer[1], null),
                    _ => new(buffer[0], buffer[1], buffer[2])
                };
            }

            internal void Encode(Span<char> buffer)
            {
                Debug.Assert(buffer.Length >= CharChunk.Size);

                if (!X.HasValue)
                {
                    return;
                }

                var x = X.Value;
                var upperSixBitsX = x >> 2;
                var lowerTwoBitsX = x << 4 & 0b00110000;

                buffer[0] = Alphabet[upperSixBitsX];

                if (!Y.HasValue)
                {
                    // bytes = xxxxxxxx (none) (none)
                    // chars = 00xxxxx 00xx0000 (pad) (pad)
                    buffer[1] = Alphabet[lowerTwoBitsX];
                    buffer[2] = PaddingChar;
                    buffer[3] = PaddingChar;
                    return;
                }

                var y = Y.Value;
                var upperFourBitsY = y >> 4;
                var lowerFourBitsY = y << 2 & 0b00111100;

                buffer[1] = Alphabet[lowerTwoBitsX | upperFourBitsY];

                if (!Z.HasValue)
                {
                    // bytes xxxxxxxx yyyyyyyy (none)
                    // chars 00xxxxxx 00xxyyyy 00yyyy00
                    buffer[2] = Alphabet[lowerFourBitsY];
                    buffer[3] = PaddingChar;
                    return;
                }

                // bytes xxxxxxxx yyyyyyyy zzzzzzzz
                // chars 00xxxxxx 00xxyyyy 00yyyyzz 00zzzzzz
                var z = Z!.Value;

                buffer[2] = Alphabet[lowerFourBitsY | (z >> 6)];
                buffer[3] = Alphabet[z & SextetMask];
            }
        }

        private readonly record struct CharChunk(char A, char B, char C, char D)
        {
            internal const int Size = 4;

            internal int ByteCount => C == PaddingChar ? 1 : D == PaddingChar ? 2 : 3;

            internal static CharChunk Next(ReadOnlySpan<char> buffer)
            {
                if (buffer.Length < 4)
                {
                    throw new ArgumentException("Char chunk must be a multiple of 4");
                }

                return new(buffer[0], buffer[1], buffer[2], buffer[3]);
            }

            internal void Decode(Span<byte> buffer)
            {
                Debug.Assert(buffer.Length < 4);

                var a = Alphabet.IndexOf(A); // 00aaaaaa
                var b = Alphabet.IndexOf(B); // 00bbbbbb
                var c = Alphabet.IndexOf(C); // 00cccccc
                var d = Alphabet.IndexOf(D); // 00dddddd

                // 00000000aaaaaabbbbbbccccccdddddd
                var value = (a << 18) | (b << 12) | ((c & SextetMask) << 6) | (d & SextetMask);

                // aaaaaabb
                buffer[0] = (byte)(value >>> 16);

                if (c < 0)
                {
                    return;
                }

                // bbbbcccc
                buffer[1] = (byte)(value >> 8 & OctetMask);

                if (d < 0)
                {
                    return;
                }

                // ccdddddd
                buffer[2] = (byte)(value & OctetMask);
            }
        }
    }

}
