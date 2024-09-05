/*  Copyright (c) MediaArea.net SARL. All Rights Reserved.
 *
 *  Use of this source code is governed by a BSD-style license that can
 *  be found in the License.html file in the root of the source tree.
 */

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
// Microsoft Visual C# wrapper for MediaInfo Library
// See MediaInfo.h for help
//
// To make it working, you must put MediaInfo.Dll
// in the executable folder
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MediaInfoLib;

public enum StreamKind
{
    General,
    Video,
    Audio,
    Text,
    Other,
    Image,
    Menu,
}

public enum InfoKind
{
    Name,
    Text,
    Measure,
    Options,
    NameText,
    MeasureText,
    Info,
    HowTo,
}

public enum InfoOptions
{
    ShowInInform,
    Support,
    ShowInSupported,
    TypeOfValue,
}

[Flags]
public enum InfoFileOptions
{
    FileOption_Nothing = 0x00,
    FileOption_NoRecursive = 0x01,
    FileOption_CloseAll = 0x02,
    FileOption_Max = 0x04,
};

[Flags]
public enum Status
{
    None = 0x00,
    Accepted = 0x01,
    Filled = 0x02,
    Updated = 0x04,
    Finalized = 0x08,
}

public static class Libs
{
    public const string Windows = "MediaInfo.dll";
    public const string MacOS = "libmediainfo.dylib";

    public static void LoadDll(string dllPath)
    {
        if (!SetDllDirectory(dllPath))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);
    }
}

/// <summary>
/// Contains constants for the options available in MediaInfo.
/// Each constant represents a parameter that can be used with MediaInfo_Option.
/// </summary>
public static class Options
{
    /// <summary>
    /// Gets the version of MediaInfo.
    /// </summary>
    public const string Info_Version = nameof(Info_Version);

    /// <summary>
    /// Gets the list of all supported parameters.
    /// </summary>
    public const string Info_Parameters = nameof(Info_Parameters);

    /// <summary>
    /// Gets the capacities information, such as supported formats.
    /// </summary>
    public const string Info_Capacities = nameof(Info_Capacities);

    /// <summary>
    /// Gets the list of supported codecs.
    /// </summary>
    public const string Info_Codecs = nameof(Info_Codecs);

    /// <summary>
    /// Sets the output language (e.g., "en" for English, "fr" for French).
    /// </summary>
    public const string Language = nameof(Language);

    /// <summary>
    /// Sets a custom output format.
    /// Example: "Inform=General;%FileName%" outputs the file name.
    /// </summary>
    public const string Inform = nameof(Inform);

    /// <summary>
    /// Sets the output format (e.g., "Text", "XML", "HTML", "JSON").
    /// </summary>
    public const string Output = nameof(Output);

    /// <summary>
    /// Sets whether to display complete information (default is "1" for enabled).
    /// </summary>
    public const string Complete = nameof(Complete);

    /// <summary>
    /// Sets whether to enable continuous file name testing when processing multiple files.
    /// </summary>
    public const string File_TestContinuousFileNames = nameof(File_TestContinuousFileNames);

    /// <summary>
    /// Sets whether the file is seekable ("1" for seekable, "0" for non-seekable).
    /// </summary>
    public const string File_IsSeekable = nameof(File_IsSeekable);

    /// <summary>
    /// Controls the parsing speed (e.g., "1.0" for normal speed, "0.5" for half speed).
    /// </summary>
    public const string ParseSpeed = nameof(ParseSpeed);

    /// <summary>
    /// Sets whether to enable demuxing mode ("1" for enabled, "0" for disabled).
    /// </summary>
    public const string Demux = nameof(Demux);

    /// <summary>
    /// Sets whether to extract cover image data ("1" for extraction, "0" for no extraction).
    /// </summary>
    public const string Cover_Data = nameof(Cover_Data);

    /// <summary>
    /// Sets whether to enable internet access for fetching additional information ("1" for enabled).
    /// </summary>
    public const string Internet = nameof(Internet);

    /// <summary>
    /// Sets a custom information template.
    /// </summary>
    public const string CustomInform = nameof(CustomInform);
}

public class MediaInfo : IDisposable
{
    /// <summary>
    /// Import of DLL functions. DO NOT USE until you know what you do (MediaInfo DLL do NOT use CoTaskMemAlloc to allocate memory)
    /// </summary>
    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_New();

    [DllImport(Libs.Windows)]
    private static extern void MediaInfo_Delete(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Open(nint Handle, [MarshalAs(UnmanagedType.LPWStr)] string FileName);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_Open(nint Handle, nint FileName);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Open_Buffer_Init(nint Handle, long File_Size, long File_Offset);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_Open(nint Handle, long File_Size, long File_Offset);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Open_Buffer_Continue(nint Handle, nint Buffer, nint Buffer_Size);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_Open_Buffer_Continue(nint Handle, long File_Size, byte[] Buffer, nint Buffer_Size);

    [DllImport(Libs.Windows)]
    private static extern long MediaInfo_Open_Buffer_Continue_GoTo_Get(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern long MediaInfoA_Open_Buffer_Continue_GoTo_Get(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Open_Buffer_Finalize(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_Open_Buffer_Finalize(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern void MediaInfo_Close(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Inform(nint Handle, nint Reserved);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_Inform(nint Handle, nint Reserved);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_GetI(nint Handle, nint StreamKind, nint StreamNumber, nint Parameter, nint KindOfInfo);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_GetI(nint Handle, nint StreamKind, nint StreamNumber, nint Parameter, nint KindOfInfo);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Get(nint Handle, nint StreamKind, nint StreamNumber, [MarshalAs(UnmanagedType.LPWStr)] string Parameter, nint KindOfInfo, nint KindOfSearch);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_Get(nint Handle, nint StreamKind, nint StreamNumber, nint Parameter, nint KindOfInfo, nint KindOfSearch);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Option(nint Handle, [MarshalAs(UnmanagedType.LPWStr)] string Option, [MarshalAs(UnmanagedType.LPWStr)] string Value);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoA_Option(nint Handle, nint Option, nint Value);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_State_Get(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfo_Count_Get(nint Handle, nint StreamKind, nint StreamNumber);

    /// <summary>
    /// MediaInfo class
    /// </summary>
    public MediaInfo()
    {
        try
        {
            Handle = MediaInfo_New();
        }
        catch
        {
            Handle = 0;
        }
        if (Environment.OSVersion.ToString().IndexOf("Windows") == -1)
            MustUseAnsi = true;
        else
            MustUseAnsi = false;
    }

    ~MediaInfo()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                if (Handle == 0)
                    return;
                MediaInfo_Delete(Handle);
            }

            Close();

            disposed = true;
        }
    }

    public int Open(string FileName)
    {
        if (Handle == 0)
            return 0;
        if (MustUseAnsi)
        {
            nint FileName_Ptr = Marshal.StringToHGlobalAnsi(FileName);
            int ToReturn = (int)MediaInfoA_Open(Handle, FileName_Ptr);
            Marshal.FreeHGlobal(FileName_Ptr);
            return ToReturn;
        }
        else
            return (int)MediaInfo_Open(Handle, FileName);
    }

    public int Open_Buffer_Init(long File_Size, long File_Offset)
    {
        if (Handle == 0) return 0; return (int)MediaInfo_Open_Buffer_Init(Handle, File_Size, File_Offset);
    }

    public int Open_Buffer_Continue(nint Buffer, nint Buffer_Size)
    {
        if (Handle == 0) return 0; return (int)MediaInfo_Open_Buffer_Continue(Handle, Buffer, Buffer_Size);
    }

    public long Open_Buffer_Continue_GoTo_Get()
    {
        if (Handle == 0) return 0; return (long)MediaInfo_Open_Buffer_Continue_GoTo_Get(Handle);
    }

    public int Open_Buffer_Finalize()
    {
        if (Handle == 0) return 0; return (int)MediaInfo_Open_Buffer_Finalize(Handle);
    }

    public void Close()
    {
        if (Handle == 0) return; MediaInfo_Close(Handle);
    }

    public string Inform()
    {
        if (Handle == 0)
            return "Unable to load MediaInfo library";
        if (MustUseAnsi)
            return Marshal.PtrToStringAnsi(MediaInfoA_Inform(Handle, 0));
        else
            return Marshal.PtrToStringUni(MediaInfo_Inform(Handle, 0));
    }

    public string Get(StreamKind StreamKind, int StreamNumber, string Parameter, InfoKind KindOfInfo, InfoKind KindOfSearch)
    {
        if (Handle == 0)
            return "Unable to load MediaInfo library";
        if (MustUseAnsi)
        {
            nint Parameter_Ptr = Marshal.StringToHGlobalAnsi(Parameter);
            string ToReturn = Marshal.PtrToStringAnsi(MediaInfoA_Get(Handle, (nint)StreamKind, StreamNumber, Parameter_Ptr, (nint)KindOfInfo, (nint)KindOfSearch));
            Marshal.FreeHGlobal(Parameter_Ptr);
            return ToReturn;
        }
        else
            return Marshal.PtrToStringUni(MediaInfo_Get(Handle, (nint)StreamKind, StreamNumber, Parameter, (nint)KindOfInfo, (nint)KindOfSearch));
    }

    public string Get(StreamKind StreamKind, int StreamNumber, int Parameter, InfoKind KindOfInfo)
    {
        if (Handle == 0)
            return "Unable to load MediaInfo library";
        if (MustUseAnsi)
            return Marshal.PtrToStringAnsi(MediaInfoA_GetI(Handle, (nint)StreamKind, StreamNumber, Parameter, (nint)KindOfInfo));
        else
            return Marshal.PtrToStringUni(MediaInfo_GetI(Handle, (nint)StreamKind, StreamNumber, Parameter, (nint)KindOfInfo));
    }

    public string Option(string Option, string Value)
    {
        if (Handle == 0)
            return "Unable to load MediaInfo library";
        if (MustUseAnsi)
        {
            nint Option_Ptr = Marshal.StringToHGlobalAnsi(Option);
            nint Value_Ptr = Marshal.StringToHGlobalAnsi(Value);
            string ToReturn = Marshal.PtrToStringAnsi(MediaInfoA_Option(Handle, Option_Ptr, Value_Ptr));
            Marshal.FreeHGlobal(Option_Ptr);
            Marshal.FreeHGlobal(Value_Ptr);
            return ToReturn;
        }
        else
            return Marshal.PtrToStringUni(MediaInfo_Option(Handle, Option, Value));
    }

    public int State_Get()
    {
        if (Handle == 0) return 0; return (int)MediaInfo_State_Get(Handle);
    }

    public int Count_Get(StreamKind StreamKind, int StreamNumber)
    {
        if (Handle == 0) return 0; return (int)MediaInfo_Count_Get(Handle, (nint)StreamKind, StreamNumber);
    }

    private readonly nint Handle;
    private readonly bool MustUseAnsi;

    /// <summary>
    /// Default values, if you know how to set default values in C#, say me
    /// </summary>
    public string Get(StreamKind StreamKind, int StreamNumber, string Parameter, InfoKind KindOfInfo)
    {
        return Get(StreamKind, StreamNumber, Parameter, KindOfInfo, InfoKind.Name);
    }

    public string Get(StreamKind StreamKind, int StreamNumber, string Parameter)
    {
        return Get(StreamKind, StreamNumber, Parameter, InfoKind.Text, InfoKind.Name);
    }

    public string Get(StreamKind StreamKind, int StreamNumber, int Parameter)
    {
        return Get(StreamKind, StreamNumber, Parameter, InfoKind.Text);
    }

    public string Option(string Option_)
    {
        return Option(Option_, string.Empty);
    }

    public int Count_Get(StreamKind StreamKind)
    {
        return Count_Get(StreamKind, -1);
    }

    private bool disposed = false;
}

public static class MediaInfoExtension
{
    public static MediaInfo WithOpen(this MediaInfo self, string FileName)
    {
        _ = self.Open(FileName);
        return self;
    }

    public static MediaInfo WithOption(this MediaInfo self, string Option, string Value, out string Result)
    {
        Result = self.Option(Option, Value);
        return self;
    }

    public static MediaInfo WithOption(this MediaInfo self, string Option_, out string Result)
    {
        Result = self.Option(Option_);
        return self;
    }
}

public class MediaInfoList : IDisposable
{
    //Import of DLL functions. DO NOT USE until you know what you do (MediaInfo DLL do NOT use CoTaskMemAlloc to allocate memory)
    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_New();

    [DllImport(Libs.Windows)]
    private static extern void MediaInfoList_Delete(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_Open(nint Handle, [MarshalAs(UnmanagedType.LPWStr)] string FileName, nint Options);

    [DllImport(Libs.Windows)]
    private static extern void MediaInfoList_Close(nint Handle, nint FilePos);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_Inform(nint Handle, nint FilePos, nint Reserved);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_GetI(nint Handle, nint FilePos, nint StreamKind, nint StreamNumber, nint Parameter, nint KindOfInfo);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_Get(nint Handle, nint FilePos, nint StreamKind, nint StreamNumber, [MarshalAs(UnmanagedType.LPWStr)] string Parameter, nint KindOfInfo, nint KindOfSearch);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_Option(nint Handle, [MarshalAs(UnmanagedType.LPWStr)] string Option, [MarshalAs(UnmanagedType.LPWStr)] string Value);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_State_Get(nint Handle);

    [DllImport(Libs.Windows)]
    private static extern nint MediaInfoList_Count_Get(nint Handle, nint FilePos, nint StreamKind, nint StreamNumber);

    //MediaInfo class
    public MediaInfoList()
    {
        Handle = MediaInfoList_New();
    }

    ~MediaInfoList()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                MediaInfoList_Delete(Handle);
            }

            Close();

            disposed = true;
        }
    }

    public int Open(string FileName, InfoFileOptions Options)
    {
        return (int)MediaInfoList_Open(Handle, FileName, (nint)Options);
    }

    public void Close(int FilePos)
    {
        MediaInfoList_Close(Handle, FilePos);
    }

    public string Inform(int FilePos)
    {
        return Marshal.PtrToStringUni(MediaInfoList_Inform(Handle, FilePos, 0));
    }

    public string Get(int FilePos, StreamKind StreamKind, int StreamNumber, string Parameter, InfoKind KindOfInfo, InfoKind KindOfSearch)
    {
        return Marshal.PtrToStringUni(MediaInfoList_Get(Handle, FilePos, (nint)StreamKind, StreamNumber, Parameter, (nint)KindOfInfo, (nint)KindOfSearch));
    }

    public string Get(int FilePos, StreamKind StreamKind, int StreamNumber, int Parameter, InfoKind KindOfInfo)
    {
        return Marshal.PtrToStringUni(MediaInfoList_GetI(Handle, FilePos, (nint)StreamKind, StreamNumber, Parameter, (nint)KindOfInfo));
    }

    public string Option(string Option, string Value)
    {
        return Marshal.PtrToStringUni(MediaInfoList_Option(Handle, Option, Value));
    }

    public int State_Get()
    {
        return (int)MediaInfoList_State_Get(Handle);
    }

    public int Count_Get(int FilePos, StreamKind StreamKind, int StreamNumber)
    {
        return (int)MediaInfoList_Count_Get(Handle, FilePos, (nint)StreamKind, StreamNumber);
    }

    private readonly nint Handle;

    /// <summary>
    /// Default values, if you know how to set default values in C#, say me
    /// </summary>
    public void Open(string FileName)
    {
        Open(FileName, 0);
    }

    public void Close()
    {
        Close(-1);
    }

    public string Get(int FilePos, StreamKind StreamKind, int StreamNumber, string Parameter, InfoKind KindOfInfo)
    {
        return Get(FilePos, StreamKind, StreamNumber, Parameter, KindOfInfo, InfoKind.Name);
    }

    public string Get(int FilePos, StreamKind StreamKind, int StreamNumber, string Parameter)
    {
        return Get(FilePos, StreamKind, StreamNumber, Parameter, InfoKind.Text, InfoKind.Name);
    }

    public string Get(int FilePos, StreamKind StreamKind, int StreamNumber, int Parameter)
    {
        return Get(FilePos, StreamKind, StreamNumber, Parameter, InfoKind.Text);
    }

    public string Option(string Option_)
    {
        return Option(Option_, string.Empty);
    }

    public int Count_Get(int FilePos, StreamKind StreamKind)
    {
        return Count_Get(FilePos, StreamKind, -1);
    }

    private bool disposed = false;
}

public static class MediaInfoListExtension
{
    public static MediaInfoList WithOpen(this MediaInfoList self, string FileName)
    {
        self.Open(FileName);
        return self;
    }

    public static MediaInfoList WithOption(this MediaInfoList self, string Option, string Value, out string Result)
    {
        Result = self.Option(Option, Value);
        return self;
    }

    public static MediaInfoList WithOption(this MediaInfoList self, string Option_, out string Result)
    {
        Result = self.Option(Option_);
        return self;
    }
}
