namespace Glitch.Functional.Parsing
{
    public readonly record struct Position(int Line, int Col, int Pos)
    {
        public static readonly Position Zero = new();
        public static readonly Position Start = new(1, 1, 1);

        public override string ToString() => $"Line: {Line}, Col: {Col}, Pos: {Pos}";

        public Position NewLine() => this with { Line = Line + 1, Col = 0 };

        public static Position operator ++(Position pos)
            => pos with { Pos = pos.Pos + 1, Col = pos.Col + 1 };
    }
}
