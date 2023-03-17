using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace RudderstackForms.Models.FormInputs
{
    public abstract class FormInput
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
        public string Label { get; set; }

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public bool Required { get; set; }

        protected FormInput(InputType type, string label, bool required)
        {
            Type = type;
            Label = label;
            Required = required;
        }
    }
}
