# stupid-`for`-loop tricks: finding repeating substrings

Is your string made up of repeating substrings?

## testing for repeating substring

```csharp
public static bool IsRepeatedSubstringPattern(string s) {
    int n = s.Length;
    
    // The largest possible repeating substring length is n / 2
    for (int len = 1; len <= n / 2; len++) {
        if (n % len == 0) { // Substring must divide evenly
            int left = 0;
            int right = len;
            bool match = true;

            // Two pointers verifying segments match the prefix
            while (right < n) {
                if (s[left] != s[right]) {
                    match = false;
                    break;
                }
                left++;
                right++;
            }

            if (match) return true;
        }
    }

    return false;
}

```

```csharp
IsRepeatedSubstringPattern("ABABAB")
```

> Output:
>
> ```
> True
> ```

```csharp
IsRepeatedSubstringPattern("AAAAAB")
```

> Output:
>
> ```
> False
> ```

```csharp
IsRepeatedSubstringPattern("AAA")
```

> Output:
>
> ```
> True
> ```

## getting the repeating substring

```csharp
public static (string? substring, int count) GetRepeatingSubstringResult(string s)
{
    if (string.IsNullOrEmpty(s)) return default;

    int inputLength = s.Length;
    bool isIterationLessThanOrEqualHalfInputLength(int i) => i <= inputLength / 2;
    bool isIterationMultipleInputLength(int i) => inputLength % i == 0;

    for (int i = 1; isIterationLessThanOrEqualHalfInputLength(i); i++)
    {
        if (!isIterationMultipleInputLength(i)) continue;

        string candidate = s.Substring(0, i);

        if(!IsCandidateConcatenatedEqualToOriginalInput(s, candidate, inputLength, i)) continue;
        
        return (candidate, inputLength / i);
    }

    return default;
}

public static bool IsCandidateConcatenatedEqualToOriginalInput(string input, string candidate, int inputLength, int i)
{
    int repeatCount = inputLength / i;

    string concatenated = string.Concat(Enumerable.Repeat(candidate, repeatCount));
    
    return (concatenated == input);
}

```

```csharp
GetRepeatingSubstringResult("ABCABCABC")
```

> Output (HTML):
>
> <style>.verso-fsharp-output{--fsharp-bg:var(--vscode-editor-background,var(--verso-cell-output-background,#fff));--fsharp-fg:var(--vscode-editor-foreground,var(--verso-cell-output-foreground,#1e1e1e));--fsharp-border:var(--vscode-editorWidget-border,var(--verso-border-default,#e0e0e0));--fsharp-header-bg:var(--vscode-editorWidget-background,var(--verso-cell-background,#f5f5f5));--fsharp-hover:var(--vscode-list-hoverBackground,var(--verso-cell-hover-background,#f0f0f0));--fsharp-muted:var(--vscode-descriptionForeground,var(--verso-editor-line-number,#858585));--fsharp-error-bg:var(--vscode-inputValidation-errorBackground,var(--verso-status-error,#fde7e9));--fsharp-ok-bg:var(--vscode-inputValidation-infoBackground,var(--verso-status-success,#e6f4ea));font-family:var(--verso-code-output-font-family,monospace);font-size:13px;color:var(--fsharp-fg);}.verso-fsharp-output table{border-collapse:collapse;width:auto;background:var(--fsharp-bg);color:var(--fsharp-fg);}.verso-fsharp-output th{text-align:left;padding:6px 12px;border-bottom:2px solid var(--fsharp-border);background:var(--fsharp-header-bg);font-weight:600;}.verso-fsharp-output td{padding:5px 12px;border-bottom:1px solid var(--fsharp-border);}.verso-fsharp-output tbody tr:hover{background:var(--fsharp-hover);}.verso-fsharp-output .verso-fsharp-none{color:var(--fsharp-muted);font-style:italic;}.verso-fsharp-output .verso-fsharp-ok{padding:2px 6px;background:var(--fsharp-ok-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-error{padding:2px 6px;background:var(--fsharp-error-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-footer{padding:6px 0;color:var(--fsharp-muted);font-size:12px;}</style><div class="verso-fsharp-output">(<span title="String">ABC</span>, <span title="Int32">3</span>)</div>

```csharp
GetRepeatingSubstringResult("AAAAAB")
```

> Output (HTML):
>
> <style>.verso-fsharp-output{--fsharp-bg:var(--vscode-editor-background,var(--verso-cell-output-background,#fff));--fsharp-fg:var(--vscode-editor-foreground,var(--verso-cell-output-foreground,#1e1e1e));--fsharp-border:var(--vscode-editorWidget-border,var(--verso-border-default,#e0e0e0));--fsharp-header-bg:var(--vscode-editorWidget-background,var(--verso-cell-background,#f5f5f5));--fsharp-hover:var(--vscode-list-hoverBackground,var(--verso-cell-hover-background,#f0f0f0));--fsharp-muted:var(--vscode-descriptionForeground,var(--verso-editor-line-number,#858585));--fsharp-error-bg:var(--vscode-inputValidation-errorBackground,var(--verso-status-error,#fde7e9));--fsharp-ok-bg:var(--vscode-inputValidation-infoBackground,var(--verso-status-success,#e6f4ea));font-family:var(--verso-code-output-font-family,monospace);font-size:13px;color:var(--fsharp-fg);}.verso-fsharp-output table{border-collapse:collapse;width:auto;background:var(--fsharp-bg);color:var(--fsharp-fg);}.verso-fsharp-output th{text-align:left;padding:6px 12px;border-bottom:2px solid var(--fsharp-border);background:var(--fsharp-header-bg);font-weight:600;}.verso-fsharp-output td{padding:5px 12px;border-bottom:1px solid var(--fsharp-border);}.verso-fsharp-output tbody tr:hover{background:var(--fsharp-hover);}.verso-fsharp-output .verso-fsharp-none{color:var(--fsharp-muted);font-style:italic;}.verso-fsharp-output .verso-fsharp-ok{padding:2px 6px;background:var(--fsharp-ok-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-error{padding:2px 6px;background:var(--fsharp-error-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-footer{padding:6px 0;color:var(--fsharp-muted);font-size:12px;}</style><div class="verso-fsharp-output">(<span title="String"><span class="verso-fsharp-none">null</span></span>, <span title="Int32">0</span>)</div>

```csharp
GetRepeatingSubstringResult("AAA")
```

> Output (HTML):
>
> <style>.verso-fsharp-output{--fsharp-bg:var(--vscode-editor-background,var(--verso-cell-output-background,#fff));--fsharp-fg:var(--vscode-editor-foreground,var(--verso-cell-output-foreground,#1e1e1e));--fsharp-border:var(--vscode-editorWidget-border,var(--verso-border-default,#e0e0e0));--fsharp-header-bg:var(--vscode-editorWidget-background,var(--verso-cell-background,#f5f5f5));--fsharp-hover:var(--vscode-list-hoverBackground,var(--verso-cell-hover-background,#f0f0f0));--fsharp-muted:var(--vscode-descriptionForeground,var(--verso-editor-line-number,#858585));--fsharp-error-bg:var(--vscode-inputValidation-errorBackground,var(--verso-status-error,#fde7e9));--fsharp-ok-bg:var(--vscode-inputValidation-infoBackground,var(--verso-status-success,#e6f4ea));font-family:var(--verso-code-output-font-family,monospace);font-size:13px;color:var(--fsharp-fg);}.verso-fsharp-output table{border-collapse:collapse;width:auto;background:var(--fsharp-bg);color:var(--fsharp-fg);}.verso-fsharp-output th{text-align:left;padding:6px 12px;border-bottom:2px solid var(--fsharp-border);background:var(--fsharp-header-bg);font-weight:600;}.verso-fsharp-output td{padding:5px 12px;border-bottom:1px solid var(--fsharp-border);}.verso-fsharp-output tbody tr:hover{background:var(--fsharp-hover);}.verso-fsharp-output .verso-fsharp-none{color:var(--fsharp-muted);font-style:italic;}.verso-fsharp-output .verso-fsharp-ok{padding:2px 6px;background:var(--fsharp-ok-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-error{padding:2px 6px;background:var(--fsharp-error-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-footer{padding:6px 0;color:var(--fsharp-muted);font-size:12px;}</style><div class="verso-fsharp-output">(<span title="String">A</span>, <span title="Int32">3</span>)</div>

```csharp
GetRepeatingSubstringResult("ABC")
```

> Output (HTML):
>
> <style>.verso-fsharp-output{--fsharp-bg:var(--vscode-editor-background,var(--verso-cell-output-background,#fff));--fsharp-fg:var(--vscode-editor-foreground,var(--verso-cell-output-foreground,#1e1e1e));--fsharp-border:var(--vscode-editorWidget-border,var(--verso-border-default,#e0e0e0));--fsharp-header-bg:var(--vscode-editorWidget-background,var(--verso-cell-background,#f5f5f5));--fsharp-hover:var(--vscode-list-hoverBackground,var(--verso-cell-hover-background,#f0f0f0));--fsharp-muted:var(--vscode-descriptionForeground,var(--verso-editor-line-number,#858585));--fsharp-error-bg:var(--vscode-inputValidation-errorBackground,var(--verso-status-error,#fde7e9));--fsharp-ok-bg:var(--vscode-inputValidation-infoBackground,var(--verso-status-success,#e6f4ea));font-family:var(--verso-code-output-font-family,monospace);font-size:13px;color:var(--fsharp-fg);}.verso-fsharp-output table{border-collapse:collapse;width:auto;background:var(--fsharp-bg);color:var(--fsharp-fg);}.verso-fsharp-output th{text-align:left;padding:6px 12px;border-bottom:2px solid var(--fsharp-border);background:var(--fsharp-header-bg);font-weight:600;}.verso-fsharp-output td{padding:5px 12px;border-bottom:1px solid var(--fsharp-border);}.verso-fsharp-output tbody tr:hover{background:var(--fsharp-hover);}.verso-fsharp-output .verso-fsharp-none{color:var(--fsharp-muted);font-style:italic;}.verso-fsharp-output .verso-fsharp-ok{padding:2px 6px;background:var(--fsharp-ok-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-error{padding:2px 6px;background:var(--fsharp-error-bg);border-radius:3px;}.verso-fsharp-output .verso-fsharp-footer{padding:6px 0;color:var(--fsharp-muted);font-size:12px;}</style><div class="verso-fsharp-output">(<span title="String"><span class="verso-fsharp-none">null</span></span>, <span title="Int32">0</span>)</div>

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
