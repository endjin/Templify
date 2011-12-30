namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser {
	using System;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;

	[Export( typeof(IFunctionTokenizer) )]
	public class GuidFunctionTokenizer : IFunctionTokenizer {
		private readonly IGuidFactory guidFactory;
		private readonly Regex guidRegex;

		[ImportingConstructor]
		public GuidFunctionTokenizer( IGuidFactory GuidFactory ) {
			if ( GuidFactory == null ) {
				throw new ArgumentNullException( "GuidFactory" );
			}
			this.guidFactory = GuidFactory;
			this.guidRegex = new Regex( @"__GUID\(([A-Za-z]?)\)__", RegexOptions.Compiled );
		}

		public string TokenizeContent( string Content ) {
			string results = Content;
			if ( !string.IsNullOrWhiteSpace( Content ) ) {
				results = guidRegex.Replace(
					Content, match => {
						string format = "";
						if ( match.Groups.Count > 1 ) {
							format = match.Groups[1].Value;
						}
						Guid guid = this.guidFactory.NewGuid();
						return guid.ToString( format ); // Valid format values: http://msdn.microsoft.com/en-us/library/97af8hh4.aspx
					}
					);
			}
			return results;
		}

	}

	// For testability:

	public interface IGuidFactory {
		Guid NewGuid();
	}

	[Export( typeof(IGuidFactory) )]
	public class GuidFactory : IGuidFactory {
		public Guid NewGuid() {
			return Guid.NewGuid();
		}
	}

}