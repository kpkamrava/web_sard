namespace web_sard.Models.tbls.user
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="login" />.
    /// </summary>
    public class login
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether remember.
        /// </summary>
        public bool remember { get; set; }
    }
}
