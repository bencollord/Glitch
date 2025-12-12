Collection Extensions
=====================

Extensions to collections that don't fit will with Linq.

There are separate namespaces for Standard (generic),
ReadOnly, and Immutable collections. This is so that they can be imported
separately. Unfortunately, most of the standard collection interfaces don't 
share a common interface, but the standard implementations implement them all anyway,
so there are a few instances, in particular with any extensions to `IDictionary<K, V>`
and `IReadOnlyDictionary<K, V>` where the extension needs to be added to both interfaces,
which will lead to "call is ambiguous" errors when used withc `Dictionary<K, V>`, 
which implements both.