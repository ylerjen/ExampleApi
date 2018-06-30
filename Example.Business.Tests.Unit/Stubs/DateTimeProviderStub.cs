using System;
using Example.Helpers;

namespace Example.Business.Tests.Unit.Stubs
{
    public class DateTimeProviderStub : IDateTimeProvider
    {
        public DateTime Now()
        {
            return new DateTime(2018, 12, 31, 23, 59, 59);
        }
    }
}
