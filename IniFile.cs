using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{
    string Path;
    string? EXE = Assembly.GetExecutingAssembly().GetName().Name;

    // Create AppData Folder
    string AppData = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{Assembly.GetExecutingAssembly().GetName().Name}";

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    public IniFile(params (string Key, string Value, string Section)[] defaultValues)
    {
        if (!Directory.Exists(AppData))
        {
            Directory.CreateDirectory(AppData);
        }

        Path = new FileInfo($"{AppData}\\{EXE}.ini").FullName;

        foreach (var (Key, Value, Section) in defaultValues)
        {
            if (!KeyExists(Key, Section))
            {
                Write(Key, Value, Section);
            }
        }
    }

    public string Read(string Key, string? Section)
    {
        var RetVal = new StringBuilder(255);
        GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
        return RetVal.ToString();
    }

    public void Write(string? Key, string? Value, string? Section)
    {
        WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
    }

    public void DeleteKey(string Key, string? Section)
    {
        Write(Key, null, Section ?? EXE);
    }

    public void DeleteSection(string? Section)
    {
        Write(null, null, Section ?? EXE);
    }

    public bool KeyExists(string Key, string? Section)
    {
        return Read(Key, Section).Length > 0;
    }
}
