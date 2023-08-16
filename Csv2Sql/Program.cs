using System.Text;

namespace Csv2Sql;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (!args.Any())
            {
                Console.WriteLine("csv2sql has 2 modes: 'standard' and 'aggregate'. In 'standard' mode you parse and map a CSV file into rows of SQL statements. In 'aggregate' mode you choose a column you wish to aggregate into a comma separated line.");
                Console.WriteLine();
                Console.WriteLine("In 'standard' mode at least 2 arguments are required:");
                Console.WriteLine("  1: the path to the CSV to be processed");
                Console.WriteLine(
                    "  2: a string representation of the output (i.e 'UPDATE Table1 SET Val1 = '@0', Val2 = @1 WHERE Id = @2'). Each column in the CSV is extracted with @XX syntax.");
                Console.WriteLine(
                    "  3: <optional> whether or not the header row should be skipped (true|false). Defaults to 'false'.");
                Console.WriteLine("  4: <optional> the delimiter to be used when parsing the CSV. Defaults to ','.");
                Console.WriteLine(@"Example: './csv2sql example.csv ""SELECT @1"" true "";""'");
                Console.WriteLine();
                Console.WriteLine("In 'aggregate' mode your first argument is --aggregate followed by 3 additional arguments:");
                Console.WriteLine("  1: the path to the CSV to be processed");
                Console.WriteLine("  2: the number of the column to be parsed (zero indexed)");
                Console.WriteLine(
                    "  3: <optional> whether or not the header row should be skipped (true|false). Defaults to 'false'.");
                Console.WriteLine("  4: <optional> the delimiter to be used when parsing the CSV. Defaults to ','.");
                Console.WriteLine(@"Example: './csv2sql --aggregate example.csv 2'");
                
                return;
            }

            if (args[0] == "--aggregate")
            {
                ExecuteAggregate(args);
            }
            else
            {
                ExecuteStandard(args);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }

    private static void ExecuteStandard(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("At least 2 arguments are needed!");
        }
        
        var csvPath = args[0].TrimStart('"').TrimEnd('"');
        var sqlTemplate = args[1].TrimStart('"').TrimEnd('"');
        var skipHeader = false;
        if (args.Length >= 3 && bool.TryParse(args[2].TrimStart('"').TrimEnd('"'), out skipHeader))
        {
        }

        var delimiter = args.Length >= 4 ? args[3].TrimStart('"').TrimEnd('"') : ",";

        using FileStream fs = File.Open(csvPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using BufferedStream bs = new BufferedStream(fs);
        using StreamReader sr = new StreamReader(bs);

        var lineCount = 0;
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            if (!skipHeader || lineCount > 0)
            {
                var columns = Csv.ParseLine(line, delimiter[0]);
                var result = new StringBuilder(sqlTemplate);

                for (var colIndex = columns.Length - 1; colIndex >= 0; colIndex--)
                {
                    result.Replace($"@{colIndex}", columns[colIndex].TrimStart('"').TrimEnd('"'));
                }

                Console.WriteLine(result.ToString());
            }

            lineCount++;
        }
    }

    private static void ExecuteAggregate(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("At least 3 arguments are needed, the first being --aggregate");
        }
        
        var csvPath = args[1].TrimStart('"').TrimEnd('"');
        var extractFromColIndex = int.Parse(args[2].TrimStart('"').TrimEnd('"'));
        var skipHeader = false;
        if (args.Length >= 4 && bool.TryParse(args[3].TrimStart('"').TrimEnd('"'), out skipHeader))
        {
        }
        var delimiter = args.Length >= 5 ? args[4].TrimStart('"').TrimEnd('"') : ",";
        
        using FileStream fs = File.Open(csvPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using BufferedStream bs = new BufferedStream(fs);
        using StreamReader sr = new StreamReader(bs);

        var lineCount = 0;
        var values = new List<string>();
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            if (!skipHeader || lineCount > 0)
            {
                var columns = Csv.ParseLine(line, delimiter[0]);
                values.Add(columns[extractFromColIndex].TrimStart('"').TrimEnd('"'));
            }

            lineCount++;
        }

        Console.WriteLine(string.Join(",", values));
    }
}