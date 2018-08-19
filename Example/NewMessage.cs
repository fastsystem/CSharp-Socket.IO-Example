using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class NewMessage
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
