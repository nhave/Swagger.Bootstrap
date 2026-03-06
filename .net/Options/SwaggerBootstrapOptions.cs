namespace Swagger.Bootstrap.Options
{
    public class SwaggerBootstrapOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether experimental features are enabled.
        /// </summary>
        /// <remarks>Experimental features may be unstable or subject to change in future releases. Enable
        /// this option only if you are testing new functionality or are aware of the potential risks.</remarks>
        public bool UseExperimentalFeatures { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether authentication is enabled for the application.
        /// </summary>
        /// <remarks>When set to <see langword="true"/>, a login page is made available at the configured
        /// authentication endpoint. Set this property to <see langword="false"/> to disable authentication and allow
        /// unrestricted access.</remarks>
        public bool UseAuthentication { get; set; } = false;

        /// <summary>
        /// Gets the options used to configure the login page.
        /// </summary>
        public LoginPageOptions loginOptions { get; private set; } = new LoginPageOptions();
    }

    public class LoginPageOptions
    {
        /// <summary>
        /// Gets or sets the relative endpoint path used for user login requests.
        /// </summary>
        public string LoginEndpoint { get; set; } = "/auth/login";

        /// <summary>
        /// Gets or sets the name of the JavaScript variable that will hold the JWT token in the JavaScript context.
        /// </summary>
        public string VariableName { get; set; } = "jwtToken";

        /// <summary>
        /// Gets or sets the expected format for the user name input.
        /// </summary>
        /// <remarks>Set this property to specify whether the user name should be treated as plain text or
        /// as an email address. This affects how user input is validated and displayed.</remarks>
        public UserNameType UserNameType { get; set; } = UserNameType.Text;
    }

    public enum UserNameType
    {
        Text,
        Email
    }
}
