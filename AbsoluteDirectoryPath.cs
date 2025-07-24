using IO = System.IO;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	public struct AbsoluteDirectoryPath
	{

		public readonly string value;


		public AbsoluteDirectoryPath ( string str )
		{
			#if DEBUG && !PLATFORM_STANDALONE_LINUX
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(AbsoluteDirectoryPath)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(AbsoluteDirectoryPath)} starts with '\\' character" );
			#endif

			this.value = str;
		}


		public override string ToString () => this.value;
		public bool Exists () => !string.IsNullOrEmpty(this.value) && IO.Directory.Exists(this.value);
		public void Create () => IO.Directory.CreateDirectory( this.value );
		public AbsoluteDirectoryPath[] GetDirectories ()
		{
			#if DEBUG
			try {
			#endif

			var arr = IO.Directory.GetDirectories( this.value );
			AbsoluteDirectoryPath[] results = new AbsoluteDirectoryPath[ arr.Length ];
			for( int i=0 ; i<arr.Length ; i++ ) results[i] = arr[i];
			return results;
			
			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteDirectoryPath)}: {this.value}");
				throw;
			}
			#endif
		}
		public AbsoluteFilePath Combine ( FileName fileName )
		{
			#if DEBUG
			try {
			#endif

			return new AbsoluteFilePath(
					!string.IsNullOrEmpty( this.value )
				?	IO.Path.Combine( this.value , fileName )
				:	(string) fileName
			);

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteDirectoryPath)}: {this.value}, {nameof(fileName)}: {fileName}");
				throw;
			}
			#endif
		}
		public AbsoluteDirectoryPath Combine ( DirectoryName directoryName )
		{
			#if DEBUG
			try {
			#endif

			return new AbsoluteDirectoryPath(
					!string.IsNullOrEmpty( this.value )
				?	IO.Path.Combine( this.value , directoryName )
				:	(string) directoryName
			);

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteDirectoryPath)}: {this.value}, {nameof(directoryName)}: {directoryName}");
				throw;
			}
			#endif
		}


		public static implicit operator AbsoluteDirectoryPath ( string path ) => new AbsoluteDirectoryPath( path );
		public static implicit operator string ( AbsoluteDirectoryPath structure ) => structure.value;


	}
}
