namespace Data.Services.Rules
{
    public class RuleFactory : IRuleFactory
    {
        private readonly IRule firstRule;

        public RuleFactory()
        {
            this.firstRule = new ReprovadoRule();

            this.firstRule
                .SetNext(new AprovadoRule())
                .SetNext(new ItemsAprovadosAMaiorRule())
                .SetNext(new ItemsAprovadosAMenorRule())
                .SetNext(new ValorAprovadoAMaiorRule())
                .SetNext(new ValorAprovadoAMenorRule());
        }

        public IRule GetFirstRule()
        {
            return this.firstRule;
        }
    }
}
