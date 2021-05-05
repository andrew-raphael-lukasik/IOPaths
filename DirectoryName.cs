using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> Directory name only. Includes extension </summary>
	public struct DirectoryName
	{
		public readonly string Value;
		public DirectoryName ( string text )
		{
			#if DEBUG
			
			#endif

			this.Value = text;
		}
		public static implicit operator DirectoryName ( string text ) => new DirectoryName( text );
		public static implicit operator string ( DirectoryName directoryName ) => directoryName.Value;
		override public string ToString () => this.Value;
	}
}
