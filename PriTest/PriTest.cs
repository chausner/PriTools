using PriFormat;
using System.Diagnostics;

namespace PriTest;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("PriTest <one or more paths to .pri files or paths to folders containing .pri files>");
            return;
        }

        List<string> paths = [];

        foreach (string path in args)            
            if (File.Exists(path))
                paths.Add(path);
            else if (Directory.Exists(path))
                paths.AddRange(Directory.GetFiles(path, "*.pri", SearchOption.AllDirectories));
            else
                throw new Exception(string.Format("Path does not exist: {0}", path));

        List<(string Path, string Exception)> errors = [];

        foreach (string path in paths)
        {
            try
            {
                using Stream stream = File.OpenRead(path);
                PriFile priFile = PriFile.Parse(stream);

                var unknownSections = priFile.Sections.OfType<UnknownSection>();

                if (unknownSections.Any())
                    throw new Exception(string.Format("Unknown sections: {0}", string.Join(' ', unknownSections.Select(s => s.SectionIdentifier))));
            }
            catch (Exception e) when (!Debugger.IsAttached)
            {
                Console.WriteLine("Error: {0}", path);
                Console.WriteLine(e);
                errors.Add((path, e.ToString()));
            }
        }

        Console.WriteLine("Done. {0} files loaded successfully. {1} files failed to load.", paths.Count - errors.Count, errors.Count);
        Console.WriteLine("Errors:");

        foreach (var errorGroup in errors.GroupBy(x => x.Exception).OrderByDescending(g => g.Count()).ThenBy(g => g.Key))
        {
            Console.WriteLine("{0}:", errorGroup.Key);

            foreach (var item in errorGroup.OrderBy(x => x.Path))                
                Console.WriteLine("  {0}", item.Path);

            Console.WriteLine();
        }
    }
}
