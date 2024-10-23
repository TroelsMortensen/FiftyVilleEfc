using System.Collections.Immutable;
using System.Reflection;
using System.Text;

namespace FiftyVilleEfc;

public static class ListToTable
{
    public static string Format<T>(IEnumerable<T> input)
    {
        ImmutableList<T> elements = input.ToImmutableList();

        ImmutableArray<PropertyInfo> properties = ExtractPropertiesFromElementType<T>();
        Dictionary<string, int> columnWidths = CalculateColumnWidths(elements, properties);

        string tableHeader = CreateTableHeader(properties, columnWidths);

        string dividerLine = CreateDividerLine(tableHeader.Length);

        string table = CreateTable(elements, properties, columnWidths);

        return new StringBuilder()
            .AppendLine(dividerLine)
            .AppendLine(tableHeader)
            .AppendLine(dividerLine)
            .AppendLine(table)
            .AppendLine(dividerLine)
            .ToString();
    }

    private static string CreateDividerLine(int length)
        => Enumerable.Range(0, length - 1)
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

    private static string CreateSingleRow<T>(
        ImmutableArray<PropertyInfo> properties,
        IReadOnlyDictionary<string, int> columnLengths,
        T item)
        => properties.Aggregate("| ", (acc, prop) =>
            acc +
            (prop.GetValue(item)?.ToString() ?? "null")
            .SuffixUpToTargetWithEmptySpaces(columnLengths[prop.Name])
            + "| "
        );

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
        => properties.Aggregate("| ", (acc, property) =>
            acc + property.Name
                    .SuffixUpToTargetWithEmptySpaces(columnLengths[property.Name])
                + "| "
        );
}

public static class UtilExtensions
{
    public static string StringJoin(this IEnumerable<string> list, char separator)
        => string.Join(separator, list);

    public static string SuffixUpToTargetWithEmptySpaces(this string self, int numberOfSpaces)
        => self + Enumerable.Range(0, numberOfSpaces + 1 - self.Length)
            .Aggregate("", (acc, _) => acc + " ");
}