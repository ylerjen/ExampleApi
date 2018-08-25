using Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.Repository.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly List<User> userList = new List<User>() {
            new User(new Guid("0ec5e125-5eee-4102-b48d-011a600fd74a"), "Norris", "Chuck", new DateTime(1940, 3, 10)){ Username = "Braddock", Descr = "The real superhero" },
            new User(new Guid("8f8cfba1-b30e-4289-a7ca-3aa122adb30a"), "Wayne", "Bruce", new DateTime(1939, 3, 30)){ Username = "Batman", Descr = "Just another geek" },
            new User(new Guid("3fd45a08-c46e-45bf-8afe-00de36975748"), "Parker", "Peter", new DateTime(1969, 5, 1)){ Username = "Spiderman", Descr = "Not a menthol fan" },
            new User(new Guid("ec401dbe-56f4-4b81-8c2a-0b84873dfdb2"), "Kent", "Clark", new DateTime(1938, 6, 1)){ Username = "Superman", Descr = "Perhaps we know him, but I don't have my glasses" },
            new User(new Guid("5335b7a4-e4b3-4129-8c05-df50f0aad1e1"), "Wilson", "Wade", new DateTime(1991, 2, 4)){ Username = "Deadpool", Descr = "Well..." },
            new User(new Guid("27b3623a-8f75-40f3-9313-884ad7f7b50b"), "Allen", "Barry", new DateTime(1940, 1, 9)){ Username = "Flash", Descr = "Look, a... ! Too late..." }
        };
    
        public List<User> GetUsersList(uint skip, uint length)
        {
            return this.userList
                .Skip((int)(skip * length))
                .Take((int)length)
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
