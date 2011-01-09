namespace Endjin.Templify.Specifications.Domain.Infrastructure
{
    #region Using Directives

    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Infrastructure;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;

    #endregion

    [Subject(typeof(Configuration))]
    public class when_configuration_is_asked_to_get_file_exclusion : Specification<IConfiguration, Configuration>
    {
        static string result;

        Because of = () => result = subject.GetFileExclusions();

        It should_return_create_mode; //= () => result.ShouldNotBeEmpty();
    }
}