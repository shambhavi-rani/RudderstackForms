using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace RudderstackForms.Models.FormInputs
{
    public class FormTextInput : FormInput
    {
        public FormTextInput(string label, bool required,
            string regexErrorMessage, string placeholder, string regex)
            : base(InputType.Text, label, required)
        {
            RegexErrorMessage = regexErrorMessage;
            Placeholder = placeholder;
            Regex = regex;
        }

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public string RegexErrorMessage { get; set; }

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public string Placeholder { get; set; }

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public string Regex { get; set; }
    }
}
