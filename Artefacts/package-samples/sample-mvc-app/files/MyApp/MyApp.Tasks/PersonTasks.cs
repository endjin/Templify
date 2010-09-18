namespace MyApp.Tasks
{
    #region Using Directives

    using MyApp.Domain;
    using MyApp.Domain.Contracts.Tasks;
    using MyApp.Infrastructure;

    #endregion

    public class PersonTasks : IPersonTasks
    {
        public void Process(Person person)
        {
            new PersonRepository().Save(person);
        }
    }
}