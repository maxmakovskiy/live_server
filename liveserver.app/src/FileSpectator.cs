namespace liverserver.app {

public class FileSpectator {

    public static string[] AbsolutePathToFiles(string target)
    {
        var result = new List<string>();

        foreach(string filename in Directory.GetFiles(target, "", SearchOption.AllDirectories)) 
        {
            result.Add(filename);
        }

        return result.ToArray();
    }

}

}