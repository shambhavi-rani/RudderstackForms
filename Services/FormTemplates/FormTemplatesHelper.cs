using AutoMapper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MongoDB.Bson;
using RudderstackForms.Models;
using RudderstackForms.Models.FormInputs;

namespace RudderstackForms.Services.FormTemplates
{
    public class FormTemplatesHelper
    {
        public static FormTemplate GetFormTemplateFromFormTemplateDTO(FormTemplateDTO formTemplateDTO)
        {
            var formTemplate = new FormTemplate();
            formTemplate.Type = formTemplateDTO.Type;
            formTemplate.Fields = GetFormTemplateFieldsFromDTO(formTemplateDTO);

            return formTemplate;
        }

        private static Dictionary<string, FormInput> GetFormTemplateFieldsFromDTO(FormTemplateDTO formTemplateDTO)
        {
            var fileds = new Dictionary<string, FormInput>();

            foreach (var dtoField in formTemplateDTO.Fields)
            {
                var field = GetFormInputFromGenericInput(dtoField.Value);

                fileds[dtoField.Key] = field;
            }

            return fileds;
        }

        public static FormInput GetFormInputFromGenericInput(FormInputGeneric genericInput)
        {
            var mapper = GetFormInputMapperConfiguration();

            switch (genericInput.Type)
            {
                case InputType.Text:
                    return mapper.Map<FormInputGeneric, FormTextInput>(genericInput);
                case InputType.Radio:
                    return mapper.Map<FormInputGeneric, FormRadioInput>(genericInput);
                case InputType.CheckBox:
                    return mapper.Map<FormInputGeneric, FormCheckboxInput>(genericInput);
                default:
                    //TODO: create custom exceptions for better exception handling
                    throw new ArgumentException();
            }
        }

        private static IMapper GetFormInputMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<FormInputGeneric, FormCheckboxInput>();
                cfg.CreateMap<FormInputGeneric, FormTextInput>();
                cfg.CreateMap<FormInputGeneric, FormRadioInput>();
            });

            return config.CreateMapper();
        }
    }
}
