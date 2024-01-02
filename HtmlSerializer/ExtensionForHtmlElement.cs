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
        public static IEnumerable<HtmlElement> GetElementsBySelector(this HtmlElement element, Selector selector)
        {
            IEnumerable<HtmlElement> elements = null;
            RecursiveFuncForGetElementsBySelector(element, selector, elements);
            return elements;

        }
        public static void RecursiveFuncForGetElementsBySelector(HtmlElement htmlElement, Selector selector, IEnumerable<HtmlElement> htmlElements)
        {
            if (selector == null)
                htmlElements.ToList().Add(htmlElement);

            //if()

            //if (selector.TagName != null)
            //{
            //    if (selector.TagName == htmlElement.Name)
            //    {
            //        if (selector.Id != null)
            //        {
            //            if (selector.Id == htmlElement.ID)
            //            {

            //            }
            //        }
            //    }
            //    foreach (var child in htmlElement.Children)
            //    {
            //        RecursiveFuncForGetElementsBySelector(child, selector, htmlElements);
            //    }
            //}
            //if (selector.TagName != null && selector.Id != null && selector.Classes != null)
            //{
            //    if ()
            //}

        }
    }
}
