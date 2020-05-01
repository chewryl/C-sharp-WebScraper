using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebScraper.Data;

namespace WebScraper.Workers
{
    public class Scraper
    {
        public List<string> Scrape(ScrapeCriteria ScrapeCriteria)   // method takes the ScrapeCriteria & returns scraped list
        {
            List<string> scrapedElements = new List<string>();

            // Perform scraping
            MatchCollection matches = Regex.Matches(ScrapeCriteria.Data, ScrapeCriteria.Regex, ScrapeCriteria.RegExOption); // input, regex, regexOption

            foreach (Match match in matches)
            {
                if (!ScrapeCriteria.Parts.Any()) // no parts specified in this element
                {
                    scrapedElements.Add(match.Groups[0].Value);  // adding value of matched element to list
                }
                else // parts specified for this element
                {
                    foreach (var part in ScrapeCriteria.Parts)
                    {
                        Match matchedPart = Regex.Match(match.Groups[0].Value, part.Regex, part.RegexOption);

                        if (matchedPart.Success) scrapedElements.Add(matchedPart.Groups[1].Value); 
                    }
                }
            }

            return scrapedElements; // as list of strings
        }
    }
}
