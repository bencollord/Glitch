namespace Glitch.Functional.Errors;

public enum GlobalErrorCode
{
    Unspecified        = 0,
    None               = -0xBAD0000,
    Aggregate          = -0xBAD0001,
    NoElements         = -0xBAD0002,
    MoreThanOneElement = -0xBAD0003,
    ParseError         = -0xBAD0004,
    Unexpected         = -0xBAD0005,
    BadUnwrap          = -0xBAD0006,
    KeyNotFound        = -0xBAD0007,
    InvalidCast        = -0xBADCA57,
}
