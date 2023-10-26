using System;

namespace CouchShopper.Data.Helpers
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {}
    }
}
