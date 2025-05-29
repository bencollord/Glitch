namespace Glitch.Text
{
    public readonly record struct Indentation
    {
        public static readonly Indentation Empty = new(string.Empty);

        public static readonly Indentation Spaces = new(' ', 4);

        public static readonly Indentation HalfSpaces = new(' ', 2);
        
        public static readonly Indentation Tabs = new('\t', 1);

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
            init => level = value > 0 ? value : 0;
        }

        public Indentation Replace(string text)
            => new(text, Level);

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
