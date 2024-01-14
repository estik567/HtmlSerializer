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
        public List<string> Attributes { get; set; }=new List<string>();
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
        public IEnumerable<HtmlElement> GetDescendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                HtmlElement currentElement = queue.Dequeue();
                yield return currentElement;
                foreach(HtmlElement child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        public IEnumerable<HtmlElement> GetAncestors()
        {
            HtmlElement currentHtmlElement = this.Parent;
            while(currentHtmlElement is not null)
            {
                yield return currentHtmlElement;
                currentHtmlElement = currentHtmlElement.Parent;
            }
        }

        public override string ToString()
        {
            string result = "id ";
            result+= this.ID ==null?"=null":this.ID.ToString();
            result += " name= ";
            result+= this.Name == null ? "null" : this.Name.ToString();
            return result;
        }
    }
}
