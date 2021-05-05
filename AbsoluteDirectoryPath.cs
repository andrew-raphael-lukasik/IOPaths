using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	public struct AbsoluteDirectoryPath : IDirectory
	{
		public readonly string Value;
		public AbsoluteDirectoryPath ( string str )
		{
			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(FileName)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(FileName)} starts with '\\' character" );
			#endif

			this.Value = str;
		}
		string IDirectory.Path => this.Value;
		string IDirectory.AbsPath => this.Value;
		public void Create () => IO.Directory.CreateDirectory(this.Value);
		public static implicit operator AbsoluteDirectoryPath ( string path ) => new AbsoluteDirectoryPath( path );
		public static implicit operator string ( AbsoluteDirectoryPath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}
}
