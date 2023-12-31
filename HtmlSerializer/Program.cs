// See https://aka.ms/new-console-template for more information
using HtmlSerializer;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var html = await Load("https://moodle.malkabruk.co.il/mod/book/view.php?id=100&chapterid=123");
var claenHtml = new Regex("\\s").Replace(html, "");
var htmlLines = new Regex("<(.*?)>").Split(claenHtml).Where(s => s.Length > 0);
//var attribute = new Regex("([^\\s]^?)=\"(.*?)\"").Matches();
var rootElement = new HtmlElement();
var currentElement = rootElement;
var allTags = HtmlHelper.Instance.HtmlAllTags;
var htmlTagsWithoutClose = HtmlHelper.Instance.HtmlTagsWithoutClose;

foreach (var htmlLine in htmlLines)
{
    string currentWord = new Regex("(.*)[^=]").Match(htmlLine).Value;
    if (currentWord == "/html")
    {
        break;
    }
    else if (currentWord.StartsWith("/") && currentElement.Parent is not null)
    {
        currentElement = currentElement.Parent;
    }
    else if (allTags.Contains(currentWord))
    {
        HtmlElement newElement = new HtmlElement();
        currentElement.Children.Add(newElement);
        newElement.Name = currentWord;
        var attributes = new Regex("([^\\s]^?)=\"(.*?)\"").Matches(htmlLine);
        foreach (var item in attributes)
        {
            newElement.Attributes.Add(item.ToString());
            if (item.ToString() == "Id")
                newElement.ID = item.ToString();
        }
        var classAttribute = new Regex("class=\"(.*?)\"").Match(attributes.ToString());
        var classAttributes = new Regex("[^class=]").Matches(classAttribute.ToString());
        foreach (var item in classAttributes)
        {
            newElement.Classes.Add(item.ToString());
        }
        newElement.Parent = currentElement;
        if (!(htmlLine.EndsWith("/") || htmlTagsWithoutClose.Contains(currentWord)))
        {
            currentElement = newElement;

            if (rootElement is null)
            {
                rootElement = currentElement;
            }
        }
    }
    else
    {
        currentElement.InnerHtml += htmlLine;
    }
}
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

Console.ReadKey();

