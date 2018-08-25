using Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.Repository.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly List<User> userList = new List<User> {
            new User(new Guid("0ec5e125-5eee-4102-b48d-011a600fd74a"), "King", "Stephen", new DateTime()) { Descr = "Master of suspense" },
            new User(new Guid("8f8cfba1-b30e-4289-a7ca-3aa122adb30a"), "Martin", "George RR", new DateTime()) { Descr = "An American novelist and short-story writer in the fantasy, horror, and science fiction genres" }
        };

        public List<User> GetUsersList(int skip, int length)
        {
            return this.userList
                .Skip(skip * length)
                .Take(length)
                .ToList();
        }

        public User GetUserById(Guid id)
        {
            return this.userList.Find(a => a.Id == id);
        }

        public bool UserExists(Guid id)
        {
            return this.userList.Any(a => a.Id == id);
        }

        public User InsertUser(User user)
        {
            user.Id = Guid.NewGuid();
            this.userList.Add(user);
            return user;
        }
    }
}
