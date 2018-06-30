using System;

namespace Example.Helpers
{
    public class DateTimeProvider :  IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
