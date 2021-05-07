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
			_Exists( "UnitTest_file" , false );
		}
		[Test] public void _Exists_false_2 ()
		{
			_Exists( $"UnitTest_folder/UnitTest_file" , false );
		}
		[Test] public void _Exists_true_1 ()
		{
			string nameWithExtention = $".UnitTest_file.unit_test_file";
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
			string nameWithExtention = $".UnitTest_file.unit_test_file";
			string folder = "UnitTest_folder";
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
			string path = IO.Path.Combine( Application.temporaryCachePath , "UnitTest_folder" );
			if( IO.Directory.Exists(path) ) IO.Directory.Delete(path);
			_Exists( path , false );
		}
		[Test] public void _Exists_false_2 ()
		{
			string path = IO.Path.Combine( Application.temporaryCachePath , "UnitTest_folder" , "UnitTest_subfolder" );
			if( IO.Directory.Exists(path) ) IO.Directory.Delete(path);
			_Exists( path , false );
		}
		[Test] public void _Exists_true_1 ()
		{
			string path = IO.Path.Combine( Application.temporaryCachePath , "UnitTest_folder" );
			if( IO.Directory.Exists(path) ) IO.Directory.Delete(path);
			var dir = IO.Directory.CreateDirectory( path );
			try
			{
				_Exists( path , true );
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
			string absDirectoryPath = IO.Path.Combine( Application.temporaryCachePath , "UnitTest_folder" , "UnitTest_subfolder" );
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
		
	}


	class _ResourceFilePath
	{
		[Test] public void _Exists_false_1 ()
		{
			_Exists( "UnitTest_file" , false );
		}
		[Test] public void _Exists_true_1 ()
		{
			const string asset_resources = "Assets/Resources";
			string file = "UnitTest_file";
			string folder = "UnitTest_folder";
			string resourcePath = $"{folder}/{file}";

			if( !AssetDatabase.IsValidFolder(asset_resources) )
				AssetDatabase.CreateFolder( "Assets/" , "Resources" );
			if( !AssetDatabase.IsValidFolder(IO.Path.Combine(asset_resources,folder)) )
				AssetDatabase.CreateFolder( asset_resources , folder );
			
			string absFilePath = IO.Path.Combine( Application.dataPath, $"Resources/{folder}/{file}.txt" );
			IO.File.WriteAllText( absFilePath , "test content" );
			AssetDatabase.Refresh();
			System.Action dispose = () => AssetDatabase.DeleteAsset($"{asset_resources}/{folder}");
			
			try
			{
				_Exists( resourcePath , true );
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
	}


	class _AbsoluteFilePath
	{
		[Test] public void _Exists_false_1 ()
		{
			_Exists( IO.Path.Combine( Application.temporaryCachePath , "UnitTest_file" ) , false );
		}

		[Test] public void _Exists_true_1 ()
		{
			string absFilePath = IO.Path.Combine( Application.temporaryCachePath , "UnitTest_file.txt" );
			IO.File.WriteAllText( absFilePath , "test content" );
			System.Action dispose = () => IO.File.Delete( absFilePath );
			try
			{
				_Exists( absFilePath , true );
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
			Assert.AreEqual( expected:expected , actual:new AbsoluteFilePath(path).Exists() );
		}
	}


}
