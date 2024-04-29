# MediaInfo

Here are MediaInfo .NET APIs wrapped from [MediaArea.net](https://mediaarea.net/en/MediaInfo/Download/Windows).

> This package only supports Windows 64-bit.
>
> If you want to use in Windows 32-bit, you can replace the `MediaInfo.dll` to 32bit type by yourself.

## Nuget

https://www.nuget.org/packages/MediaInfoDLL

## Usage

```c#
using MediaInfo mi = new();
mi.Open(@"C:\media.mp4");
string inform = mi.Inform();
mi.Close();
Console.WriteLine(inform);
```

## Demo

[ConsoleDemo](https://github.com/lemutec/MediaInfoNet/tree/main/demo/ConsoleDemo)

[WinFormsDemo](https://github.com/lemutec/MediaInfoNet/tree/main/demo/WinFormsDemo)
