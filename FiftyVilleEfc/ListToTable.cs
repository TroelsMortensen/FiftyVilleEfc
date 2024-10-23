using System.Collections.Immutable;
using System.Reflection;
using System.Text;

namespace FiftyVilleEfc;

public static class ListToTable
{
    public static string Format<T>(IEnumerable<T> input)
    {
        StringBuilder builder = new();
        ImmutableList<T> elements = input.ToImmutableList();

        ImmutableArray<PropertyInfo> properties = ExtractPropertiesFromElementType<T>();
        Dictionary<string, int> columnWidths = CalculateColumnWidths(elements, properties);

        string tableHeader = CreateTableHeader(properties, columnWidths);

        string dividerLine = CreateDividerLine(tableHeader.Length);

        string table = CreateTable(elements, properties, columnWidths);

        builder
            .AppendLine(dividerLine)
            .AppendLine(tableHeader)
            .AppendLine(dividerLine)
            .AppendLine(table)
            .AppendLine(dividerLine);

        return builder.ToString();
    }

    private static string CreateDividerLine(int length)
        => Enumerable.Range(0, length)
            .Aggregate("", (acc, _) => acc + "-");

    private static ImmutableArray<PropertyInfo> ExtractPropertiesFromElementType<T>()
        => typeof(T).GetProperties()
            .Where(info =>
                info.PropertyType.IsPrimitive ||
                info.PropertyType == typeof(string))
            .ToImmutableArray();

    private static string CreateTable<T>(
        IEnumerable<T> elements,
        ImmutableArray<PropertyInfo> properties,
        IReadOnlyDictionary<string, int> columnLengths
    )
        => elements.Select(
                element => CreateSingleRow(properties, columnLengths, element)
            )
            .StringJoin('\n');

    private static string CreateSingleRow<T>(IEnumerable<PropertyInfo> properties, IReadOnlyDictionary<string, int> columnLengths, T item)
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
            .Aggregate("", (acc, _) => acc + " ");

    private static Dictionary<string, int> CalculateColumnWidths<T>(IEnumerable<T> elements, ImmutableArray<PropertyInfo> properties)
        => properties.ToDictionary(
            prop => prop.Name,
            prop => FindMaxColumnWidth(elements, prop)
        );

    private static int FindMaxColumnWidth<T>(IEnumerable<T> list, PropertyInfo prop)
        => Math.Max(
            prop.Name.Length,
            list.Max(element => PropertyNameLengthOrZero(prop, element))
        );

    private static int PropertyNameLengthOrZero<T>(PropertyInfo prop, T element)
        => prop.GetValue(element)?
            .ToString()?
            .Length ?? 0;

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