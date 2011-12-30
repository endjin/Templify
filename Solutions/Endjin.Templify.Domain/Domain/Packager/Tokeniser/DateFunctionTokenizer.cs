namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser {
	using System;
	using System.ComponentModel.Composition;
	using System.Text.RegularExpressions;
	using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;

	[Export( typeof(IFunctionTokenizer) )]
	public class DateFunctionTokenizer : IFunctionTokenizer {
		private readonly IDateTimeNowFactory dateTimeNowFactory;
		private readonly Regex dateRegex;

		[ImportingConstructor]
		public DateFunctionTokenizer( IDateTimeNowFactory DateTimeNowFactory ) {
			if ( DateTimeNowFactory == null ) {
				throw new ArgumentNullException( "DateTimeNowFactory" );
			}
			this.dateTimeNowFactory = DateTimeNowFactory;
			this.dateRegex = new Regex( @"__DATE\(([^)]*)\)__", RegexOptions.Compiled );
		}

		public string TokenizeContent( string Content ) {
			string results = Content;
			if ( !string.IsNullOrWhiteSpace( Content ) ) {
				results = dateRegex.Replace(
					Content, match => {
						string format = "";
						if ( match.Groups.Count > 1 ) {
							format = match.Groups[1].Value;
						}
						if ( string.IsNullOrWhiteSpace( format ) ) {
							format = "d"; // Short date
						}
						return this.dateTimeNowFactory.Now().ToString( format );
					}
					);
			}
			return results;
		}

	}

	// For testability:

	public interface IDateTimeNowFactory {
		DateTime Now();
	}

	[Export( typeof(IDateTimeNowFactory) )]
	public class DateTimeNowFactory : IDateTimeNowFactory {
		public DateTime Now() {
			return DateTime.Now;
		}
	}

}