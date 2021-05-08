using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	// <summary> File path relative to Resources folder </summary>
	public struct ResourceFilePath
	{

		public readonly string value;


		public ResourceFilePath ( string str )
		{
			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(ResourceFilePath)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(ResourceFilePath)} starts with '\\' character" );
			Assert.IsFalse( System.IO.Path.HasExtension(str) , $"{nameof(ResourceFilePath)} has an extension, this won't work" );
			#endif

			this.value = str;
		}


		override public string ToString () => this.value;
		public bool Exists () => !string.IsNullOrEmpty(this.value) && Resources.Load(this.value)!=null;
		public string ReadAllText ()
		{
			#if DEBUG
			try {
			#endif

			var asset = Resources.Load<TextAsset>(this.value);
			return asset!=null ? asset.text : null;
			
			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}


		public static implicit operator ResourceFilePath ( string path ) => new ResourceFilePath( path );
		public static implicit operator string ( ResourceFilePath structure ) => structure.value;


	}
}
