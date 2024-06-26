using System.Collections.Generic;

namespace WebView.Style
{
    public static class PlaceHolderStyle
    {
        public static IList<Rule> GetPlaceholderStyle(string selectorName ,IRuleProperties properties)
        {
            List<Rule>? placeholderRules = new();

            placeholderRules.Add(new Rule()
            {
                Selector = new CssStringSelector() { SelectorName = $"{selectorName}::placeholder" },
                Properties = properties
            });

            placeholderRules.Add(new Rule()
            {
                Selector = new CssStringSelector() { SelectorName = $"{selectorName}:-ms-input-placeholder" },
                Properties = properties
            });

            placeholderRules.Add(new Rule()
            {
                Selector = new CssStringSelector() { SelectorName = $"{selectorName}::-ms-input-placeholder" },
                Properties = properties
            });

            return placeholderRules;
        }
    }
}
