using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlElement
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }= new List<HtmlElement>();

        public HtmlElement(string iD, string name, List<string> attributes, List<string> classes, string innerHtml, HtmlElement parent, List<HtmlElement> children)
        {
            ID = iD;
            Name = name;
            Attributes = attributes;
            Classes = classes;
            InnerHtml = innerHtml;
            Parent = parent;
            Children = children;
        }

        public HtmlElement(){ }
    }
}
