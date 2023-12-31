using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlSerializer
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } = new List<string>();
        public HtmlElement Parent { get; set; } = new HtmlElement();
        public HtmlElement Child { get; set; } = new HtmlElement();


        static Selector GetSelector(string str)
        {
            var allTags = HtmlHelper.Instance.HtmlAllTags;
            var htmlTagsWithoutClose = HtmlHelper.Instance.HtmlTagsWithoutClose;
            string[] arr = str.Split(new char[] { '#', '.' }).ToArray();
            Selector selector = new Selector();

            if (arr[0] != "" && (allTags.Contains(arr[0]) || (htmlTagsWithoutClose.Contains(arr[0]))))
            {
                selector.TagName = arr[0];
            }
            selector.Id = arr[1];
            selector.Classes.Add(arr[2]);

            return selector;

        }
    }
}
