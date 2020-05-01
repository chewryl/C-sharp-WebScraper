using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebScraper.Data
{
    public class ScrapeCriteria
    {
        public ScrapeCriteria()
        {
            Parts = new List<ScrapeCriteriaPart>(); // list of parts - will look further into a html element if we want to look for more stuff if there are any parts specified
        }

        public string Data { get; set; }
        public string Regex { get; set; }
        public RegexOptions RegExOption { get; set; }
        public List<ScrapeCriteriaPart> Parts { get; set; }
    }
}
