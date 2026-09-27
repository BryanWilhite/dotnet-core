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

```csharp
GetRepeatingSubstringResult("AAAAAB")
```

```csharp
GetRepeatingSubstringResult("AAA")
```

```csharp
GetRepeatingSubstringResult("ABC")
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
