﻿using System.Net;

namespace Exception.ExceptionBase;

public class ErrorOnValidationException : HackathonException
{

	private readonly List<string> _errors;

	public override int StatusCode => (int)HttpStatusCode.BadRequest;

	public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
	{
		_errors = errorMessages;
	}

	public override List<string> GetErrors()
	{
		return _errors;
	}
}