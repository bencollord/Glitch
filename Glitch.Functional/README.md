Glitch.Functional
=================

A functional programming library containing common types and functions for coding in a functional style.
This library is heavily inspired by Paul Louth's excellent LanguageExt library, mixed in with some inspiration
and conventions taken from Rust. It eschews some of the complexity of Louth's language hacks at the cost of some
reduced flexibility and increased duplication and adheres more to standard C# coding conventions, such as using
PascalCase for all names, prefixing (most) type parameters with T, and using method names like Select and Where
for mapping and filtering. My hope is that this will make it less of a barrier to those who do not have a background 
in Haskell programming or category theory.

Architecture
------------

The namespaces in this project are organized like packages, with Core containing some basic functions, combinators,
and the Result\<T, E\> and Option\<T\> monads. All other packages are allowed to take dependencies on Core, but
Core must not be dependent on anything else.

### Errors

The Errors package provides the Error types used for signaling expected application errors where raising an exception 
isn't appropriate, such as input validation errors, as well as the Expected\<T\> monad, which is a convenience type 
that's essentially a Result\<T, E\> with the E value constrained to derive from Error so that generic parameters don't
end up getting too messy when implementing something like railway oriented programming.

Architecturally, it exists somewhere between Core and the other packages. Since error handling is so fundamental to an application,
and the Error type provides several implicit conversion operators for convenience if you do decide to use it, I didn't want
to make this library so opinionated that one *has* to use it. That said, the other namespaces are are allowed to take 
dependencies on it.

### Monads

A full monad tutorial is outside the scope of this document, as they are notoriously hard to explain (TODO: this is a cop out because I don't want to write one right now. I'll put at least a rudimentary one in later).

All monads support Linq query syntax for at least `from` and `let` expressions, including typed range variables that translate
to calls to `Cast<T>`. Monads which have a sensible nothing value, such as Option, also support `where`.

Operators like `join`, `group`, and `orderby` are not supported since their implementation
would be borderline nonsensical for most monads.

The monads are organized with a main file containing the generic container type and instance methods and a corresponding
.Module file which contains static factory methods and static functions that make it easier to work with them using
Linq query syntax.

### Other Packages

The other packages are currently works in progress. They are as follows:

#### Extensions

// TODO

#### Effects

Provides the IEffect\<T\> interface and extension methods. Designed kind of like IEnumerable in that
the interface itself is designed to be small and easy to implement while most of the functionality
is implemented in extension methods. The intent is that one can design a type that represents a lazily
evaluated calculation of some kind and get the monadic functionality for free.

Effects can optionally have an input value, which effectively makes them an implementation of the Reader monad.

#### Rand

A monadic random number generator which allows fluently creating random data values.

#### Optics

Provides composable lenses and prisms for reading and writing from deeply nested immutable objects.

#### Parsing (Experimental)

A parser combinator library inspired by Sprache and Pidgin. A lot of it is currently untested or informally
tested in LinqPad, so it's not particularly reliable yet.

#### IO (Experimental)

Provides a monad for fluently composing computations which, when executed, will perform IO operations.
Heavily based on Paul Louth's implementation, but not quite as sophisticated yet.

#### RWS (Experimental)

Implementations of the Reader, Writer, and State monads. Currently experimental since these are some of the hardest
