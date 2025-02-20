﻿using System.Net;

namespace TechLibrary.Exceptions;

public class NotFoundException : TechLibraryException
{
    public NotFoundException(string message) : base(message) { }

    public override List<string> GetErrorMessages() 
        => [Message];

    public override HttpStatusCode GetStatusCode() 
        => HttpStatusCode.NotFound;
}