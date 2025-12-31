namespace Glitch.Functional.Inference;

public static class Infer
{
    public static InferenceContext<T> Type<T>() => new();
    public static InferenceContext<T> TypeOf<T>(T _) => new();
    public static InferenceContext<T> ElementTypeOf<T>(IEnumerable<T> _) => new();
    public static InferenceContext<T> ValueTypeOf<T>(Option<T> _) => new();
    public static InferenceContext<T> ValueTypeOf<T, E>(IResult<T, E> _) => new();
    public static InferenceContext<T> ErrorTypeOf<T, E>(IResult<T, E> _) => new();
}
