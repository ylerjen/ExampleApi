using System;
using System.Collections.Generic;
using Example.Domain.Validations;

namespace Example.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public List<BusinessValidationError> BusinessValidationErrors { get; set; }

        public ValidationException(List<BusinessValidationError> errors) : base("Business Validation errors occured")
        {
            this.BusinessValidationErrors = errors;
        }
    }
}