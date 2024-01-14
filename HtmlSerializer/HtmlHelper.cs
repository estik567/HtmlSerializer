using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;

        public List<string> HtmlAllTags { get; set; }
        public List<string> HtmlTagsWithoutClose { get; set; }

        private HtmlHelper()
        {
            var contentAllTags = File.ReadAllText("Tags/HtmlTags.txt");
            HtmlAllTags = JsonSerializer.Deserialize<List<string>>(contentAllTags);

            var contentTagsWithoutClose = File.ReadAllText("Tags/HtmlVoidTags.txt");
            HtmlTagsWithoutClose = JsonSerializer.Deserialize<List<string>>(contentTagsWithoutClose);

        }

    }
}
