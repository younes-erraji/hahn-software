﻿namespace AtoZ.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException() : base()
    {
    }

    public AuthenticationException(string message) : base(message)
    {
    }
}
