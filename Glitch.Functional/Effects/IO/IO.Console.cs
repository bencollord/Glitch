using Glitch.Functional;

namespace Glitch.Functional.Effects;

public static partial class IO
{
    public static IO<TextReader> StdIn => Return(Console.In);
    public static IO<TextWriter> StdOut => Return(Console.Out);
    public static IO<TextWriter> StdErr => Return(Console.Error);

    public static IO<DateTime> Now => Lift(_ => DateTime.Now);
    public static IO<DateTimeOffset> LocalNow => Lift(_ => DateTimeOffset.Now);
    public static IO<DateTimeOffset> UtcNow => Lift(_ => DateTimeOffset.UtcNow);

    public static IO<ConsoleKeyInfo> ReadKey() => Lift(_ => Console.ReadKey());
    public static IO<Option<string>> ReadLine() => Lift(_ => Option.Maybe(Console.ReadLine()));
    public static IO<Unit> Write(string text) => Lift(_ => Console.Write(text));
    public static IO<Unit> WriteLine() => Lift(_ => Console.WriteLine());
    public static IO<Unit> WriteLine(string text) => Lift(_ => Console.WriteLine(text));
    public static IO<Unit> Clear() => Lift(_ => Console.Clear());
}