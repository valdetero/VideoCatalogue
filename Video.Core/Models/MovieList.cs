using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Video.Core.Models
{
    public class MovieList
    {
        [JsonProperty(PropertyName="page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "results")]
        public IEnumerable<Movie> Movies { get; set; }
    }
}
