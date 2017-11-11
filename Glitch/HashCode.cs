using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public struct HashCode : IEquatable<HashCode>
    {
        private int value;

        private HashCode(int seed)
        {
            value = seed;
        }

        public static HashCode CreateFor<T>(T obj) => new HashCode(obj?.GetHashCode() ?? 0);

        public HashCode CombineWith(int hash) => new HashCode(Compute(value, hash));

        public HashCode CombineWith<T>(T obj) => CombineWith(obj?.GetHashCode() ?? 0);

        public HashCode CombineAll<T>(IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                return this;
            }

            int hash = value;

            foreach (T item in sequence)
            {
                hash = Compute(hash, item?.GetHashCode() ?? 0);
            }

            return new HashCode(hash);
        }

        public bool Equals(HashCode other) => this.value == other.value;

        public override bool Equals(object obj)
        {
            if (obj is HashCode)
            {
                return Equals((HashCode)obj);
            }
            if (obj is int)
            {
                return (int)obj == value;
            }

            return false;
        }

        public override int GetHashCode() => value;

        public static implicit operator int(HashCode code) => code.value;

        public static bool operator ==(HashCode x, HashCode y) => x.Equals(y);

        public static bool operator !=(HashCode x, HashCode y) => !x.Equals(y);

        private int Compute(int hash, int next) => unchecked(((hash << 5) + hash) ^ next);

        // TODO figure out a way to make algorithms interchangeable.
        private int DotNetTupleHash(params int[] values)
        {
            int hashCode = 0;

            unchecked
            {
                foreach (int value in values)
                {
                    hashCode = ((hashCode << 5) + hashCode) ^ value;
                }

                return hashCode;
            }
        }

        private int FnvHash(params int[] values)
        {
            const uint FnvOffsetBasis = 2166136261;
            const uint FnvPrime = 16777619;

            uint hashCode = FnvOffsetBasis;

            unchecked
            {
                foreach (int value in values)
                {
                    hashCode ^= (uint)value;
                    hashCode *= FnvPrime;
                }

                return (int)hashCode;
            }
        }

        private int JoshBlochJavaHash(params int[] values)
        {
            const int Seed = 59617;
            const int Prime = 35677;

            int hashCode = Seed;

            unchecked
            {
                foreach (int value in values)
                {
                    hashCode *= Prime;
                    hashCode += value;
                }

                return hashCode;
            }
        }
    }
}
