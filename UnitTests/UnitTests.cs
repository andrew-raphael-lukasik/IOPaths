using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using IO = System.IO;

using IOPaths;

namespace IOPaths.UnitTests
{

	
	public class _AssetsFilePath
	{
		[Test] public void _Exists_false_1 ()
		{
			_Exists( System.Guid.NewGuid().ToString() , false );
		}
		[Test] public void _Exists_false_2 ()
		{
			string file = System.Guid.NewGuid().ToString();
			string folder = System.Guid.NewGuid().ToString();
			_Exists( $"{folder}/{file}" , false );
		}
		[Test] public void _Exists_true_1 ()
		{
			string name = System.Guid.NewGuid().ToString();
			string nameWithExtention = $".{name}.unit_test_file";
			string absFilePath = IO.Path.Combine( Application.dataPath , nameWithExtention );
			var stream = IO.File.Create( absFilePath , 4 , IO.FileOptions.DeleteOnClose );
			try
			{
				_Exists( nameWithExtention , true );
			}
			catch( System.Exception ex )
			{
				stream.Close();
				throw ex;
			}
			stream.Close();
		}
		[Test] public void _Exists_true_2 ()
		{
			string name = System.Guid.NewGuid().ToString();
			string nameWithExtention = $".{name}.unit_test_file";
			string folder = System.Guid.NewGuid().ToString();
			string absDirectoryPath = IO.Path.Combine( Application.dataPath , folder );
			string absFilePath = IO.Path.Combine( absDirectoryPath , nameWithExtention );
			var dir = IO.Directory.CreateDirectory( absDirectoryPath );
			var stream = IO.File.Create( absFilePath , 4 ,IO.FileOptions.DeleteOnClose );
			System.Action dispose = () => { stream.Close(); dir.Delete(); };
			try
			{
				_Exists( IO.Path.Combine( folder , nameWithExtention ) , true );
			}
			catch( System.Exception ex )
			{
				dispose();
				throw ex;
			}
			dispose();
		}

		void _Exists ( string path , bool expected )
		{
			Debug.Log($"\ttesting: '{path}', expected: {expected}");
			Assert.AreEqual( expected:expected , actual:new AssetsFilePath(path).Exists() );
		}
	}


	class _AbsoluteDirectoryPath
	{
		[Test] public void _Exists_false_1 ()
		{
			string folder = System.Guid.NewGuid().ToString();
			_Exists( IO.Path.Combine( Application.temporaryCachePath , folder ) , false );
		}
		[Test] public void _Exists_false_2 ()
		{
			string subFolder = System.Guid.NewGuid().ToString();
			string folder = System.Guid.NewGuid().ToString();
			_Exists( IO.Path.Combine( Application.temporaryCachePath , folder , subFolder ) , false );
		}
		[Test] public void _Exists_true_1 ()
		{
			string name = System.Guid.NewGuid().ToString();
			string absDirectoryPath = IO.Path.Combine( Application.temporaryCachePath , name );
			var dir = IO.Directory.CreateDirectory( absDirectoryPath );
			try
			{
				_Exists( absDirectoryPath , true );
			}
			catch( System.Exception ex )
			{
				dir.Delete();
				throw ex;
			}
			dir.Delete();
		}
		[Test] public void _Exists_true_2 ()
		{
			string folder = $".{System.Guid.NewGuid().ToString()}/";
			string subFolder = $"{System.Guid.NewGuid().ToString()}/";
			string absDirectoryPath = IO.Path.Combine( Application.temporaryCachePath , folder , subFolder );
			var dir = IO.Directory.CreateDirectory( absDirectoryPath );
			try
			{
				_Exists( absDirectoryPath , true );
			}
			catch( System.Exception ex )
			{
				dir.Delete();
				throw ex;
			}
			dir.Delete();
		}

		void _Exists ( string path , bool expected )
		{
			Debug.Log($"\ttesting: '{path}', expected: {expected}");
			Assert.AreEqual( expected:expected , actual:new AbsoluteDirectoryPath(path).Exists() );
		}
	}


	class _ResourceDirectoryPath
	{
		[Test] public void _Exists_false_1 ()
		{
			string guid = System.Guid.NewGuid().ToString();
			_Exists( $".{guid}" , false );
		}
		[Test] public void _Exists_true_1 ()
		{
			string folder = System.Guid.NewGuid().ToString();

			if( !AssetDatabase.IsValidFolder("Assets/Resources") )
				AssetDatabase.CreateFolder( "Assets/" , "Resources" );
			var tempFolderGuid = AssetDatabase.CreateFolder( "Assets/Resources" , folder );
			try
			{
				_Exists( folder , true );
			}
			catch( System.Exception ex )
			{
				AssetDatabase.DeleteAsset($"Assets/Resources/{folder}");
				throw ex;
			}
			AssetDatabase.DeleteAsset($"Assets/Resources/{folder}");
		}

		void _Exists ( string path , bool expected )
		{
			Debug.Log($"\ttesting: '{path}', expected: {expected}");
			Assert.AreEqual( expected:expected , actual:new ResourceDirectoryPath(path).Exists() );
		}
	}


	class _ResourceFilePath
	{
		[Test] public void _Exists_false_1 ()
		{
			string guid = System.Guid.NewGuid().ToString();
			_Exists( IO.Path.Combine( $"{guid}.unit_test_file" ) , false );
		}
		[Test] public void _Exists_true_1 ()
		{
			string file = $"{System.Guid.NewGuid().ToString()}.unit_test_file.TextAsset";
			string folder = System.Guid.NewGuid().ToString();

			if( !AssetDatabase.IsValidFolder("Assets/Resources") )
				AssetDatabase.CreateFolder( "Assets/" , "Resources" );
			AssetDatabase.CreateFolder( "Assets/Resources" , folder );
			var obj = ScriptableObject.CreateInstance<TEST>();
			AssetDatabase.CreateAsset( obj , $"Assets/Resources/{folder}/{file}" );
			System.Action dispose = () =>
			{
				Object.DestroyImmediate( obj , true );
				AssetDatabase.DeleteAsset($"Assets/Resources/{folder}/{file}");
				AssetDatabase.DeleteAsset($"Assets/Resources/{folder}");
			};
			try
			{
				_Exists( folder , true );
			}
			catch( System.Exception ex )
			{
				dispose();
				throw ex;
			}
			dispose();
		}

		void _Exists ( string path , bool expected )
		{
			Debug.Log($"\ttesting: '{path}', expected: {expected}");
			Assert.AreEqual( expected:expected , actual:new ResourceFilePath(path).Exists() );
		}
		[System.Serializable] class TEST : ScriptableObject { public string test = "test"; }
	}


	class _AbsoluteFilePath
	{
		[Test] public void _Exists_false_1 ()
		{
			string guid = System.Guid.NewGuid().ToString();
			_Exists( IO.Path.Combine( Application.temporaryCachePath , $".{guid}.unit_test_file" ) , false );
		}

		void _Exists ( string path , bool expected )
		{
			Debug.Log($"\ttesting: '{path}', expected: {expected}");
			Assert.AreEqual( expected:expected , actual:new AbsoluteFilePath(path).Exists() );
		}
	}


}
