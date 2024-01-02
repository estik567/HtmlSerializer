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
        public Selector Parent { get; set; } = new Selector();
        public Selector Child { get; set; } = new Selector();


        static Selector GetSelector(string str)
        {
            Selector rootSelector=null, currentSelector=new Selector();
            var allTags = HtmlHelper.Instance.HtmlAllTags;
            var htmlTagsWithoutClose = HtmlHelper.Instance.HtmlTagsWithoutClose;
            var arrStr = str.Split(" ");
            foreach(var item in arrStr)
            {
                Selector newSelector = new Selector();
                string[] strSelector = item.Split(new char[] { '#', '.' }).ToArray();
                if (strSelector[0] != "" && (allTags.Contains(strSelector[0]) || (htmlTagsWithoutClose.Contains(strSelector[0]))))
                {
                    newSelector.TagName = strSelector[0];
                }
                newSelector.Id = strSelector[1];
                newSelector.Classes.Add(strSelector[2]);
                newSelector.Parent = currentSelector;
                currentSelector.Child = newSelector;
                if(rootSelector == null) {
                    rootSelector = currentSelector;
                }
                currentSelector = newSelector;
            }
            return rootSelector;
        }

        //public override bool Equals(object? obj)
        //{
        //    return base.Equals(obj) && (obj is Selector) &&
        //        (obj as Selector).TagName==this.TagName &&
        //        (obj as Selector).Id == this.Id &&
        //        (obj as Selector).Classes.Equals(this.Classes)&&
        //         (obj as Selector).Parent.Equals(this.Parent)&&
        //          (obj as Selector).Child.Equals(this.Child);
        //}


    }
}
