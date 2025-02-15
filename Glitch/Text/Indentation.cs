using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Text
{
    public readonly record struct Indentation
    {
        public static readonly Indentation Empty = new Indentation(string.Empty);

        public static readonly Indentation Spaces = new Indentation(' ', 4);

        public static readonly Indentation HalfSpaces = new Indentation(' ', 2);
        
        public static readonly Indentation Tabs = new Indentation('\t', 1);

        private readonly string text;
        private readonly int level;

        public Indentation(char c, int count)
            : this(c, count, 0) { }

        public Indentation(char c, int count, int level)
            : this(new string(c, count), level) { }

        public Indentation(string text)
            : this(text, 0) { }

        public Indentation(string text, int level)
        {
            this.text = text;
            this.level = level;
        }

        public int Level
        {
            get => level;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Cannot have indent less than zero");
                }

                level = value;
            }
        }

        public Indentation Replace(string text)
            => new Indentation(text, Level);

        public override string ToString() => Enumerable.Repeat(text, Level).Join();

        public static string operator +(Indentation indent, string text)
            => indent.ToString() + text;

        public static Indentation operator +(Indentation indent, int level)
            => indent with { Level = indent.Level + level };

        public static Indentation operator -(Indentation indent, int level)
            => indent with { Level = indent.Level - level };

        public static Indentation operator ++(Indentation indent)
            => indent + 1;

        public static Indentation operator --(Indentation indent)
            => indent - 1;
    }
}
