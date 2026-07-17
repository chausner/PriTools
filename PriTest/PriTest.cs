using PriFormat;
using System.Diagnostics;

namespace PriTest;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("PriTest [--validate-hschema-checksum] <one or more paths to .pri files or paths to folders containing .pri files>");
            return;
        }

        bool validateHSchemaChecksum = args[0] == "--validate-hschema-checksum";
        string[] pathArgs = validateHSchemaChecksum ? args[1..] : args[..];

        if (pathArgs.Length == 0)
        {
            Console.WriteLine("No paths specified.");
            return;
        }

        List<string> paths = [];

        foreach (string path in pathArgs)
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

                if (validateHSchemaChecksum)
                    ValidateHSchemaChecksum(priFile);
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

    private static void ValidateHSchemaChecksum(PriFile priFile)
    {
        foreach (HierarchicalSchemaSection schema in priFile.Sections.OfType<HierarchicalSchemaSection>())
            if (schema.Version is HierarchicalSchemaVersionInfo version)
            {
                uint computedChecksum = schema.ComputeHierarchicalSchemaVersionInfoChecksum();

                if (version.Checksum != computedChecksum)
                    throw new Exception(string.Format("Checksum mismatch: expected 0x{0:X8}, computed 0x{1:X8}",
                        version.Checksum, computedChecksum));
            }
    }
}
