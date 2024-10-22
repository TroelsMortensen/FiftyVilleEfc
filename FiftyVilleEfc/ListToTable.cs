using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Xunit.Abstractions;

namespace FiftyVilleEfc;

public static class ListToTable
{
    public static string Format<T>(IEnumerable<T> input)
    {
        List<T> elements = input.ToList();
        ImmutableArray<PropertyInfo> properties = ExtractPropertiesFromElementType<T>();
        Dictionary<string, int> columnLengths = CalculateColumnWidths(elements, properties);

        string tableHeader = CreateTableHeader(properties, columnLengths);


        string top = "";
        string bottom = "";
        for (int i = 0; i < tableHeader.Length; i++)
        {
            top += "_";
            bottom += "-";
        }

        tableHeader += "\n" + bottom;
        string table = CreateTable(elements, tableHeader, properties, columnLengths);

        return "\n" + top + "\n"
               + table + "\n"
               + bottom + "\n";
    }

    private static ImmutableArray<PropertyInfo> ExtractPropertiesFromElementType<T>()
        => typeof(T).GetProperties()
            .Where(info =>
                info.PropertyType.IsValueType
                || info.PropertyType.IsPrimitive
                || info.PropertyType == typeof(string))
            .ToImmutableArray();

    private static string CreateTable<T>(
        IEnumerable<T> list,
        string tableHeader,
        ImmutableArray<PropertyInfo> properties,
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
        => rowItem + Enumerable.Range(0, numOfSpaces + 1)
            .Select(_ => " ")
            .Aggregate((acc, input) => acc + input);

    private static Dictionary<string, int> CalculateColumnWidths<T>(IEnumerable<T> elements, ImmutableArray<PropertyInfo> properties)
        => properties.Select(
                prop =>
                (
                    Name: prop.Name,
                    Length: FindMaxColumnWidth(elements, prop)
                ))
            .ToDictionary(tuple => tuple.Name, tuple => tuple.Length);

    private static int FindMaxColumnWidth<T>(IEnumerable<T> list, PropertyInfo prop)
        => Math.Max(
            prop.Name.Length,
            list.Max(element => PropertyNameLengthOrZero(prop, element))
        );

    private static int PropertyNameLengthOrZero<T>(PropertyInfo prop, T element)
        => prop.GetValue(element)?.ToString()?.Length ?? 0;

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

    private static int GetPropValueLength<T>(PropertyInfo prop, T item)
        => prop.GetValue(item) is null ? 0
            : prop.GetValue(item)!.ToString() is null ? 0
            : prop.GetValue(item)!.ToString()!.Length;

    private static Dictionary<string, int> CalculateHeaderWidths<T>(ImmutableArray<PropertyInfo> properties)
        => properties.ToDictionary(propInfo => propInfo.Name, propInfo => propInfo.Name.Length);
}

public static class UtilExtensions
{
    public static string StringJoin(this IEnumerable<string> list, char separator)
        => string.Join(separator, list);
}