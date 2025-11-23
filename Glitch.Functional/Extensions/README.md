Extensions
==========
Additional functionality for monadic types on an opt-in basis

They include things such as natural transformations between monads
(e.g. OkayOrNone(), which transforms a Result into an Option and OkayOr(Error) transforms from an Option to a Result)
and extensions to collection types such as IEnumerable.FirstOrNone() and overloads of TryGet* methods that return Options.

Modules
-------
Subdirectories in this folder are designed to work sort of like importable modules. Groups of certain functionality
on monadic types that aren't inherent to the type, such as executing impure functions with side-effects or 
enabling monadic traversals over IEnumerable, can be opted into or out of by simply adding or removing a using statement.

Currently, impure actions and traverse are modules, but some of the wilder custom operators for Map, Apply, and Bind may be
moved here as well.