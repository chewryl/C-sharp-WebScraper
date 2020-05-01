using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebScraper.Data;

namespace WebScraper.Builders
{
    public class ScrapeCriteriaBuilder
    {
        // fields
        private string _data;
        private string _regex;
        private RegexOptions _regexOption;
        private List<ScrapeCriteriaPart> _parts;

        public ScrapeCriteriaBuilder()
        {
            SetDefaults(); 
        }

        // constructor that sets defaults
        private void SetDefaults()
        {
            _data = string.Empty;
            _regex = string.Empty;
            _regexOption = RegexOptions.None;
            _parts = new List<ScrapeCriteriaPart>();
        }

        // methods that can set values for the fields
        public ScrapeCriteriaBuilder WithData(string Data)
        {
            _data = Data;
            return this;
        }

        public ScrapeCriteriaBuilder WithRegex(string regex)
        {
            _regex = regex;
            return this;
        }

        public ScrapeCriteriaBuilder WithRegexOption(RegexOptions regexOption)
        {
            _regexOption = regexOption;
            return this;
        }

        public ScrapeCriteriaBuilder WithPart(ScrapeCriteriaPart scrapeCriteriaPart)
        {
            _parts.Add(scrapeCriteriaPart); // adding to the list - so we can specify how many html elements we want to look into 
            return this;
        }

        public ScrapeCriteria Build()
        {
            ScrapeCriteria scrapeCriteria = new ScrapeCriteria();
            scrapeCriteria.Data = _data;
            scrapeCriteria.Regex = _regex;
            scrapeCriteria.RegExOption = _regexOption;
            scrapeCriteria.Parts = _parts;
            return scrapeCriteria;
        }
    }
}
