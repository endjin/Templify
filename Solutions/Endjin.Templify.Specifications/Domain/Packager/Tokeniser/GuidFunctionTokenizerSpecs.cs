namespace Endjin.Templify.Specifications.Domain.Packager.Tokeniser {
	using System;
	using System.Collections.Generic;
	using Endjin.Templify.Domain.Domain.Packager.Tokeniser;
	using NUnit.Framework;

	[TestFixture]
	public class GuidFunctionTokenizerSpecs {

		[TestCase( "", "" )]
		[TestCase( (string)null, (string)null )]
		[TestCase( "__GUID()__", "00000000-0000-0000-0000-000000000000" )]
		[TestCase( "abc__GUID()__def", "abc00000000-0000-0000-0000-000000000000def" )]
		[TestCase( "abc__GUID(__def", "abc__GUID(__def" )]
		[TestCase( "abc__GUID()__def__GUID()__ghi", "abc00000000-0000-0000-0000-000000000000def00000000-0000-0000-0000-000000000000ghi" )]
		[TestCase( "abc\n__GUID()__\ndef\n__GUID()__\nghi", "abc\n00000000-0000-0000-0000-000000000000\ndef\n00000000-0000-0000-0000-000000000000\nghi" )]
		public void ReplacementExpressions_Work( string Content, string Expected ) {

			// Arrange
			MockGuidFactory mock = new MockGuidFactory( Guid.Empty );
			GuidFunctionTokenizer g = new GuidFunctionTokenizer( mock );

			// Act
			string actual = g.TokenizeContent( Content );

			// Assert
			Assert.That( actual, Is.EqualTo( Expected ) );
		}

		[Test]
		public void EachTokenGetsUniqueGuid() {

			// Arrange
			List<Guid> guids = new List<Guid>() {
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid()
			};
			string content = "__GUID()__ __GUID()__ __GUID()__";
			string expected = string.Format( "{0} {1} {2}", guids[0], guids[1], guids[2] );
			MockGuidListFactory mock = new MockGuidListFactory( guids );
			GuidFunctionTokenizer g = new GuidFunctionTokenizer( mock );

			// Act
			string actual = g.TokenizeContent( content );

			// Assert
			Assert.That( actual, Is.EqualTo( expected ) );
		}

		[TestCase(  "" )]
		[TestCase( "N" )]
		[TestCase( "D" )]
		[TestCase( "B" )]
		[TestCase( "P" )]
		[TestCase( "X" )]
		public void Format_Works( string Format ) {

			// Arrange
			string content = "__GUID(" + Format + ")__";
			MockGuidFactory mock = new MockGuidFactory( Guid.NewGuid() );
			GuidFunctionTokenizer g = new GuidFunctionTokenizer( mock );

			// Act
			string actual = g.TokenizeContent( content );

			// Assert
			string expected = mock.NewGuid().ToString( Format );
			Assert.That( actual, Is.EqualTo( expected ) );
		}


		public class MockGuidFactory : IGuidFactory {
			private readonly Guid guid;

			public MockGuidFactory( Guid Guid ) {
				this.guid = Guid;
			}

			public Guid NewGuid() {
				return guid;
			}
		}

		public class MockGuidListFactory : IGuidFactory {
			private readonly List<Guid> guids;
			private int step = -1;

			public MockGuidListFactory( List<Guid> Guids ) {
				this.guids = Guids;
			}

			public Guid NewGuid() {
				// The next one
				step++;
				Guid g = guids.Count > step ? guids[step] : Guid.Empty;
				return g;
			}
		}

	}
}