using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RudderstackForms.Models.FormInputs
{
    public class FormRadioInput : FormInput
    {
        public FormRadioInput(string label, bool required, List<FormRadioInputOption> options)
            : base(InputType.Radio, label, required)
        {
            Options = options;
        }

        List<FormRadioInputOption> Options { get; set; }
    }

    public class FormRadioInputOption
    {
        public string Label { get; set; }
        public string Value { get; set; }

        public FormRadioInputOption(string lable, string value)
        {
            Label = lable;
            Value = value;
        }
    }
}
