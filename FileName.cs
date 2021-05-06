using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> File name + extension (optional). </summary>
	public struct FileName
	{

		public readonly string value;


		public FileName ( string text )
		{
			#if DEBUG
			Assert.IsFalse( text.Contains("/") , $"{nameof(FileName)} contains '/' character" );
			Assert.IsFalse( text.Contains("\\") , $"{nameof(FileName)} contains '\\' character" );
			#endif

			this.value = text;
		}


		override public string ToString () => this.value;


		public static implicit operator FileName ( string text ) => new FileName( text );
		public static implicit operator string ( FileName fileName ) => fileName.value;
		

	}
}
