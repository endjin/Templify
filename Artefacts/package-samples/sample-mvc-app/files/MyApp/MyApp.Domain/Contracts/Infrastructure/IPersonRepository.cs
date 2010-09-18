namespace MyApp.Domain.Contracts.Infrastructure
{
    public interface IPersonRepository
    {
        void Save(Person person);
    }
}