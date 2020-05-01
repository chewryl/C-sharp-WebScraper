using System;
using System.Text.RegularExpressions;
using WebScraper.Data;

namespace WebScraper.Builders
{
    public class ScrapeCriteriaPartBuilder
    {
        // fields
        private string _regex;
        private RegexOptions _regexOption;

        // constructor
        public ScrapeCriteriaPartBuilder()
        {
            SetDefaults();
        }

        // set defaults
        private void SetDefaults()
        {
            _regex = string.Empty;
            _regexOption = RegexOptions.None;
        }

        // methods to set field values
        public ScrapeCriteriaPartBuilder WithRegex(string regex)
        {
            _regex = regex;
            return this;
        }

        public ScrapeCriteriaPartBuilder WithRegexOption(RegexOptions regexOption)
        {
            _regexOption = regexOption;
            return this;
        }

        public ScrapeCriteriaPart Build()
        {
            ScrapeCriteriaPart scrapeCriteriaPart = new ScrapeCriteriaPart();
            scrapeCriteriaPart.Regex = _regex;
            scrapeCriteriaPart.RegexOption = _regexOption;
            return scrapeCriteriaPart;
        }
    }
}
