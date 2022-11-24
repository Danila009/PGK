using System;

namespace PGK.Application.Common.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException(string email)
            : base($"A user with this email {email} already exists") { }
    }
}
