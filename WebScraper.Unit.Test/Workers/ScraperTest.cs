using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebScraper.Workers;
using WebScraper.Data;
using WebScraper.Builders;
using System.Text.RegularExpressions;

namespace WebScraper.Test.Unit
{
    [TestClass]
    public class ScraperTest
    {
        private readonly Scraper _scraper = new Scraper();
       
        [TestMethod]
        public void FindCollectionWithNoParts()
        {
            // Test whether scraper find link & description
            var content = "Some dummy data <a href=\"http://domain.com\" data-id=\"someId\" class=\"result-title hdrlnk\">some text</a> more dummy data";

            ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder()
                .WithData(content)
                .WithRegex(@"<a href=\""(.*?)\"" data-id=\""(.*?)\"" class=\""result-title hdrlnk\"">(.*?)</a>")
                .WithRegexOption(RegexOptions.ExplicitCapture)
                .Build();

            var foundElements = _scraper.Scrape(scrapeCriteria);

            Assert.IsTrue(foundElements.Count == 1);
            Assert.IsTrue(foundElements[0] == "<a href=\"http://domain.com\" data-id=\"someId\" class=\"result-title hdrlnk\">some text</a>");
        }

        [TestMethod]
        public void FindCollectionWithTwoParts()
        {
            // Test whether scraper find link & description
            var content = "Some dummy data <a href=\"http://domain.com\" data-id=\"someId\" class=\"result-title hdrlnk\">some text</a> more dummy data";

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

            var foundElements = _scraper.Scrape(scrapeCriteria);

            Assert.IsTrue(foundElements.Count == 2);
            Assert.IsTrue(foundElements[0] == "some text"); // because this is the description
            Assert.IsTrue(foundElements[1] == "http://domain.com");
        }
    }
}