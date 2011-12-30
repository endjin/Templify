namespace Endjin.Templify.Specifications.Domain.Packager.Tokeniser {
	using System;
	using Endjin.Templify.Domain.Domain.Packager.Tokeniser;
	using NUnit.Framework;

	[TestFixture]
	public class DateFunctionTokenizerSpecs {

		private static readonly DateTime testDate = new DateTime( 2000, 5, 12, 2, 52, 19 ); // FRAGILE: Tests hard-code this in expected results

		[TestCase( "", "" )]
		[TestCase( (string)null, (string)null )]
		[TestCase( "__DATE()__", "5/12/2000" )]
		[TestCase( "abc__DATE()__def", "abc5/12/2000def" )]
		[TestCase( "abc__DATE(__def", "abc__DATE(__def" )]
		[TestCase( "abc__DATE()__def__DATE()__ghi", "abc5/12/2000def5/12/2000ghi" )]
		[TestCase( "abc\n__DATE()__\ndef\n__DATE()__\nghi", "abc\n5/12/2000\ndef\n5/12/2000\nghi" )]
		public void ReplacementExpressions_Work( string Content, string Expected ) {

			// Arrange
			MockDateTimeNowFactory mock = new MockDateTimeNowFactory( testDate );
			DateFunctionTokenizer d = new DateFunctionTokenizer( mock );

			// Act
			string actual = d.TokenizeContent( Content );

			// Assert
			Assert.That( actual, Is.EqualTo( Expected ) );

		}

		[TestCase( "" )]
		[TestCase( "d" )]
		[TestCase( "D" )]
		[TestCase( "t" )]
		[TestCase( "T" )]
		[TestCase( "f" )]
		[TestCase( "F" )]
		[TestCase( "dd-MM-yyyy hh:mm:ss tt" )]
		[TestCase( "yyyy" )]
		[TestCase( "ddd" )]
		public void Format_Works( string Format ) {

			// Arrange
			string content = "__DATE(" + Format + ")__";
			if ( string.IsNullOrEmpty( Format ) ) {
				Format = "d"; // FRAGILE: Duplicates business logic in class under test
			}
			MockDateTimeNowFactory mock = new MockDateTimeNowFactory( testDate );
			DateFunctionTokenizer d = new DateFunctionTokenizer( mock );

			// Act
			string actual = d.TokenizeContent( content );

			// Assert
			string expected = mock.Now().ToString( Format );
			Assert.That( actual, Is.EqualTo( expected ) );
		}


		public class MockDateTimeNowFactory : IDateTimeNowFactory {
			private readonly DateTime dateTime;

			public MockDateTimeNowFactory( DateTime DateTime ) {
				this.dateTime = DateTime;
			}

			public DateTime Now() {
				return dateTime;
			}

		}

	}
}