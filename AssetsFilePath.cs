using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> File path relative to Assets folder </summary>
	public struct AssetsFilePath : IFile
	{
		public readonly string Value;
		public AssetsFilePath ( string str )
		{
			const string k_assetsPathStart = "Assets/";
			string right ( string value , int length ) => value!=null && value.Length>length ? value.Substring( value.Length-length ) : value;

			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(FileName)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(FileName)} starts with '\\' character" );
			Assert.IsTrue( str.Contains(k_assetsPathStart) , $"\tSTRING '{str}' DOES NOT CONTAIN '{k_assetsPathStart}'" );
			#endif

			if( !str.StartsWith(k_assetsPathStart) )
				this.Value = right( str , str.Length - str.IndexOf(k_assetsPathStart) );
			else
				this.Value = str;
		}
		string IFile.Path => this.Value;
		string IFile.AbsPath => throw new System.NotImplementedException("implement me");
		public static implicit operator AssetsFilePath ( AbsoluteFilePath path ) => new AssetsFilePath(path);
		public static implicit operator AssetsFilePath ( string str ) => new AssetsFilePath(str);
		public static implicit operator string ( AssetsFilePath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}
}
