using System.Text;

namespace InMemoryOne.Models;

public class Blog
{
    public int? BlogId { get; set; }

    public string? Permalink { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append($"{nameof(BlogId)}: `{BlogId}`");
        sb.Append($", {nameof(Permalink)}: `{Permalink}`");

        return sb.ToString();
    }
}

