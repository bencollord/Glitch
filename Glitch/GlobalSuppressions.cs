// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Primary constructors are not mandatory and should not cause noise from the compiler")]

// And so it begins...
[assembly: SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "May change semantics and should not cause noise from the compiler")]

// Seriously, why are there multiple warnings for this? I shouldn't have to say it twice
[assembly: SuppressMessage("Style", "IDE0306:Simplify collection initialization", Justification = "May change semantics and should not cause noise from the compiler")]

// SO. FREAKING. PUSHY.
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "May change semantics and should not cause noise from the compiler")]

// This is borderline comical.
[assembly: SuppressMessage("Style", "IDE0301:Simplify collection initialization", Justification = "May change semantics and should not cause noise from the compiler")]

// Now it's not even borderline.
[assembly: SuppressMessage("Style", "IDE0303:Simplify collection initialization", Justification = "May change semantics and should not cause noise from the compiler")]
