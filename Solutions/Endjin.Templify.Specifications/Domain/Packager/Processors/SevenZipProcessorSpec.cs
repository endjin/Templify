namespace Endjin.Templify.Specifications.Domain.Packager.Filters
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Contracts.Packager.Filters;
    using Endjin.Templify.Domain.Domain.Packager.Filters;
    using Endjin.Templify.Domain.Domain.Packages;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;

    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_binary_file_filter : Specification<BinaryFileFilter>
    {
        protected static List<ManifestFile> manifest_files;
        protected static List<string> files;
        protected static string excluded_files;

        protected static IBinaryFileFilter subject;

        protected static IConfiguration config;

        Establish context = () =>
            {
                config = DependencyOf<IConfiguration>();

                excluded_files = ".cab;.dll;.doc;.docx;.exe;.gif;.ico;.jpg;.nupkg;.pdf;.png;.snk;.xls;.xlsx;.zip";

                manifest_files = new List<ManifestFile>
                    {
                        new ManifestFile { File = @"C:\MyApp\sample.cab", },
                        new ManifestFile { File = @"C:\MyApp\sample.dll", },
                        new ManifestFile { File = @"C:\MyApp\sample.doc", },
                        new ManifestFile { File = @"C:\MyApp\sample.docx", },
                        new ManifestFile { File = @"C:\MyApp\sample.exe", },
                        new ManifestFile { File = @"C:\MyApp\sample.gif", },
                        new ManifestFile { File = @"C:\MyApp\sample.ico", },
                        new ManifestFile { File = @"C:\MyApp\sample.jpg", },
                        new ManifestFile { File = @"C:\MyApp\sample.pdf", },
                        new ManifestFile { File = @"C:\MyApp\sample.png", },
                        new ManifestFile { File = @"C:\MyApp\sample.snk", },
                        new ManifestFile { File = @"C:\MyApp\sample.xls", },
                        new ManifestFile { File = @"C:\MyApp\sample.xlsx", },
                        new ManifestFile { File = @"C:\MyApp\sample.zip", },
                        new ManifestFile { File = @"C:\MyApp\sample.nupkg", },
                        new ManifestFile { File = @"C:\MyApp\sample.cs", },
                        new ManifestFile { File = @"C:\MyApp\sample.txt", },
                        new ManifestFile { File = @"C:\MyApp\sample.xml", },
                    };

                config.Stub(c => c.GetTokeniseFileExclusions()).Return(excluded_files);

                subject = new BinaryFileFilter(config);
                files = new List<string>
                    {
                        @"C:\MyApp\sample.cab",
                        @"C:\MyApp\sample.dll",
                        @"C:\MyApp\sample.doc",
                        @"C:\MyApp\sample.docx",
                        @"C:\MyApp\sample.exe", 
                        @"C:\MyApp\sample.gif", 
                        @"C:\MyApp\sample.ico", 
                        @"C:\MyApp\sample.jpg", 
                        @"C:\MyApp\sample.pdf", 
                        @"C:\MyApp\sample.png", 
                        @"C:\MyApp\sample.snk", 
                        @"C:\MyApp\sample.xls", 
                        @"C:\MyApp\sample.xlsx",
                        @"C:\MyApp\sample.zip", 
                        @"C:\MyApp\sample.nupkg",
                        @"C:\MyApp\sample.cs", 
                        @"C:\MyApp\sample.txt",
                        @"C:\MyApp\sample.xml",
                    };
            };
    }

    [Subject(typeof(BinaryFileFilter))]
    public class when_the_binary_file_filter_is_given_a_list_of_manifest_files_which_contains_only_three_valid_files_to_filter : specification_for_binary_file_filter
    {
        static IEnumerable<ManifestFile> result;

        Because of = () => result = subject.Filter(manifest_files);

        It should_return_three_file = () => result.Count().ShouldEqual(3);
    }

    [Subject(typeof(BinaryFileFilter))]
    public class when_the_binary_file_filter_is_given_a_list_of_files_which_contains_only_three_valid_files_to_filter : specification_for_binary_file_filter
    {
        static IEnumerable<string> result;

        Because of = () => result = subject.Filter(files);

        It should_return_three_file = () => result.Count().ShouldEqual(3);
    }
}