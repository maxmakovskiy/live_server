using System;
using System.Text;
using System.Text.RegularExpressions;

namespace liverserver.app {

public class FileSpectator {

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

}

}