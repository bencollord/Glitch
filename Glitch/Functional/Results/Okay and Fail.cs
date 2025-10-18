using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Functional.Results
{
#pragma warning disable IDE1006 // Naming Styles: 
    
    // Okay and Fail deviate from standard naming conventions because they're intended to be used in pattern matching.

    /// <summary>
    /// Interface representing a successful result.
    /// Deviates from standard naming conventions so that
    /// it can be used in pattern matching.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Okay<T>
    {
        T Value { get; }

        void Deconstruct(out T value);
    }

    /// <summary>
    /// Interface representing a failed result.
    /// Deviates from standard naming conventions so that
    /// it can be used in pattern matching.
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public interface Fail<E>
    {
        E Error { get; }

        void Deconstruct(out E error);
    }
#pragma warning restore IDE1006 // Naming Styles
}
