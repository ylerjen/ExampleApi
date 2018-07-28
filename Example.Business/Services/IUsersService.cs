using Example.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Example.Business.Services
{
    public interface IUsersService
    {
        bool UserExists(Guid id);

        User GetUserById(Guid id);

        List<User> GetUsersList();

        User CreateUser(User user);

        void DeleteUser(Guid id);

        void UpdateUser(User user);

        void ModifyUser(string action, string prop, object newValue);
    }
}
