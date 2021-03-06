```c#
using IOPaths;

AbsoluteFilePath persistentDataCache =
		( (AbsoluteDirectoryPath) Application.persistentDataPath )
		.Combine( (DirectoryName) "Cache" )
		.Combine( (DirectoryName) "001" )
		.Combine( (FileName) "cached.bytes" );

if( persistentDataCache.Exists() )
{
	byte[] oldBytes = persistentDataCache.ReadAllBytes();
	Debug.Log($"old cache was {oldBytes.Length} bytes long");
	persistentDataCache.Delete();
}
persistentDataCache.WriteAllBytes(
    new byte[]{0,1,2,4,8,16,32,64,128,255}
);
```
---
| type | description |
| :---         |          :--- |
| `FileName`   | file name    |
| `DirectoryName`     | directory name      |
| `AbsoluteFilePath`     | absolute file path      |
| `AbsoluteDirectoryPath`     | absolute directory path      |
| `ResourceFilePath`     | `/Assets/Resources`-relative file path      |
| `ResourceDirectoryPath`     | `/Assets/Resources`-relative directory path      |
| `AssetsFilePath`     | `/Assets`-relative file path      |

---
```
"com.andrewraphaellukasik.iopaths": "https://github.com/andrew-raphael-lukasik/IOPaths.git#upm"
```
