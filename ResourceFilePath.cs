using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> File path relative to Resources folder </summary>
	public struct ResourceFilePath : IFile
	{
		public readonly string Value;
		public ResourceFilePath ( string str )
		{
			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(FileName)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(FileName)} starts with '\\' character" );
			#endif

			this.Value = str;
		}
		string IFile.Path => this.Value;
		string IFile.AbsPath => throw new System.NotImplementedException("implement me");
		public static implicit operator ResourceFilePath ( string path ) => new ResourceFilePath( path );
		public static implicit operator string ( ResourceFilePath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}
}
