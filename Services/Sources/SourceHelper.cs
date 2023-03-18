using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RudderstackForms.Common;
using RudderstackForms.Common.Exceptions;
using RudderstackForms.Models;
using RudderstackForms.Models.FormInputs;
using RudderstackForms.Services.FormTemplates;
using System.Text.Json;

namespace RudderstackForms.Services.Sources
{
    public class SourceHelper
    {
        private readonly FormTemplatesHelper _formTemplatesHelper;

        public SourceHelper(FormTemplatesService formTemplatesService)
        {
            _formTemplatesHelper = new FormTemplatesHelper(formTemplatesService);
        }

        public void ValidateCreateSourceRequest(Source newSource)
        {
            //Validate create source request
            //-> validate sourceType exists
            //->validate source data according to template for sourceType
            var formTemplate = _formTemplatesHelper.TryGetSourceTypeFromDbAsync(newSource.Type).Result;
            if (formTemplate == null)
            {
                throw new SourceTypeNotFoundException();
            }

            ValidateSourceUserData(newSource.UserData, formTemplate.Fields);
        }

        private void ValidateSourceUserData(Dictionary<string, string> userData, Dictionary<string, FormInput> fields)
        {
            ValidateUserDataContainsAllFormTemplateFields(userData, fields);

            ValidateUserDataWithField(userData, fields);
        }

        private void ValidateUserDataWithField(Dictionary<string, string> userData, Dictionary<string, FormInput> fields)
        {
            foreach (var field in fields)
            {
                if(field.Value.Type == InputType.CheckBox)
                {
                    ValidateFieldForCheckboxInput(userData[field.Key]);
                }
                else if(field.Value.Type == InputType.Radio)
                { 
                    var radioTypeField = (FormRadioInput)field.Value;
                    ValidateFieldForRadioInput(userData[field.Key], radioTypeField.Options);
                }
            }
        }

        private void ValidateFieldForCheckboxInput(string userDataInput)
        {
            if(!Constants.BooleanValueString.Any(x => x == userDataInput))
            {
                throw new InvalidSourceCheckboxInputDataException();
            }
        }

        private void ValidateFieldForRadioInput(string userDataInput, List<FormRadioInputOption> options)
        {
            if(!options.Any(x => x.Value == userDataInput))
            {
                throw new InvalidSourceRadioInputDataException();
            }
        }

        private void ValidateUserDataContainsAllFormTemplateFields(Dictionary<string, string> userData, Dictionary<string, FormInput> fields)
        {
            foreach (var field in fields)
            {
                if (!userData.ContainsKey(field.Key))
                {
                    throw new SourceUserDataMissingRequiredFormFields();
                }

                //TODO: Validate string vs bool input
            }
        }
    }
}
