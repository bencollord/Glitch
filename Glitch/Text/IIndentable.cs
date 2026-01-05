namespace Glitch.Text;

public interface IIndentable
{
    Indentation Indentation { get; set; }
}

public static class IndentableExtensions
{
    extension(IIndentable self)
    {
        public IndentationScope BeginScope() => new(self);
    }
}
