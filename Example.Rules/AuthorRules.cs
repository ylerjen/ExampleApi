using System;
using Example.Domain.Entities;
using Example.Domain.Validations;
using Example.Helpers;
using NRules.Fluent.Dsl;
using NRules.RuleModel;

namespace Example.Rules
{
    public class AuthorRules : Rule
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public AuthorRules(IDateTimeProvider dateTimeProvider)
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
