﻿using System.Net;

namespace TechLibrary.Exceptions;

public class ConflictException : TechLibraryException
{
    public ConflictException(string message) : base(message) { }

    public override List<string> GetErrorMessages()
        => [Message];

    public override HttpStatusCode GetStatusCode()
        => HttpStatusCode.Conflict;
}
