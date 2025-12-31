using System.Linq.Expressions;

namespace Glitch.Functional.Inference;

public struct InferenceContext<T1>
{
    public InferenceContext<T1, T2> AddType<T2>() => new();
    public InferenceContext<T1, T2> AddTypeOf<T2>(T2 _) => new();
    public InferenceContext<T1, T2> AddElementTypeOf<T2>(IEnumerable<T2> _) => new();
    public InferenceContext<T1, T2> AddValueTypeOf<T2>(Option<T2> _) => new();
    public InferenceContext<T1, T2> AddValueTypeOf<T2, E>(IResult<T2, E> _) => new();
    public InferenceContext<T1, E> AddErrorTypeOf<T2, E>(IResult<T2, E> _) => new();

    public Func<T1> Func(Func<T1> func) => func;
    public Action<T1> Action(Action<T1> action) => action;
    public Expression<Func<T1>> Expression(Expression<Func<T1>> expression) => expression;
}

public struct InferenceContext<T1, T2>
{
    public InferenceContext<T1, T2, T3> AddType<T3>() => new();
    public InferenceContext<T1, T2, T3> AddTypeOf<T3>(T3 _) => new();
    public InferenceContext<T1, T2, T3> AddElementTypeOf<T3>(IEnumerable<T3> _) => new();
    public InferenceContext<T1, T2, T3> AddValueTypeOf<T3>(Option<T3> _) => new();
    public InferenceContext<T1, T2, T3> AddValueTypeOf<T3, E>(IResult<T3, E> _) => new();
    public InferenceContext<T1, T2, E> AddErrorTypeOf<T3, E>(IResult<T3, E> _) => new();

    public Func<T1, T2> Func(Func<T1, T2> func) => func;
    public Action<T1, T2> Action(Action<T1, T2> action) => action;
    public Expression<Func<T1, T2>> Expression(Expression<Func<T1, T2>> expression) => expression;
}

public struct InferenceContext<T1, T2, T3>
{
    public InferenceContext<T1, T2, T3, T4> AddType<T4>() => new();
    public InferenceContext<T1, T2, T3, T4> AddTypeOf<T4>(T4 _) => new();
    public InferenceContext<T1, T2, T3, T4> AddElementTypeOf<T4>(IEnumerable<T4> _) => new();
    public InferenceContext<T1, T2, T3, T4> AddValueTypeOf<T4>(Option<T4> _) => new();
    public InferenceContext<T1, T2, T3, T4> AddValueTypeOf<T4, E>(IResult<T4, E> _) => new();
    public InferenceContext<T1, T2, T3, E> AddErrorTypeOf<T4, E>(IResult<T4, E> _) => new();

    public Func<T1, T2, T3> Func(Func<T1, T2, T3> func) => func;
    public Action<T1, T2, T3> Action(Action<T1, T2, T3> action) => action;
    public Expression<Func<T1, T2, T3>> Expression(Expression<Func<T1, T2, T3>> expression) => expression;
}

public struct InferenceContext<T1, T2, T3, T4>
{
    public InferenceContext<T1, T2, T3, T4, T5> AddType<T5>() => new();
    public InferenceContext<T1, T2, T3, T4, T5> AddTypeOf<T5>(T5 _) => new();
    public InferenceContext<T1, T2, T3, T4, T5> AddElementTypeOf<T5>(IEnumerable<T5> _) => new();
    public InferenceContext<T1, T2, T3, T4, T5> AddValueTypeOf<T5>(Option<T5> _) => new();
    public InferenceContext<T1, T2, T3, T4, T5> AddValueTypeOf<T5, E>(IResult<T5, E> _) => new();
    public InferenceContext<T1, T2, T3, T4, E> AddErrorTypeOf<T5, E>(IResult<T5, E> _) => new();

    public Func<T1, T2, T3, T4> Func(Func<T1, T2, T3, T4> func) => func;
    public Action<T1, T2, T3, T4> Action(Action<T1, T2, T3, T4> action) => action;
    public Expression<Func<T1, T2, T3, T4>> Expression(Expression<Func<T1, T2, T3, T4>> expression) => expression;
}

public struct InferenceContext<T1, T2, T3, T4, T5>
{
    public InferenceContext<T1, T2, T3, T4, T5, T6> AddType<T6>() => new();
    public InferenceContext<T1, T2, T3, T4, T5, T6> AddTypeOf<T6>(T6 _) => new();
    public InferenceContext<T1, T2, T3, T4, T5, T6> AddElementTypeOf<T6>(IEnumerable<T6> _) => new();
    public InferenceContext<T1, T2, T3, T4, T5, T6> AddValueTypeOf<T6>(Option<T6> _) => new();
    public InferenceContext<T1, T2, T3, T4, T5, T6> AddValueTypeOf<T6, E>(IResult<T6, E> _) => new();
    public InferenceContext<T1, T2, T3, T4, T5, E> AddErrorTypeOf<T6, E>(IResult<T6, E> _) => new();

    public Func<T1, T2, T3, T4, T5> Func(Func<T1, T2, T3, T4, T5> func) => func;
    public Action<T1, T2, T3, T4, T5> Action(Action<T1, T2, T3, T4, T5> action) => action;
    public Expression<Func<T1, T2, T3, T4, T5>> Expression(Expression<Func<T1, T2, T3, T4, T5>> expression) => expression;
}

public struct InferenceContext<T1, T2, T3, T4, T5, T6>
{
    public Func<T1, T2, T3, T4, T5, T6> Func(Func<T1, T2, T3, T4, T5, T6> func) => func;
    public Action<T1, T2, T3, T4, T5, T6> Action(Action<T1, T2, T3, T4, T5, T6> action) => action;
    public Expression<Func<T1, T2, T3, T4, T5, T6>> Expression(Expression<Func<T1, T2, T3, T4, T5, T6>> expression) => expression;
}
