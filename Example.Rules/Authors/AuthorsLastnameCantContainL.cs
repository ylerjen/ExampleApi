using System.Collections.Generic;
using Example.Domain.Codes;
using Example.Domain.Entities;
using Example.Domain.Validations;
using Example.Helpers;
using NRules.Fluent.Dsl;
using NRules.RuleModel;

namespace Example.Domain.Rules.Authors
{
    public class AuthorsLastnameCantContainL : AuthorsRule
    {
        public override void Define()
        {
            Author author = null;

            this.
                When()
                .Match<Author>(
                    () => author,
                    a => a.Lastname.Contains("L")
                );

            this.
                Then()
                .Do(ctx => this.AddValidationError(ctx));
        }

        public void AddValidationError(IContext ctx)
        {
            var validationsError = new BusinessValidationError()
            {
                Error = "Author name can't contain a L",
                Fields = new List<string> {nameof(Author.Lastname)},
                ErrorCode = BusinessErrorCodes.InvalidCharacterError
            };

            ctx.Insert(validationsError);
        }
    }
}
