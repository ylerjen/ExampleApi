using System.Collections.Generic;
using Example.Domain.Codes;
using Example.Domain.Entities;
using Example.Domain.Validations;
using NRules.RuleModel;

namespace Example.Domain.Rules.Users
{
    public class UserShouldBeBornIn1984Rule : UsersRule
    {
        public override void Define()
        {
            User user = null;

            this.
                When()
                .Match<User>(
                    () => user,
                    a => a.Birthdate.Year != 1984
                );

            this.
                Then()
                .Do(ctx => this.AddValidationError(ctx));
        }

        public void AddValidationError(IContext ctx)
        {
            var validationsError = new BusinessValidationError()
            {
                Error = "User birth year should be 1984",
                ErrorCode = BusinessErrorCodes.InvalidBirthYear,
                Fields = new List<string> {nameof(User.Birthdate)}
            };

            ctx.Insert(validationsError);
        }
    }
}
