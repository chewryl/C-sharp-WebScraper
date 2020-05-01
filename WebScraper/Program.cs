using System;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using WebScraper.Builders;
using WebScraper.Data;
using WebScraper.Workers;

[assembly: InternalsVisibleTo("WebScraper.Unit.Test")]
namespace WebScraper
{
    class Program
    {
        private const string Method = "search";

        static void Main(string[] args)
        {
            // SCRAPING PROCESS
            // Download page content
            // Use regular expressions & builder pattern to create search criteria - search through elements we want in HTML
            // scrape

            try
            {
                Console.WriteLine("Please enter which city you would like to scrape information from:");
                var craigslistCity = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Please enter the CraigsList category that you would like to scrape:");
                var craigslistCategoryName = Console.ReadLine() ?? string.Empty;

                using (WebClient client = new WebClient())
                {
                    string content = client.DownloadString($"http://{craigslistCity.Replace(" ", string.Empty)}.craigslist.org/{Method}/sss?query={craigslistCategoryName}");
                    ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder()
                        .WithData(content)
                        .WithRegex(@"<a href=\""(.*?)\"" data-id=\""(.*?)\"" class=\""result-title hdrlnk\"">(.*?)</a>") // regex to pick out specific things from html element <a> 
                        .WithRegexOption(RegexOptions.ExplicitCapture)
                        .WithPart(new ScrapeCriteriaPartBuilder()
                            .WithRegex(@">(.*?)</a>") // specifying parts in the link - using builder within a builder
                            .WithRegexOption(RegexOptions.Singleline)
                            .Build())
                        .WithPart(new ScrapeCriteriaPartBuilder()
                            .WithRegex(@"href=\""(.*?)\""")
                            .WithRegexOption(RegexOptions.Singleline)
                            .Build())
                        .Build(); // Now building all of it
                 
                     
                    Scraper scraper = new Scraper();

                    var scrapedElements = scraper.Scrape(scrapeCriteria);

                    if (scrapedElements.Any())
                    {
                        foreach (var scrapedElement in scrapedElements) Console.WriteLine(scrapedElement);
                    }
                    else
                    {
                        Console.WriteLine("There were no matches for the specified scrape criteria");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
