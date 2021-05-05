using IO = System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

using IOPaths;
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
			return IO.File.Exists( file.AbsPath );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}

	public static void Delete ( this IFile file )
	{
		try {
			IO.File.Delete( file.AbsPath );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}

	public static void WriteAllBytes ( this IFile file , byte[] bytes )
	{
		try {
			IO.File.WriteAllBytes( file.AbsPath , bytes );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}

	public static void WriteAllText ( this IFile file , string text )
	{
		try {
			IO.File.WriteAllText( file.AbsPath , text );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}

	public static void WriteAllLines ( this IFile file , IEnumerable<string> lines )
	{
		try {
			IO.File.WriteAllLines( file.AbsPath , lines );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}
	
	public static byte[] ReadAllBytes ( this IFile file )
	{
		try {
			return IO.File.ReadAllBytes( file.AbsPath );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}

	public static string ReadAllText ( this IFile file )
	{
		try {
			return IO.File.ReadAllText( file.AbsPath );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}

	public static string[] ReadAllLines ( this IFile file )
	{
		try {
			return IO.File.ReadAllLines( file.AbsPath );
		}
		catch( System.Exception ex )
		{
			Debug.LogException(ex);
			Debug.LogError($"{nameof(file)}: {file.AbsPath}");
			throw;
		}
	}
	
}