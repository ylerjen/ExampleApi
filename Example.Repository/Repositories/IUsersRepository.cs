using Example.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Example.Repository.Repositories
{
    public interface IUsersRepository
    {
        bool UserExists(Guid id);

        List<User> GetUsersList(uint skip, uint length);

        User GetUserById(Guid id);

        User InsertUser(User user);
    }
}
