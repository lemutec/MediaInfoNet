/*  Copyright (c) MediaArea.net SARL. All Rights Reserved.
 *
 *  Use of this source code is governed by a BSD-style license that can
 *  be found in the License.html file in the root of the source tree.
 */

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
// Microsoft Visual C# example
//
// To make this example working, you must put MediaInfo.Dll and Example.ogg
// in the "./Bin/__ConfigurationName__" folder
// and add MediaInfoDll.cs to your project
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

using MediaInfoLib;
using System.ComponentModel;

namespace MediaInfoLib_MSCS;

/// <summary>
/// Summary description for Form1.
/// </summary>
public class Form1 : Form
{
    private RichTextBox richTextBox1 = null!;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private Container components = null!;

    public Form1()
    {
        //
        // Required for Windows Form Designer support
        //
        InitializeComponent();

        //
        // TODO: Add any constructor code after InitializeComponent call
        //
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Methode requise pour la prise en charge du concepteur - ne modifiez pas
    /// le contenu de cette méthode avec l'éditeur de code.
    /// </summary>
    private void InitializeComponent()
    {
        this.richTextBox1 = new RichTextBox();
        this.SuspendLayout();
        //
        // richTextBox1
        //
        this.richTextBox1.Location = new Point(0, 0);
        this.richTextBox1.Name = "richTextBox1";
        this.richTextBox1.Size = new Size(768, 512);
        this.richTextBox1.TabIndex = 0;
        this.richTextBox1.Text = "";
        //
        // Form1
        //
        this.ClientSize = new Size(770, 514);
        this.Controls.Add(this.richTextBox1);
        this.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.Name = "Form1";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "How to use MediaInfo.Dll";
        this.Load += Form1_Load;
        this.ResumeLayout(false);
    }

    #endregion Windows Form Designer generated code

    /// <summary>
    /// The main entry point for the application.
    /// </summary>

    private void Form1_Load(object? sender, EventArgs e)
    {
        //Test if version of DLL is compatible : 3rd argument is "version of DLL tested;Your application name;Your application version"
        string ToDisplay;
        using MediaInfo MI = new();

        ToDisplay = MI.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
        if (ToDisplay.Length == 0)
        {
            richTextBox1.Text = "MediaInfo.Dll: this version of the DLL is not compatible";
            return;
        }

        //Information about MediaInfo
        ToDisplay += "\r\n\r\nInfo_Parameters\r\n";
        ToDisplay += MI.Option("Info_Parameters");

        ToDisplay += "\r\n\r\nInfo_Capacities\r\n";
        ToDisplay += MI.Option("Info_Capacities");

        ToDisplay += "\r\n\r\nInfo_Codecs\r\n";
        ToDisplay += MI.Option("Info_Codecs");

        //An example of how to use the library
        ToDisplay += "\r\n\r\nOpen\r\n";
        MI.Open("Example.ogg");

        ToDisplay += "\r\n\r\nInform with Complete=false\r\n";
        MI.Option("Complete");
        ToDisplay += MI.Inform();

        ToDisplay += "\r\n\r\nInform with Complete=true\r\n";
        MI.Option("Complete", "1");
        ToDisplay += MI.Inform();

        ToDisplay += "\r\n\r\nCustom Inform\r\n";
        MI.Option("Inform", "General;File size is %FileSize% bytes");
        ToDisplay += MI.Inform();

        ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='FileSize'\r\n";
        ToDisplay += MI.Get(0, 0, "FileSize");

        ToDisplay += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
        ToDisplay += MI.Get(0, 0, 46);

        ToDisplay += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
        ToDisplay += MI.Count_Get(StreamKind.Audio);

        ToDisplay += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
        ToDisplay += MI.Get(StreamKind.General, 0, "AudioCount");

        ToDisplay += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
        ToDisplay += MI.Get(StreamKind.Audio, 0, "StreamCount");

        ToDisplay += "\r\n\r\nClose\r\n";
        MI.Close();

        //Example with a stream
        //ToDisplay+="\r\n"+ExampleWithStream()+"\r\n";

        //Displaying the text
        richTextBox1.Text = ToDisplay;
    }

    private string ExampleWithStream()
    {
        //Initilaizing MediaInfo
        MediaInfo MI = new MediaInfo();

        //From: preparing an example file for reading
        FileStream From = new FileStream("Example.ogg", FileMode.Open, FileAccess.Read);

        //From: preparing a memory buffer for reading
        byte[] From_Buffer = new byte[64 * 1024];
        int From_Buffer_Size; //The size of the read file buffer

        //Preparing to fill MediaInfo with a buffer
        MI.Open_Buffer_Init(From.Length, 0);

        //The parsing loop
        do
        {
            //Reading data somewhere, do what you want for this.
            From_Buffer_Size = From.Read(From_Buffer, 0, 64 * 1024);

            //Sending the buffer to MediaInfo
            System.Runtime.InteropServices.GCHandle GC = System.Runtime.InteropServices.GCHandle.Alloc(From_Buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
            nint From_Buffer_nint = GC.AddrOfPinnedObject();
            Status Result = (Status)MI.Open_Buffer_Continue(From_Buffer_nint, From_Buffer_Size);
            GC.Free();
            if ((Result & Status.Finalized) == Status.Finalized)
                break;

            //Testing if MediaInfo request to go elsewhere
            if (MI.Open_Buffer_Continue_GoTo_Get() != -1)
            {
                Int64 Position = From.Seek(MI.Open_Buffer_Continue_GoTo_Get(), SeekOrigin.Begin); //Position the file
                MI.Open_Buffer_Init(From.Length, Position); //Informing MediaInfo we have seek
            }
        }
        while (From_Buffer_Size > 0);

        //Finalizing
        MI.Open_Buffer_Finalize(); //This is the end of the stream, MediaInfo must finnish some work

        //Get() example
        return "Container format is " + MI.Get(StreamKind.General, 0, "Format");
    }
}
