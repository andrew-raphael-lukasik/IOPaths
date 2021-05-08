using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> Directory path relative to Resources folder </summary>
	public struct ResourceDirectoryPath
	{

		public readonly string value;


		public ResourceDirectoryPath ( string str )
		{
			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(ResourceDirectoryPath)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(ResourceDirectoryPath)} starts with '\\' character" );
			#endif

			this.value = str;
		}


		override public string ToString () => this.value;
		public ResourceFilePath Combine ( FileName fileName )
		{
			#if DEBUG
			try {
			#endif

			return new ResourceFilePath(
					!string.IsNullOrEmpty( this.value )
				?	IO.Path.Combine( this.value , fileName )
				:	(string) fileName
			);

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(ResourceFilePath)}: {this.value}, {nameof(fileName)}: {fileName}");
				throw;
			}
			#endif
		}
		public ResourceDirectoryPath Combine ( DirectoryName directoryName )
		{
			#if DEBUG
			try {
			#endif

			return new ResourceDirectoryPath(
					!string.IsNullOrEmpty( this.value )
				?	IO.Path.Combine( this.value , directoryName )
				:	(string) directoryName
			);

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(ResourceDirectoryPath)}: {this.value}, {nameof(directoryName)}: {directoryName}");
				throw;
			}
			#endif
		}


		public static implicit operator ResourceDirectoryPath ( string path ) => new ResourceDirectoryPath( path );
		public static implicit operator string ( ResourceDirectoryPath structure ) => structure.value;


	}
}
