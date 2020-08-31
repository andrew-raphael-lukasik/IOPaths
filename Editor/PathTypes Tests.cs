// using System.Collections.Generic;
// using UnityEngine;

// using NUnit.Framework;

// namespace PathTypes.UnitTests
// {
// 	public class RunTests
// 	{

// 		[Test] public static void Combine_AbsoluteDirectoryPath_And_FileName ()
// 		{
// 			Assert.AreEqual(
// 				"c:/folder/my_file.txt" ,
// 				new AbsoluteDirectoryPath( "c:/folder/" ).Combine( new FileName("my_file.txt") ).Value
// 			);
// 		}

// 		[Test] public static void Cast_AbsoluteFilePath_To_ResourceFilePath ()
// 		{
// 			Assert.AreEqual(
// 				"Resources/folder2/file.txt" ,
// 				(string) (ResourceFilePath) new AbsoluteFilePath("c:/folder1/Resources/folder2/file.txt")
// 			);
// 		}

// 		// [Test] public static void Cast_1 ()
// 		// {
// 		// 	Assert.AreEqual(
// 		// 		"Resources/folder2/file.txt" ,
// 		// 		(string) (ResourceFilePath) new AbsoluteFilePath("c:/folder1/Resources/folder2/file.txt")
// 		// 	);
// 		// 	int indexOfAssets = filePath.IndexOf("Resources") + "Resources/".Length;
// 		// 	string resourcesRelativeFilePath = filePath.Remove( 0 , indexOfAssets );
// 		// }


// 		[Test] public static void RelativeAssetFilePath_1 () => Assert.AreEqual( "Assets/folder2/file.txt" , (string) new RelativeAssetFilePath("c:/projects/project1/Assets/folder2/file.txt") );

// 	}
// }
