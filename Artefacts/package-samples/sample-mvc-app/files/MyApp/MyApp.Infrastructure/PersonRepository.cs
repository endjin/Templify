namespace MyApp.Infrastructure
{
    #region Using Directives

    using MyApp.Domain;
    using MyApp.Domain.Contracts.Infrastructure;
    using MyApp.Framework.Serialization;

    #endregion

    public class PersonRepository : IPersonRepository
    {
        public void Save(Person person)
        {
            Serializer.SaveInstance(person, @"c:\temp\MyApp\person.xml");
        }
    }
}
