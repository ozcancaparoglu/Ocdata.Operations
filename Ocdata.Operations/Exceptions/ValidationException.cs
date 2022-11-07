﻿using FluentValidation.Results;

namespace Ocdata.Operations.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException()
            : base("Validation Failure", "One or more validation errors occurred")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
