using System.Reflection;

namespace Swagger.Bootstrap.Utilities
{
    public static class ResourceLoader
    {
        /// <summary>
        /// Loads the contents of an embedded resource file as a string using the specified relative path.
        /// </summary>
        /// <remarks>The method searches for a resource whose name ends with the provided relative path,
        /// with directory separators replaced by periods. Resource names are case-sensitive and must match the embedded
        /// resource's name as stored in the assembly.</remarks>
        /// <param name="relativePath">The relative path to the embedded resource file, using forward or backward slashes as separators. The path
        /// is matched against resource names in the executing assembly.</param>
        /// <returns>A string containing the full contents of the embedded resource file.</returns>
        /// <exception cref="FileNotFoundException">Thrown if a resource matching the specified relative path cannot be found in the executing assembly.</exception>
        public static string Load(string relativePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly
                .GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(relativePath.Replace("/", ".").Replace("\\", ".")));

            if (resourceName == null)
                throw new FileNotFoundException($"Resource '{relativePath}' not found.");

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream!);
            return reader.ReadToEnd();
        }
    }

}
