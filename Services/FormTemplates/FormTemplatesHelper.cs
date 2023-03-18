using Amazon.Auth.AccessControlPolicy;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MongoDB.Bson;
using RudderstackForms.Models;
using RudderstackForms.Models.FormInputs;
using RudderstackForms.Services.Sources;

namespace RudderstackForms.Services.FormTemplates
{
    public class FormTemplatesHelper
    {
        private readonly FormTemplatesService _formTemplatesService;

        public FormTemplatesHelper(FormTemplatesService formTemplatesService)
        {
            _formTemplatesService = formTemplatesService;
        }

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

        public void ValidateCreateFormTemplateRequest(FormTemplateDTO formTemplateRequest)
        {
            //Validate create form template request 
            //-> key does not exist in db currently
            //-> sourceType length max 200
            //-> max number of feilds is 1000

            ValidateSourceType(formTemplateRequest.Type);

            ValidateFields(formTemplateRequest.Fields);
        }

        private void ValidateFields(Dictionary<string, FormInputGeneric> fields)
        {
            ValidateFieldsCount(fields.Count);
        }

        private void ValidateFieldsCount(int fieldsCount)
        {
            if(fieldsCount > Constants.FormTemplateFieldsCount)
            {
                //TODO: custom exception
                throw new InvalidDataException();
            }
        }

        private void ValidateSourceType(string sourceType)
        {
            ValidateSourceTypeLength(sourceType);

            ValidateSourceTypeDoesNotExistInDb(sourceType);
        }

        private void ValidateSourceTypeDoesNotExistInDb(string sourceType)
        {
            var formTemplate = TryGetSourceTypeFromDbAsync(sourceType).Result;
            if(formTemplate != null)
            {
                //TODO: custom exception - key already exists in db
                throw new InvalidDataException();
            }
        }

        private void ValidateSourceTypeLength(string sourceType)
        {
            if (sourceType.Length > Constants.SourceTypeMaxLength)
            {
                //TODO: custom exception
                throw new InvalidDataException();
            }
        }

        public async Task<FormTemplate?> TryGetSourceTypeFromDbAsync(string sourceType)
        {
            return await _formTemplatesService.GetAsync(sourceType);
        }
    }
}
