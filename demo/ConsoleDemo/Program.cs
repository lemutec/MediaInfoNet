using MediaInfoLib_MSCS;

if (Environment.GetCommandLineArgs().Count() >= 2)
{
    CLI.Main(new string[] { Environment.GetCommandLineArgs()[0] });
}
else
{
    CLI.Main(Environment.GetCommandLineArgs());
}
