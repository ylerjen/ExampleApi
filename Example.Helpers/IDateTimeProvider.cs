using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Helpers
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
