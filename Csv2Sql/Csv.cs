using System.Text;

namespace Csv2Sql;

public static class Csv
{
    public static string[] ParseLine(string row, char delimiter)
    {
        var result = new List<string>();
        var itemBuilder = new StringBuilder();
        var awaitingClosingTag = false;

        void AddToResult()
        {
            result ??= new List<string>();
            itemBuilder ??= new StringBuilder();

            result.Add(itemBuilder.ToString().Trim('"').Trim());
            itemBuilder.Clear();
        }

        for (var i = 0; i < row.Length; i++)
        {
            if (i == 0 && row[i] == '"')
            {
                awaitingClosingTag = true;
                itemBuilder.Append(row[i]);
                continue;
            }
            
            if (i == 0 && row[i] == delimiter)
            {
                AddToResult();
                continue;
            }
            
            if (row[i] != delimiter)
            {
                itemBuilder.Append(row[i]);
                continue;
            }

            if (row.Length > i + 1 && row[i + 1] == '"')
            {
                awaitingClosingTag = true;
                AddToResult();
                continue;
            }
            
            if (awaitingClosingTag && row[i - 1] == '"')
            {
                awaitingClosingTag = false;
                AddToResult();
                continue;
            }

            if (awaitingClosingTag)
            {
                itemBuilder.Append(row[i]);
                continue;
            }

            if (row.Length > 0 && row[i - 1] == delimiter)
            {
                AddToResult();
                continue;
            }

            AddToResult();
        }

        AddToResult();

        return result.ToArray();
    }
}