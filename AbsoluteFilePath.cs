using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// <summary> Utility structures to help working with file paths in Unity. Adds few extra validation checks. </summary>
namespace IOPaths
{
	public struct AbsoluteFilePath
	{

		public readonly string value;


		public AbsoluteFilePath ( string str )
		{
			#if DEBUG
			Assert.IsFalse( str.StartsWith("/") , $"{nameof(FileName)} starts with '/' character" );
			Assert.IsFalse( str.StartsWith("\\") , $"{nameof(FileName)} starts with '\\' character" );
			#endif

			this.value = str;
		}


		public override string ToString () => this.value;
		public bool Exists () => !string.IsNullOrEmpty(this.value) && IO.File.Exists(this.value);
		public bool Contains ( string str ) => this.value.Contains(str);
		public AbsoluteFilePath ChangeExtension ( string extension ) => new AbsoluteFilePath( IO.Path.ChangeExtension(this.value,extension) );
		public void Delete ()
		{
			#if DEBUG
			try {
			#endif

			IO.File.Delete( this.value );
			
			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
		public void WriteAllBytes ( byte[] bytes )
		{
			#if DEBUG
			try {
			#endif

			IO.File.WriteAllBytes( this.value , bytes );

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
		public void WriteAllText ( string text )
		{
			#if DEBUG
			try {
			#endif

			IO.File.WriteAllText( this.value , text );

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
		public void WriteAllLines ( IEnumerable<string> lines )
		{
			#if DEBUG
			try {
			#endif

			IO.File.WriteAllLines( this.value , lines );

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
		public byte[] ReadAllBytes ()
		{
			#if DEBUG
			try {
			#endif

			return IO.File.ReadAllBytes( this.value );
			
			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
		public string ReadAllText ()
		{
			#if DEBUG
			try {
			#endif

			return IO.File.ReadAllText( this.value );

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
		public string[] ReadAllLines ()
		{
			#if DEBUG
			try {
			#endif

			return IO.File.ReadAllLines( this.value );

			#if DEBUG
			} catch( System.Exception ex )
			{
				Debug.LogException(ex);
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}
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
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
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
				Debug.LogError($"{nameof(AbsoluteFilePath)}: {this.value}");
				throw;
			}
			#endif
		}


		public static implicit operator AbsoluteFilePath ( string path ) => new AbsoluteFilePath( path );
		public static implicit operator string ( AbsoluteFilePath structure ) => structure.value;
		public static implicit operator ResourceFilePath ( AbsoluteFilePath absoluteFilePath )
		{
			string rawValue = absoluteFilePath.value;
			int splitIndex = Mathf.Max( rawValue.LastIndexOf("Resources") , 0 );
			return rawValue.Substring( splitIndex );
		}

		
	}
}
