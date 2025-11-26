using Glitch.IO;

namespace Glitch.Functional.Effects;

// UNDONE Bring this back when it's safe to reference Glitch again.
public static partial class IO
{
    public static IO<FileStream> OpenRead(FileInfo file) => Use(f => file.OpenRead());
    public static IO<FileStream> OpenRead(FilePath path) => Return(new FileInfo(path)).AndThen(OpenRead);

    public static IO<FileStream> OpenWrite(FileInfo file) => Use(f => file.OpenWrite());
    public static IO<FileStream> OpenWrite(FilePath path) => Return(new FileInfo(path)).AndThen(OpenWrite);

    public static IO<StreamReader> OpenText(FileInfo file) => Use(f => file.OpenText());
    public static IO<StreamReader> OpenText(FilePath path) => Return(new FileInfo(path)).AndThen(OpenText);

    public static IO<StreamWriter> CreateText(FileInfo file) => Use(f => file.CreateText());
    public static IO<StreamWriter> CreateText(FilePath path) => Return(new FileInfo(path)).AndThen(CreateText);

    public static IO<StreamWriter> AppendText(FileInfo file) => Use(f => file.AppendText());
    public static IO<StreamWriter> AppendText(FilePath path) => Return(new FileInfo(path)).AndThen(AppendText);

    public static IO<string> ReadAllText(FileInfo file) => Lift(f => file.ReadAllText());
    public static IO<string> ReadAllText(FilePath path) => Return(new FileInfo(path)).AndThen(ReadAllText);

    public static Func<string, IO<Unit>> WriteAllText(FileInfo file) => (string text) => Lift(f => file.WriteAllText(text));
    public static Func<string, IO<Unit>> WriteAllText(FilePath path) => WriteAllText(new FileInfo(path));
}