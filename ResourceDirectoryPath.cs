using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> Directory path relative to Resources folder </summary>
	public struct ResourceDirectoryPath : IDirectory
	{
		public readonly string Value;
		public ResourceDirectoryPath ( string str )
		{
			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(FileName)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(FileName)} starts with '\\' character" );
			#endif

			this.Value = str;
		}
		string IDirectory.Path => this.Value;
		string IDirectory.AbsPath => throw new System.NotImplementedException("implement me");
		public static implicit operator ResourceDirectoryPath ( string path ) => new ResourceDirectoryPath( path );
		public static implicit operator string ( ResourceDirectoryPath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}
}
