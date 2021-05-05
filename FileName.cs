using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> File name only. Includes extension </summary>
	public struct FileName
	{
		public readonly string Value;
		public FileName ( string text )
		{
			#if DEBUG
			Assert.IsFalse( text.Contains("/") , $"{nameof(FileName)} contains '/' character" );
			Assert.IsFalse( text.Contains("\\") , $"{nameof(FileName)} contains '\\' character" );
			#endif

			this.Value = text;
		}
		public static implicit operator FileName ( string text ) => new FileName( text );
		public static implicit operator string ( FileName fileName ) => fileName.Value;
		override public string ToString () => this.Value;
	}
}
