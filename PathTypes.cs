﻿using IO = System.IO;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace PathTypes
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
		// public bool Exists () => IO.Directory.Exists(this.Value);
		public void Create () => IO.Directory.CreateDirectory(this.Value);
		public static implicit operator AbsoluteDirectoryPath ( string path ) => new AbsoluteDirectoryPath( path );
		public static implicit operator string ( AbsoluteDirectoryPath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}

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
		public static implicit operator ResourceFilePath ( string path ) => new ResourceFilePath( path );
		public static implicit operator string ( ResourceFilePath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}

	// <summary> Directory path relative to Resources folder </summary>
	public struct ResourceDirectoryPath
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
		public static implicit operator ResourceDirectoryPath ( string path ) => new ResourceDirectoryPath( path );
		public static implicit operator string ( ResourceDirectoryPath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}

	// <summary> File path relative to Assets folder </summary>
	public struct RelativeAssetFilePath : IFile
	{
		public readonly string Value;
		public RelativeAssetFilePath ( string str )
		{
			const string k_assetsPathStart = "Assets/";

			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(FileName)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(FileName)} starts with '\\' character" );
			Assert.IsTrue( str.Contains(k_assetsPathStart) , $"\tSTRING '{str}' DOES NOT CONTAIN '{k_assetsPathStart}'" );
			#endif

			if( !str.StartsWith(k_assetsPathStart) )
				this.Value = str.Right( str.Length - str.IndexOf(k_assetsPathStart) );
			else
				this.Value = str;
		}
		string IFile.Path => this.Value;
		public static implicit operator RelativeAssetFilePath ( AbsoluteFilePath path ) => new RelativeAssetFilePath(path);
		public static implicit operator RelativeAssetFilePath ( string str ) => new RelativeAssetFilePath(str);
		public static implicit operator string ( RelativeAssetFilePath structure ) => structure.Value;
		override public string ToString () => this.Value;
	}

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



	public interface IDirectory { string Path { get; } }
	public interface IFile { string Path { get; } }



	public static class LocalExtensionMethods
	{

		public static bool Exists ( this IDirectory directory )
		{
			try {
				return IO.Directory.Exists( directory.Path );
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(directory)}: {directory.Path}");
				throw;
			}
		}

		public static AbsoluteDirectoryPath[] GetDirectories ( this IDirectory directory )
		{
			try {
				var arr = IO.Directory.GetDirectories( directory.Path );
				AbsoluteDirectoryPath[] results = new AbsoluteDirectoryPath[ arr.Length ];
				for( int i=0 ; i<arr.Length ; i++ ) results[i] = arr[i];
				return results;
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(directory)}: {directory.Path}");
				throw;
			}
		}

		public static AbsoluteFilePath Combine ( this IDirectory dir , FileName fileName )
		{
			try {
				return new AbsoluteFilePath(
					IO.Path.Combine( dir.Path , fileName )
				);
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(dir)}: {dir.Path}, {nameof(fileName)}: {fileName}");
				throw;
			}
		}

		public static AbsoluteDirectoryPath Combine ( this IDirectory dir , DirectoryName directoryName )
		{
			try {
				return new AbsoluteDirectoryPath(
					IO.Path.Combine( dir.Path , directoryName )
				);
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(dir)}: {dir.Path}, {nameof(directoryName)}: {directoryName}");
				throw;
			}
		}

		public static AbsoluteDirectoryPath GetDirectoryName ( this IFile file )
		{
			try {
				return new AbsoluteDirectoryPath(
					IO.Path.GetDirectoryName( file.Path )
				);
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(file)}: {file.Path}");
				throw;
			}
		}
		
		public static FileName GetFileName ( this IFile file )
		{
			try {
				return new FileName(
					IO.Path.GetFileName( file.Path )
				);
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(file)}: {file.Path}");
				throw;
			}
		}

		public static bool Exists ( this IFile file )
		{
			try {
				return IO.File.Exists( file.Path );
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(file)}: {file.Path}");
				throw;
			}
		}

		public static void Delete ( this IFile file )
		{
			try {
				if( file.Exists() ) IO.File.Delete( file.Path );
			}
			catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(file)}: {file.Path}");
				throw;
			}
		}

	}
	
}