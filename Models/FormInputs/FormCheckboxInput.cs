using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RudderstackForms.Models.FormInputs
{
    public class FormCheckboxInput : FormInput
    {
        public FormCheckboxInput(string label, bool required)
            : base(InputType.CheckBox, label, required)
        {
        }
    }
}
