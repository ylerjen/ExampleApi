using Example.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Example.Business.Services
{
    public interface IUsersService
    {
        bool UserExists(Guid id);

        User GetUserById(Guid id);

        /// <summary>
        /// Retrieve a list of existing users using a paging system
        /// </summary>
        /// <param name="skip">is the number of pages we want to skip. Page length is based on the length param</param>
        /// <param name="length">is the number of item we want to return</param>
        /// <returns>The list of user found</returns>
        List<User> GetUsersList(uint skip, uint length);

        User CreateUser(User user);

        void DeleteUser(Guid id);

        void UpdateUser(User user);

        void ModifyUser(string action, string prop, object newValue);
    }
}
