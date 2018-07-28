using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Domain.Exceptions;
using Example.Domain.Rules.Authors;
using Example.Domain.Validations;
using NRules;
using NRules.Fluent;

namespace Example.Business
{
    public class RuleService
    {
        protected ISession RulesSession { get; set; }

        public List<BusinessValidationError> FactsList { private set; get; }

        public RuleService()
        {
            //Load rules
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(AuthorsLastnameCantContainL).Assembly));

            //Compile rules
            var factory = repository.Compile();

            //Create a working session
            this.RulesSession = factory.CreateSession();
        }

        protected void RunRulesSession(bool isErrorThrowing)
        { 
            //Start match/resolve/act cycle
            this.RulesSession.Fire();

            this.FactsList = this.RulesSession.Query<BusinessValidationError>().ToList();

            if (this.FactsList.Count > 0)
            { 
                throw new ValidationException(this.FactsList);
            }
        }

    }
}
