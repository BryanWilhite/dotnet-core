# C#: 2D array—convert adjacency matrix to adjacency list

This tree structure can be translated to an adjacency matrix:

```plaintext
      A
     / \
    B   C
         \
          D
```

```csharp

int[,] adjacencyMatrix = new int[,]
{
    { 0, 1, 1, 0 },
    { 1, 0, 0, 0 },
    { 1, 0, 0, 1 },
    { 0, 0, 1, 0 },
};

string[] matrixLabels = new[] { "A", "B", "C", "D" };

KeyValuePair<string, string[]>[] adjacencyList = matrixLabels
    .Select(label => new KeyValuePair<string, string[]>(label, new string[0]))
    .ToArray();

var columnUpperBound = adjacencyMatrix.GetUpperBound(1);
var columnIndex = 0;
var rowIndex = 0;

//report header:
var sb = new StringBuilder("  ");
sb.AppendLine(matrixLabels.Aggregate((a, i) => $"{a} {i}"));

foreach (var element in adjacencyMatrix)
{
    if (adjacencyMatrix[rowIndex, columnIndex] == 1)
    {
        var label = matrixLabels[columnIndex];
        var appended = adjacencyList[rowIndex].Value.Union(new[] { label });
        adjacencyList[rowIndex] = new KeyValuePair<string, string[]>(
                adjacencyList[rowIndex].Key,
                appended.ToArray()
            );
    }

    //report body:
    var isAtBeginningOfColumn = (columnIndex == 0);
    var isAtEndOfColumn = (columnIndex > 0) && (columnIndex % columnUpperBound == 0);

    if (isAtBeginningOfColumn)
    {
        sb.Append($"{matrixLabels[rowIndex]} ");
    }

    if (isAtEndOfColumn)
    {
        sb.AppendLine($"{element}");

        columnIndex = 0;
        rowIndex++;
    }
    else
    {
        sb.Append($"{element} ");
        columnIndex++;
    }
}

Console.WriteLine(
    $"""

    report: {nameof(adjacencyMatrix)}

    {sb}
    """);

// adjacencyList.Dump(nameof(adjacencyList));
```

> Output:
>
> ```
> 
> report: adjacencyMatrix
> 
>   A B C D
> A 0 1 1 0
> B 1 0 0 0
> C 1 0 0 1
> D 0 0 1 0
> 
> 
> ```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

