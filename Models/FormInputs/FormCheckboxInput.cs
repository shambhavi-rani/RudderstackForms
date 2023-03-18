using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace RudderstackForms.Models.FormInputs
{
    public class FormCheckboxInput : FormInput
    {
        public FormCheckboxInput(string label, bool required, bool selection)
            : base(InputType.CheckBox, label, required)
        {
            Selection = selection;
        }

        [Required]
        public bool Selection { get; set; }
    }
}
