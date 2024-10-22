using System.Reflection;

namespace FiftyVilleEfc;

public static class ListToTable
{
    public static string Format<T>(IEnumerable<T> list)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        Dictionary<string, int> columnLengths = InitializeInfo(list, properties);
        string tableHeader = CreateTableHeader(properties, columnLengths);


        string top = "";
        string bottom = "";
        for (int i = 0; i < tableHeader.Length; i++)
        {
            top += "_";
            bottom += "-";
        }

        tableHeader += bottom;
        string table = CreateTable(list, tableHeader, properties, columnLengths);

        return "\n" + top
                    + table
                    + bottom + "\n";
        // Console.WriteLine("\n" + top);
        // Console.WriteLine(table);
        // Console.WriteLine(bottom + "\n");
    }

    private static string CreateTable<T>(
        IEnumerable<T> list,
        string tableHeader,
        PropertyInfo[] properties,
        Dictionary<string, int> columnLengths
    )
    {
        string table = tableHeader + "\n";

        IEnumerable<string> rows = list.Select(item => CreateTableRow(properties, columnLengths, item));
        string mainTable = string.Join("\n", rows);
        // foreach (T item in list)
        // {
            // string row = CreateTableRow(properties, columnLengths, item);
            // table += row + "\n";
        // }
        table += mainTable;
        // table = table.TrimEnd('\n');
        return table;
    }

    private static string CreateTableRow<T>(IEnumerable<PropertyInfo> properties, IReadOnlyDictionary<string, int> columnLengths, T item)
    {
        string row = "";
        foreach (PropertyInfo prop in properties)
        {
            int targetLength = columnLengths[prop.Name];
            string rowItem = prop.GetValue(item)?.ToString() ?? "null";
            rowItem = PadWithEmptySpace(rowItem, targetLength - rowItem.Length);

            row += rowItem + "| ";
        }

        row = row.TrimEnd('|');
        return row;
    }

    private static string PadWithEmptySpace(string rowItem, int numOfSpaces)
    {
        string pad = "";    
        for (int i = 0; i < numOfSpaces; i++)
        {
            pad += " ";
        }

        return rowItem + pad;
    }

    private static string CreateTableHeader(IEnumerable<PropertyInfo> properties, IReadOnlyDictionary<string, int> columnLengths)
    {
        string tableHeader = "";
        foreach (PropertyInfo prop in properties)
        {
            string headerItem = prop.Name;
            while (headerItem.Length <= columnLengths[prop.Name])
            {
                headerItem += " ";
            }

            tableHeader += headerItem + "| ";
        }

        tableHeader = tableHeader.TrimEnd('|');
        return tableHeader;
    }

    private static Dictionary<string, int> InitializeInfo<T>(IEnumerable<T> list, PropertyInfo[] properties)
    {
        Dictionary<string, int> columnLengths = InitializeWithHeaderLengths<T>(properties);

        // Finding column lengths
        foreach (T item in list)
        {
            foreach (PropertyInfo prop in properties)
            {
                if (prop.GetValue(item) == null)
                {
                    continue;
                }

                columnLengths[prop.Name] = Math.Max(
                    columnLengths[prop.Name],
                    GetPropValueLength(prop, item)
                );
            }
        }

        return columnLengths;
    }

    private static int GetPropValueLength<T>(PropertyInfo prop, T item)
        => prop.GetValue(item) is null ? 0
            : prop.GetValue(item)!.ToString() is null ? 0
            : prop.GetValue(item)!.ToString()!.Length;

    private static Dictionary<string, int> InitializeWithHeaderLengths<T>(PropertyInfo[] properties)
        => properties.ToDictionary(propInfo => propInfo.Name, propInfo => propInfo.Name.Length);
}