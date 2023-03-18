using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace RudderstackForms.Models.FormInputs
{
    public class FormInputGeneric
    {
        /// <summary>
        /// Id fo object
        /// </summary>
        /// TODO: see if want to update type to match sample config - create enum for inputType and map to InputType
        [Required]
        public InputType Type { get; set; }

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public string Label { get; set; } = null!;

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public bool Required { get; set; }

        /// <summary>
        /// TextInput RegexErrorMessage
        /// </summary>
        public string? RegexErrorMessage { get; set; }

        /// <summary>
        /// TextInput PlaceHolder
        /// </summary>
        public string? Placeholder { get; set; }

        /// <summary>
        /// TextInput Regex
        /// </summary>
        public string? Regex { get; set; }

        /// <summary>
        /// RadioInput Options
        /// </summary>
        public List<FormRadioInputOption>? Options { get; set; }

        /// <summary>
        /// CheckboxInput Selection: true/false
        /// </summary>
        public bool Selection { get; set; }
    }
}
