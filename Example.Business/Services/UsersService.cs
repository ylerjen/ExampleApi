using System;
using System.Collections.Generic;
using System.Linq;
using Example.Domain.Entities;
using Example.Domain.Rules.Users;
using Example.Domain.Validations;
using Example.Helpers;
using Example.Repository.Repositories;
using NRules;
using NRules.Fluent;

namespace Example.Business.Services
{
    public class UsersService : RuleService, IUsersService
    {
        private IUsersRepository UsersRepository { get; }

        private IDateTimeProvider DateTimeProvider { get; }

        public UsersService(IDateTimeProvider dateTimeProvider, IUsersRepository usersRepository)
        {
            this.DateTimeProvider = dateTimeProvider;
            this.UsersRepository = usersRepository;
        }

        public bool UserExists(Guid id)
        {
            return this.UsersRepository.UserExists(id);
        }

        public User GetUserById(Guid id)
        {
            return this.UsersRepository.GetUserById(id);
        }

        public List<User> GetUsersList()
        {
            return this.UsersRepository.GetUsersList();
        }

        public User CreateUser(User user)
        {
            //Insert facts into rules engine's memory
            this.RulesSession.Insert(user);
            this.RunRulesSession(true);

            return this.UsersRepository.InsertUser(user);
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void ModifyUser(string action, string prop, object newValue)
        {
            throw new NotImplementedException();
        }
    }
}
