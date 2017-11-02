using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Songhay.SyndicationOne
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            var items = new List<SyndicationItem>();

            using (var xmlReader = XmlReader.Create("http://scripting.com/rss.xml", new XmlReaderSettings { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();
                            items.Add(new SyndicationItem(item));
                            break;
                        default:
                            break;
                    }
                }
            }

            var str = new StringBuilder();
            str.Append("<ul>");
            foreach (var i in items)
            {
                str.Append($"<li>{i.Description}</li>");
            }
            str.Append("</ul>");

            var html = $@"
<html>
    <head>
        <link rel=""stylesheet"" type=""text/css"" href=""http://fonts.googleapis.com/css?family=Germania+One"">
        <style>
            body {{
            font-family: 'Germania One', serif;
            font-size: 24px;
        }}
        </style>
    </head>
    <body>
        {str.ToString()}
    </body>
</html>
";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "rss.html");
            File.WriteAllText(path, html);
        }
    }
}
