using System.Text.RegularExpressions;

namespace liveserver.app;

public static class CmdOptions
{
    public static bool IsValid(string[] cmd)
    {
        if (cmd.Length == 0) return false;

        string whole = String.Join(' ', cmd);
        string pattern = "(-target (\\w|\\S)+)";

        return Regex.IsMatch(whole, pattern);
    }
    
}