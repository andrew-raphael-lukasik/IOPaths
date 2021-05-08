#if UNITY_EDITOR
using IO = System.IO;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> File path relative to Assets folder </summary>
	public struct AssetsFilePath
	{


		public readonly string value;


		public AssetsFilePath ( string text )
		{
			#if DEBUG
			Assert.IsFalse( text.StartsWith("/") , $"{nameof(AssetsFilePath)} starts with '/' character" );
			Assert.IsFalse( text.StartsWith("\\") , $"{nameof(AssetsFilePath)} starts with '\\' character" );
			#endif

			this.value = text;
		}


		override public string ToString () => this.value;
		public bool Exists () => !string.IsNullOrEmpty(this.value) && ((AbsoluteFilePath)this).Exists();
		// string right ( string value , int length ) => value!=null && value.Length>length ? value.Substring( value.Length-length ) : value;
		public AbsoluteDirectoryPath GetDirectoryName ()
		{
			#if DEBUG
			try {
			#endif

			return new AbsoluteDirectoryPath( IO.Path.GetDirectoryName( this.value ) );

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AssetsFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
		
		public FileName GetFileName ()
		{
			#if DEBUG
			try {
			#endif

			return new FileName( IO.Path.GetFileName( this.value ) );

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AssetsFilePath)}: {this.value}");
				throw;
			}
			#endif
		}


		public static implicit operator AssetsFilePath ( string text ) => new AssetsFilePath(text);
		public static implicit operator AbsoluteFilePath ( AssetsFilePath obj ) => new AbsoluteFilePath( IO.Path.Combine( Application.dataPath , obj.value ) );
		public static implicit operator string ( AssetsFilePath obj ) => obj.value;


	}
}
#endif