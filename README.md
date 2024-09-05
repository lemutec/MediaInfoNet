[![NuGet](https://img.shields.io/nuget/v/MediaInfoDLL.svg)](https://nuget.org/packages/MediaInfoDLL) [![Actions](https://github.com/lemutec/MediaInfoNet/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/lemutec/MediaInfoNet/actions/workflows/library.nuget.yml) [![Platform](https://img.shields.io/badge/platform-Windows-blue?logo=windowsxp&color=1E9BFA)](https://dotnet.microsoft.com/en-us/download/dotnet/latest/runtime)

# MediaInfo

Minimal MediaInfo .NET APIs wrapped from [MediaArea.net](https://mediaarea.net/en/MediaInfo/Download/Windows).

Support for Windows Vista, 7, 8, 10, 11 with x86, x64.

Nuget：https://www.nuget.org/packages/MediaInfoDLL

## Usage

1. Provide Media Inform

```c#
using MediaInfoLib;

using MediaInfo lib = new();
lib.Open(@"C:\media.mp4");
Console.WriteLine(lib.Inform());
```

if you wanna completed inform.

```c#
using MediaInfoLib;

using MediaInfo lib = new();
lib.Open(fileName);
lib.Option("Complete", "1");
Console.WriteLine(lib.Inform());
```

2. Check audio track exists

```C#
using MediaInfoLib;

using MediaInfo lib = new();
bool hasAudio = lib.WithOpen(fileName).Count_Get(StreamKind.Audio) > 0;
Console.WriteLine(hasAudio);
```

3. Get audio track parameter

```C#
using MediaInfoLib;

using MediaInfo lib = new();
lib.Open(fileName);
_ = double.TryParse(lib.Get(StreamKind.Audio, 0, "BitRate"), out double bitRate);
Console.WriteLine(bitRate);
```

## Samples

[ConsoleDemo](https://github.com/lemutec/MediaInfoNet/tree/main/demo/ConsoleDemo)

[WinFormsDemo](https://github.com/lemutec/MediaInfoNet/tree/main/demo/WinFormsDemo)

## License

MediaInfoLib - https://github.com/MediaArea/MediaInfoLib

Copyright (c) MediaArea.net SARL. All Rights Reserved.

This program is freeware under BSD-2-Clause license conditions.

See License.html for more information
