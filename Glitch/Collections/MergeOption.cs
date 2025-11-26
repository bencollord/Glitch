namespace Glitch.Collections;

/// <summary>
    /// Specifies the behavior for duplicate keys
    /// when merging dictionaries.
    /// </summary>
public enum MergeOption
{
    /// <summary>
        /// Ignores the conflicting entry 
        /// and keeps the existing one.
        /// </summary>
    Ignore,

    /// <summary>
        /// Overwrites the existing entry with the
        /// one found in the new dictionary
        /// </summary>
    Overwrite,

    /// <summary>
        /// Throws an exception when finding a duplicate entry.
        /// </summary>
    Strict
}
