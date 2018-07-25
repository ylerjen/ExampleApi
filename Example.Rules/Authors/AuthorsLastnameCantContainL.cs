using Example.Domain.Entities;
using Example.Helpers;
using NRules.Fluent.Dsl;
using NRules.RuleModel;

namespace Example.Domain.Rules.Authors
{
    public class AuthorsLastnameCantContainL : AuthorsRule
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public AuthorsLastnameCantContainL(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public override void Define()
        {
            Author author = null;

            When()
                .Match<Author>(
                    () => author,
                    a => a.Lastname.Contains("L")
                );

            Then()
                .Do(ctx => this.AddValidationError(ctx));
        }

        public void AddValidationError(IContext ctx)
        {
            ctx.Insert("Author name can't contain a L");
        }
    }
}
