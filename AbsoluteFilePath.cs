using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	public struct AbsoluteFilePath : IFile
	{
		public readonly string Value;
		public AbsoluteFilePath ( string str )
		{
			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(FileName)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(FileName)} starts with '\\' character" );
			#endif

			this.Value = str;
		}
		public bool Exists () => IO.File.Exists(Value);
		public bool Contains ( string str ) => this.Value.Contains(str);
		public AbsoluteFilePath ChangeExtension ( string extension ) => new AbsoluteFilePath( IO.Path.ChangeExtension(this.Value,extension) );
		string IFile.Path => this.Value;
		string IFile.AbsPath => this.Value;
		public static implicit operator AbsoluteFilePath ( string path ) => new AbsoluteFilePath( path );
		public static implicit operator string ( AbsoluteFilePath structure ) => structure.Value;
		public static implicit operator ResourceFilePath ( AbsoluteFilePath absoluteFilePath )
		{
			string rawValue = absoluteFilePath.Value;
			int splitIndex = rawValue.LastIndexOf("Resources");
			return rawValue.Substring( splitIndex );
		}
		override public string ToString () => this.Value;
	}
}
