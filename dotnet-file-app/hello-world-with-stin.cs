if (Console.IsInputRedirected)
{
    string input = Console.In.ReadToEnd();

    Console.WriteLine($"Hello {input}!");
}
else
{
    Console.WriteLine("Hello world!");
}
