// See https://aka.ms/new-console-template for more information
using HtmlSerializer;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

var html = await Load("https://moodle.malkabruk.co.il/mod/book/view.php?id=100&chapterid=123");

var cleanHtml= Regex.Replace(html, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
var htmlLines = new Regex("<(.*?)>").Split(html).Where(s => s.Length > 0);
var rootElement = new HtmlElement();
var currentElement = rootElement;
var allTags = HtmlHelper.Instance.HtmlAllTags;
var htmlTagsWithoutClose = HtmlHelper.Instance.HtmlTagsWithoutClose;
foreach (var htmlLine in htmlLines)
{
    var currentWord = new Regex("^\\W*([\\w-]+)").Match(htmlLine).ToString();
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
        newElement.Name = currentWord;
        currentElement.Children.Add(newElement);
        var onlyAttributes = new Regex("( .*)").Match(htmlLine);
        var attributes = new Regex("(.*?)=\"(.*?)\"").Matches(onlyAttributes.ToString());
        foreach (var item in attributes)
        {
            newElement.Attributes.Add(item.ToString());
            var nameAttribute= new Regex("^\\W*([\\w-]+)").Match(item.ToString()).ToString();
            if (nameAttribute.ToString()==" id")
                newElement.ID = new Regex("(=.*)").Match(item.ToString()).ToString();
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

string str = "link";
HashSet<HtmlElement> hash = rootElement.GetElementsBySelector(Selector.GetSelector(str));
hash.ToList<HtmlElement>().ForEach(a => Console.WriteLine(a));


Console.ReadKey();

