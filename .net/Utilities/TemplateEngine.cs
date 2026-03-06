namespace Swagger.Bootstrap.Utilities
{
    public static class TemplateEngine
    {
        /// <summary>
        /// Replaces placeholders in the specified template string with corresponding values from the provided
        /// dictionary.
        /// </summary>
        /// <remarks>Placeholders in the template must be in the format {{key}}, where 'key' matches a key
        /// in the values dictionary. The method performs a simple string replacement and does not support nested or
        /// recursive placeholders.</remarks>
        /// <param name="template">The template string containing placeholders in the format {{key}} to be replaced.</param>
        /// <param name="values">A dictionary containing key-value pairs where each key corresponds to a placeholder in the template, and
        /// each value is the replacement text.</param>
        /// <returns>A string with all placeholders in the template replaced by their corresponding values. If a placeholder does
        /// not have a matching key in the dictionary, it remains unchanged.</returns>
        public static string Apply(string template, Dictionary<string, string> values)
        {
            foreach (var kv in values)
            {
                template = template.Replace("{{" + kv.Key + "}}", kv.Value);
            }
            return template;
        }
    }
}
