using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> Directory name only.</summary>
	public struct DirectoryName
	{

		public readonly string value;


		public DirectoryName ( string text )
		{
			#if DEBUG
			// @TODO: assertions
			#endif

			this.value = text;
		}


		override public string ToString () => this.value;
		

		public static implicit operator DirectoryName ( string text ) => new DirectoryName( text );
		public static implicit operator string ( DirectoryName directoryName ) => directoryName.value;


	}
}
