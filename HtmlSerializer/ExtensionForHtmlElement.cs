using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HtmlSerializer
{
    public static class ExtensionForHtmlElement
    {
        public static HashSet<HtmlElement> GetElementsBySelector(this HtmlElement element, Selector selector)
        {
            HashSet<HtmlElement> elements = new HashSet<HtmlElement>();
            RecursiveFuncForGetElementsBySelector(element, selector, elements);
            return elements;

        }
        public static HashSet<HtmlElement> RecursiveFuncForGetElementsBySelector(HtmlElement htmlElement, Selector selector, HashSet<HtmlElement> htmlElements)
        {
            if (selector == null)
            {
                return htmlElements;
            }
            if (selector.TagName != null && selector.Id != null && selector.Classes.Count() != 0)
            {
                if (selector.TagName == htmlElement.Name && selector.Id == htmlElement.ID && selector.Classes.Equals(htmlElement.Classes))
                {
                    htmlElements.Add(htmlElement);
                }
                foreach (HtmlElement child in htmlElement.Children)
                {
                    RecursiveFuncForGetElementsBySelector(child, selector, htmlElements);
                }
            }
            else if (selector.TagName != null && selector.Id != null)
            {
                if (selector.TagName == htmlElement.Name && selector.Id == htmlElement.ID)
                {
                    htmlElements.Add(htmlElement);
                    foreach (HtmlElement child in htmlElement.Children)
                    {
                        RecursiveFuncForGetElementsBySelector(child, selector.Child, htmlElements);
                    }
                }
            }
            else if (selector.Id != null && selector.Classes.Count() != 0)
            {
                if (selector.Classes.Equals(htmlElement.Classes) && selector.Id == htmlElement.ID)
                {
                    htmlElements.Add(htmlElement);
                    foreach (HtmlElement child in htmlElement.Children)
                    {
                        RecursiveFuncForGetElementsBySelector(child, selector.Child, htmlElements);
                    }
                }
            }
            else if (selector.TagName != null && selector.Classes.Count() != 0)
            {
                if (selector.Classes.Equals(htmlElement.Classes) && selector.TagName == htmlElement.Name)
                {
                    htmlElements.Add(htmlElement);
                    foreach (HtmlElement child in htmlElement.Children)
                    {
                        RecursiveFuncForGetElementsBySelector(child, selector.Child, htmlElements);
                    }
                }
            }
            else if (selector.TagName == htmlElement.Name || (selector.Id != null && selector.Id == htmlElement.ID) || selector.Classes.Equals(htmlElement.Classes))
            {
                htmlElements.Add(htmlElement);
                foreach (HtmlElement child in htmlElement.Children)
                {
                    RecursiveFuncForGetElementsBySelector(child, selector.Child, htmlElements);
                }
            }
            foreach (HtmlElement child in htmlElement.Children)
            {
                 RecursiveFuncForGetElementsBySelector(child, selector, htmlElements);
            }
            return htmlElements;

        }
    }
}
