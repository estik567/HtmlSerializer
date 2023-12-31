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
            //HtmlAllTags = JsonConverter.DeserializeObject<List<string>>(contentAllTags);
            //dynamic dy = JsonSerializer.Deserialize<dynamic>(contentAllTags);
            //HtmlAllTags = dy;
            HtmlAllTags = JsonSerializer.Deserialize<List<string>>(contentAllTags);

            var contentTagsWithoutClose = File.ReadAllText("Tags/HtmlVoidTags.txt");
            //dynamic dy = JsonSerializer.Deserialize<dynamic>(contentTagsWithoutClose);
            //HtmlTagsWithoutClose = (List<string>) dy;
            //HtmlTagsWithoutClose = (List<string>)JsonSerializer.Deserialize(contentTagsWithoutClose, HtmlTagsWithoutClose.GetType());
            HtmlTagsWithoutClose = JsonSerializer.Deserialize< List<string>>(contentTagsWithoutClose);

        }

    }
}
