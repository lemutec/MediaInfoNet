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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static MediaInfoLib.Options;

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
/// https://github.com/MediaArea/MediaInfoLib/blob/master/Source/MediaInfo/MediaInfo_Config.h
/// </summary>
public static class Options
{
    /// <summary>
    /// Sets whether to display complete information (default is "1" for enabled).
    /// </summary>
    [Description("void Complete_Set (size_t NewValue);")]
    public const string Complete = "Complete";

    [Description("size_t Complete_Get ()")]
    public const string Complete_Get = "Complete_Get";

    [Description("void BlockMethod_Set (size_t NewValue);")]
    public const string BlockMethod = "BlockMethod";

    [Description("size_t BlockMethod_Get ();")]
    public const string BlockMethod_Get = "BlockMethod_Get";

    [Description("void Internet_Set (size_t NewValue);")]
    public const string Internet = "Internet";

    [Description("size_t Internet_Get ();")]
    public const string Internet_Get = "Internet_Get";

    [Description("void MultipleValues_Set (size_t NewValue);")]
    public const string MultipleValues = "MultipleValues";

    [Description("size_t MultipleValues_Get ();")]
    public const string MultipleValues_Get = "MultipleValues_Get";

    [Description("void ReadByHuman_Set (bool NewValue);")]
    public const string ReadByHuman = "ReadByHuman";

    [Description("bool ReadByHuman_Get ();")]
    public const string ReadByHuman_Get = "ReadByHuman_Get";

    [Description("void Legacy_Set (bool NewValue);")]
    public const string Legacy = "Legacy";

    [Description("bool Legacy_Get ();")]
    public const string Legacy_Get = "Legacy_Get";

    [Description("void LegacyStreamDisplay_Set(bool Value);")]
    public const string LegacyStreamDisplay = "LegacyStreamDisplay";

    [Description("bool LegacyStreamDisplay_Get();")]
    public const string LegacyStreamDisplay_Get = "LegacyStreamDisplay_Get";

    [Description("void SkipBinaryData_Set(bool Value);")]
    public const string SkipBinaryData = "SkipBinaryData";

    [Description("bool SkipBinaryData_Get();")]
    public const string SkipBinaryData_Get = "SkipBinaryData_Get";

    [Description("void ParseSpeed_Set(float32 NewValue);")]
    public const string ParseSpeed = "ParseSpeed";

    [Description("float32 ParseSpeed_Get();")]
    public const string ParseSpeed_Get = "ParseSpeed_Get";

    [Description("void Verbosity_Set(float32 NewValue);")]
    public const string SkipBinarVerbosityyData_Get = "Verbosity";

    [Description("float32 Verbosity_Get();")]
    public const string Verbosity_Get = "Verbosity_Get";

    [Description("void Compat_Set(int64u NewValue);")]
    public const string Compat = "Compat";

    [Description("int64u Compat_Get();")]
    public const string Compat_Get = "Compat_Get";

    [Description("void Https_Set(bool NewValue);")]
    public const string Https = "Https";

    [Description("bool Https_Get();")]
    public const string Https_Get = "Https_Get";

    /// <summary>
    /// <see cref="Trace_Format"/>
    /// </summary>
    [Description("void Trace_Format_Set(trace_Format NewValue);")]
    public const string TraceFormat = "Trace_Format";

    [Description("trace_Format Trace_Format_Get();")]
    public const string TraceFormat_Get = "Trace_Format_Get";

    [Description("void Demux_Set(int8u NewValue);")]
    public const string Demux = "Demux";

    [Description("int8u Demux_Get();")]
    public const string Demux_Get = "Demux_Get";

    [Description("void LineSeparator_Set(const Ztring &NewValue);")]
    public const string LineSeparator = "LineSeparator";

    [Description("Ztring LineSeparator_Get();")]
    public const string LineSeparator_Get = "LineSeparator_Get";

    [Description("void Version_Set(const Ztring &NewValue);")]
    public const string Version = "Version";

    [Description("Ztring Version_Get();")]
    public const string Version_Get = "Version_Get";

    [Description("void ColumnSeparator_Set(const Ztring &NewValue);")]
    public const string ColumnSeparator = "ColumnSeparator";

    [Description("Ztring ColumnSeparator_Get();")]
    public const string ColumnSeparator_Get = "ColumnSeparator_Get";

    [Description("void TagSeparator_Set(const Ztring &NewValue);")]
    public const string TagSeparator = "TagSeparator";

    [Description("Ztring TagSeparator_Get();")]
    public const string TagSeparator_Get = "TagSeparator_Get";

    [Description("void Quote_Set(const Ztring &NewValue);")]
    public const string Quote = "Quote";

    [Description("Ztring Quote_Get();")]
    public const string Quote_Get = "Quote_Get";

    [Description("void DecimalPoint_Set(const Ztring &NewValue);")]
    public const string DecimalPoint = "DecimalPoint";

    [Description("Ztring DecimalPoint_Get();")]
    public const string DecimalPoint_Get = "DecimalPoint_Get";

    [Description("void ThousandsPoint_Set(const Ztring &NewValue);")]
    public const string ThousandsPoint = "ThousandsPoint";

    [Description("Ztring ThousandsPoint_Get();")]
    public const string ThousandsPoint_Get = "ThousandsPoint_Get";

    [Description("void CarriageReturnReplace_Set(const Ztring &NewValue);")]
    public const string CarriageReturnReplace = "CarriageReturnReplace";

    [Description("Ztring CarriageReturnReplace_Get();")]
    public const string CarriageReturnReplace_Get = "CarriageReturnReplace_Get";

    [Description("void StreamMax_Set(const ZtringListList &NewValue);")]
    public const string StreamMax = "StreamMax";

    [Description("Ztring StreamMax_Get();")]
    public const string StreamMax_Get = "StreamMax_Get";

    /// <summary>
    /// Sets the output language (e.g., "en" for English, "fr" for French).
    /// https://github.com/MediaArea/MediaInfo/blob/master/Source/Resource/Language.csv
    /// <see cref="Language_ISO639"/><see cref="LanguageExtension"/>
    /// </summary>
    [Description("void Language_Set(const ZtringListList &NewLanguage);")]
    public const string Language = "Language";

    [Description("Ztring Language_Get();")]
    public const string Language_Get = "Language_Get";

    [Description("void Inform_Set(const ZtringListList &NewInform);")]
    public const string Inform = "Inform";

    [Description("Ztring Inform_Get();")]
    public const string Inform_Get = "Inform_Get";

    [Description("void Inform_Version_Set(bool NewValue);")]
    public const string Inform_Version_Set = "Inform_Version";

    [Description("bool Inform_Version_Get();")]
    public const string Inform_Version_Get = "Inform_Version_Get";

    [Description("void Inform_Timestamp_Set(bool NewValue);")]
    public const string Inform_Timestamp = "Inform_Timestamp";

    [Description("bool Inform_Timestamp_Get();")]
    public const string Inform_Timestamp_Get = "Inform_Timestamp_Get";

    [Description("InfoMap &Format_Get();")]
    public const string Format_Get = "Format_Get";

    [Description("const Ztring &Codec_Get(private const Ztring &Value, private infocodec_t KindOfCodecInfo = InfoCodec_Name);")]
    public const string Codec_Get = "Codec_Get";

    [Description("void AcquisitionDataOutputMode_Set(size_t Value);")]
    public const string AcquisitionDataOutputMode = "AcquisitionDataOutputMode";

    [Description("size_t AcquisitionDataOutputMode_Get();")]
    public const string AcquisitionDataOutputMode_Get = "AcquisitionDataOutputMode_Get";

    public static class Returns
    {
        public const string Frame = "Frame";
        public const string Container = "Container";
        public const string Elementary = "Elementary";
        public const string OptionNotKnown = "Option not known";
    }

    public enum Trace_Format
    {
        Trace_Format_Tree,
        Trace_Format_CSV,
        Trace_Format_XML,
        Trace_Format_MICRO_XML,
    };

    public enum Inform_Format
    {
        Tree,
        Text,
        CSV,
        HTML,
        XML,
        MAXML,
        JSON,
    }

    /// <summary>
    /// Language_ISO639;ar;be;bg;ca;cs;da;de;en;es;eu;fa;fr;gl;gr;hu;id;it;ja;ko;lt;nl;pl;pt;pt-BR;ro;ru;sk;sq;sv;th;tr;uk;zh-CN;zh-HK;zh-TW;hr;hy;ka
    /// </summary>
    public enum Language_ISO639
    {
        [Description("en")] English,
        [Description("fr")] French,
        [Description("de")] German,
        [Description("es")] Spanish,
        [Description("it")] Italian,
        [Description("ja")] Japanese,
        [Description("ko")] Korean,
        [Description("zh-CN")] ChineseSimplified,
        [Description("zh-HK")] ChineseTraditional,
        [Description("ru")] Russian,
        [Description("pt")] Portuguese,
        [Description("pt-BR")] PortugueseBrazilian,
        [Description("nl")] Dutch,
        [Description("sv")] Swedish,
        [Description("da")] Danish,
        [Description("fi")] Finnish,
        [Description("no")] Norwegian,
        [Description("pl")] Polish,
        [Description("cs")] Czech,
        [Description("sk")] Slovak,
        [Description("hu")] Hungarian,
        [Description("bg")] Bulgarian,
        [Description("uk")] Ukrainian,
        [Description("ro")] Romanian,
        [Description("hr")] Croatian,
        [Description("sr")] Serbian,
        [Description("sl")] Slovenian,
        [Description("mk")] Macedonian,
        [Description("lt")] Lithuanian,
        [Description("lv")] Latvian,
        [Description("et")] Estonian,
        [Description("gr")] Greek,
        [Description("tr")] Turkish,
        [Description("fa")] Persian,
        [Description("ar")] Arabic,
        [Description("he")] Hebrew,
        [Description("id")] Indonesian,
        [Description("th")] Thai,
        [Description("hy")] Armenian,
        [Description("ka")] Georgian,
    }
}

public static class LanguageExtension
{
    public static string ToOption(this Language_ISO639 language_ISO639)
    {
        if (typeof(Language_ISO639)
            .GetField(language_ISO639.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() is DescriptionAttribute { } attr)
        {
            return attr.Description;
        }

        // Fallback to default language.
        return "en";
    }

    public static string ToOption(this CultureInfo culture)
    {
        string language_iso639 = culture.Name switch
        {
            "zh" => "zh-CN",
            "zh-CN" or "zh-HK" or "zh-TW" or "pt-BR" => culture.Name,
            _ => culture.TwoLetterISOLanguageName,
        };

        if (Array.IndexOf(
            "ar;be;bg;ca;cs;da;de;en;es;eu;fa;fr;gl;gr;hu;id;it;ja;ko;lt;nl;pl;pt;pt-BR;ro;ru;sk;sq;sv;th;tr;uk;zh-CN;zh-HK;zh-TW;hr;hy;ka".Split(';'),
            language_iso639) < 0)
        {
            // Fallback to default language.
            return "en";
        }

        return language_iso639;
    }

    public static string ToOptionValue(this CultureInfo culture, Encoding? encoding = null)
    {
        // https://github.com/MediaArea/MediaInfo/tree/master/Source/Resource/Plugin/Language
        string name = $"MediaInfoNet.Resource.Plugin.Language.{culture.ToOption()}.csv";
        using Stream stream = typeof(MediaInfo).Assembly.GetManifestResourceStream(name);
        using StreamReader reader = new(stream, encoding ?? Encoding.UTF8);
        StringBuilder result = new();
        char[] buffer = new char[(int)reader.BaseStream.Length];
        int count;

        while ((count = reader.Read(buffer, 0, buffer.Length)) > 0)
        {
            result.Append(buffer, 0, count);
        }

        return result.ToString();
    }
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
