using System.Diagnostics;

string? clipboard = ReadClipboardWithXClip();

if(clipboard != null)
{
    Console.WriteLine($"Hello {clipboard.Trim()}!");
}
else
{
    if (args.Length > 0)
        Console.WriteLine($"Hello {args[0]}!");
    else
        Console.WriteLine("Hello world!");
}

static string? ReadClipboardWithXClip()
{
    try
    {
        ProcessStartInfo processStartInfo = new()
        {
            FileName = "xclip",
            Arguments = "-selection clipboard -o", // Read from clipboard
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            Console.Error.WriteLine($"xclip error: {error}");

            return null;
        }

        return string.IsNullOrWhiteSpace(output) ? null : output;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error reading clipboard: {ex.Message}");

        return null;
    }
}
