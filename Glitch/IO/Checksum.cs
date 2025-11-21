using Glitch.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Glitch.IO
{
    public sealed class Checksum : IEquatable<Checksum>, IFormattable
    {
        private static readonly Dictionary<HashAlgorithmName, Func<HashAlgorithm>> FactoryMap = new()
        {
            [HashAlgorithmName.MD5]      = MD5.Create,
            [HashAlgorithmName.SHA1]     = SHA1.Create,
            [HashAlgorithmName.SHA256]   = SHA256.Create,
            [HashAlgorithmName.SHA384]   = SHA384.Create,
            [HashAlgorithmName.SHA512]   = SHA512.Create,
            [HashAlgorithmName.SHA3_256] = SHA3_256.Create,
            [HashAlgorithmName.SHA3_384] = SHA3_384.Create,
            [HashAlgorithmName.SHA3_512] = SHA3_512.Create,
        };

        private readonly byte[] value;
        private readonly HashAlgorithmName algorithmName;

        public Checksum(byte[] value, HashAlgorithmName algorithmName)
        {
            this.value = value;
            this.algorithmName = algorithmName;
        }

        public static Checksum Compute(Stream stream, HashAlgorithmName algorithmName)
        {
            using var algorithm = CreateAlgorithm(algorithmName);

            var checksum = algorithm.ComputeHash(stream);

            return new Checksum(checksum, algorithmName);
        }

        public static Checksum Compute(byte[] bytes, HashAlgorithmName algorithmName)
        {
            using var algorithm = CreateAlgorithm(algorithmName);

            var checksum = algorithm.ComputeHash(bytes);

            return new Checksum(checksum, algorithmName);
        }

        public static async Task<Checksum> ComputeAsync(Stream stream, HashAlgorithmName algorithmName, CancellationToken cancellationToken = default)
        {
            using var algorithm = CreateAlgorithm(algorithmName);

            var checksum = await algorithm.ComputeHashAsync(stream, cancellationToken);

            return new Checksum(checksum, algorithmName);
        }

        private static HashAlgorithm CreateAlgorithm(HashAlgorithmName algorithmName)
        {
            if (!FactoryMap.TryGetValue(algorithmName, out var factory))
            {
                throw new KeyNotFoundException($"Invalid hash algorithm {algorithmName}");
            }

            return factory();
        }

        public bool Equals(Checksum? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return BytesEqual(value, other.value);
        }

        public override bool Equals(object? obj) => Equals(obj as Checksum);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            hashCode.Add(algorithmName);
            hashCode.AddBytes(value);

            return hashCode.ToHashCode();
        }

        public byte[] ToByteArray()
        {
            var copy = new byte[value.Length];

            value.CopyTo(copy, 0);

            return copy;
        }

        public Base64String ToBase64() => Base64String.Encode(value);

        /// <summary>
        /// Formats the checksum into a base64 string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToString("G", null);

        /// <summary>
        /// <inheritdoc cref="ToString(string?, IFormatProvider?)"/>
        /// </summary>
        /// <param name="format">
        /// <inheritdoc cref="ToString(string?, IFormatProvider?)"/>
        /// </param>
        /// <returns></returns>
        public string ToString(string? format) => ToString(format, null);

        /// <summary>
        /// Formats the checksum into a string using the provided <paramref name="format"/> specifier.
        /// </summary>
        /// <param name="format">
        /// The format specifier that will be used to format the string.
        /// 
        /// 'g' or a null format string will return the value base64 encoded.
        /// 'u' will return it base64url encoded.
        /// 'x' will return a string with the hexadecimal values of the checksum's bytes.
        /// 
        /// Capitalizing the letter will prepend the algorithm name to the output string.
        /// </param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            format ??= "g"; // Default format

            var output = format.ToLower() switch
            {
                "G" => ToBase64().ToString(),
                "U" => ToBase64().ToUrlSafeString(),
                "X" => ToByteString(formatProvider),
                _   => throw new FormatException($"Invalid format string '{format}")
            };

            return char.IsUpper(format[0]) ? $"{algorithmName} {output}" : output;
        }

        /// <summary>
        /// Converts the checksum value to a string of all of its bytes,
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        private string ToByteString(IFormatProvider? formatProvider)
        {
            var numberFormat = formatProvider?.GetFormat<NumberFormatInfo>();
            var buffer = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                buffer.Append(value[i].ToString("X2", numberFormat));
            }

            return buffer.ToString();
        }

        private static bool BytesEqual(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y) => x.Length == y.Length && x.SequenceEqual(y);
    }
}
