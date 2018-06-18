using System;

namespace ExampleDomain.Validations
{
    public class ValidationException : Exception
    {
        public string Field { get; set; }

        public ValidationException(string msg, string fieldname): base(msg)
        {
            this.Field = fieldname;
        }
    }
}
