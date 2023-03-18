using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace RudderstackForms.Models.FormInputs
{
    public class FormRadioInput : FormInput
    {
        public FormRadioInput(string label, bool required, 
            List<FormRadioInputOption> options)
            : base(InputType.Radio, label, required)
        {
            Options = options;
        }

        [Required]
        public List<FormRadioInputOption> Options { get; set; }
    }

    public class FormRadioInputOption
    {
        public string Label { get; set; }
        public string Value { get; set; }

        public FormRadioInputOption(string label, string value)
        {
            Label = label;
            Value = value;
        }
    }
}
