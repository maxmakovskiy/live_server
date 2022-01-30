using System.Text.RegularExpressions;

namespace liveserver.app {

public static class FileUtils {

    public static string[] AbsolutePathToFiles(string targetDir)
    {
        var result = new List<string>();

        foreach(string filename in Directory.GetFiles(targetDir, "", SearchOption.AllDirectories)) 
        {
            result.Add(filename);
        }

        return result.ToArray();
    }

    public static string LoadAndInject(string filename) 
    {
        string injection = LoadFileAsOneliner("/home/xemerius/devs/live_server/liveserver.app/src/to_inject.js");

        var result = new List<string>();
        foreach(string line in File.ReadAllLines(filename)) 
        {
            if (Regex.IsMatch(line, $"<body>.*</body>")) {
                string internalContent = line.Trim(new char[] {'<', '>', 'b', 'o', 'd', 'y', '/'});
                result.Add("<body>");
                result.Add(internalContent);
                result.Add(injection);
                result.Add("</body>");
                continue;
            }
            
            if (Regex.IsMatch(line, $".*</body>.*")) {
                result.Add("<script>");
                result.Add(injection);
                result.Add("</script>");
            }

            result.Add(line.Trim(' '));
        }

        return String.Join("\n", result.ToArray());
    }

    public static string LoadFileAsOneliner(string filename) 
    {
        string result = "";
        foreach(string line in File.ReadAllLines(filename))
        {
            result += line.Trim(' ');
        }

        return result;
    }

    public static Tuple<string, string> SplitToDirAndFile(string fullPath)
    {
        string targetDir = "";
        string targetFile = "";

        string source = ReverseString(fullPath);
        int i = 0;
        for (; i < source.Length; ++i) {
            if (source[i] == '/') {
                ++i;
                break;
            }
            targetFile += source[i];
        }

        for (; i < source.Length; ++i) {
            targetDir += source[i];
        }

        return Tuple.Create(
            ReverseString(targetDir),
            ReverseString(targetFile)
        );
    }

    private static string ReverseString(string input) 
    {
        return new string(input.Reverse().ToArray());
    }



}

}