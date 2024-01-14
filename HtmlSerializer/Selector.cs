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
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        public Selector()
        {

        }

        public static Selector GetSelector(string str)
        {
            Selector rootSelector = null, currentSelector = null;
            var allTags = HtmlHelper.Instance.HtmlAllTags;
            var htmlTagsWithoutClose = HtmlHelper.Instance.HtmlTagsWithoutClose;
            var arrStr = str.Split(" ");
            foreach (var item in arrStr)
            {
                Selector newSelector = new Selector();
                string[] strSelector = item.Split(new char[] { '#', '.' }).ToArray();
                if (strSelector[0] != "" && (allTags.Contains(strSelector[0]) || (htmlTagsWithoutClose.Contains(strSelector[0]))))
                {
                    newSelector.TagName = strSelector[0];
                }
                if (strSelector.Count() > 1)
                    newSelector.Id = strSelector[1];
                if (strSelector.Count() > 2)
                    newSelector.Classes.Add(strSelector[2]);
                if (currentSelector == null)
                {
                    currentSelector = new Selector();
                    newSelector.Parent = null;
                    rootSelector = newSelector;
                }
                else
                {
                    newSelector.Parent = currentSelector;
                    currentSelector.Child = newSelector;
                }

                currentSelector = newSelector;
            }
            return rootSelector;
        }
    }
}
