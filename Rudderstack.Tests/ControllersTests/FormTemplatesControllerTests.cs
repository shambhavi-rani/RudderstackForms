using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Moq;
using RudderstackForms.Common.Exceptions;
using RudderstackForms.Controllers;
using RudderstackForms.Models;
using RudderstackForms.Models.FormInputs;
using RudderstackForms.Services;
using RudderstackForms.Services.FormTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Rudderstack.Tests.ControllersTests
{
    [TestClass]
    public class FormTemplatesControllerTests
    {
        private FormTemplatesController formTemplatesController;
        private Mock<IFormTemplatesService> formTemplatesService;

        [TestMethod]
        public async Task GetAll_SuccessAsync()
        {
            Setup();

            var result = await formTemplatesController.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("TestSource", result[0].Type);
            Assert.AreEqual(2, result[0].Fields.Count);
        }

        [TestMethod]
        public async Task Create_SuccessAsync()
        {
            Setup();

            var formTemplateDTO = GetFormTemplateDTO();
            var result = await formTemplatesController.Create(formTemplateDTO);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.IsNotNull(createdAtActionResult.Value);
            Assert.IsInstanceOfType(createdAtActionResult.Value, typeof(FormTemplate));
            var formTemplateInResponse = (FormTemplate)createdAtActionResult.Value;
            Assert.AreEqual("GetAll", createdAtActionResult.ActionName);
            Assert.AreEqual("TestSource", formTemplateInResponse.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRegexInCreateSourceException))]
        public async Task Create_InvalidRegexInCreateFormTemplate_FailureAsync()
        {
            Setup();

            var formTemplateDTO = GetFormTemplateDTO();
            formTemplateDTO.Fields["field1"] = GetGenericTextFieldWithInvalidRegex();
            await formTemplatesController.Create(formTemplateDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormInputException))]
        public async Task Create_InvalidInputTypeFieldInCreateFormTemplate_FailureAsync()
        {
            Setup();

            var formTemplateDTO = GetFormTemplateDTO();
            formTemplateDTO.Fields["field1"] = GetGenericFieldWithInvalidInputType();
            await formTemplatesController.Create(formTemplateDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(MaxFormTemplateFieldsCountExceededException))]
        public async Task Create_CreateFormTemplateWithMoreThanMaxFieldCount_FailureAsync()
        {
            Setup();

            var formTemplateDTO = GetFormTemplateDTO(1001);
            await formTemplatesController.Create(formTemplateDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(SourceTypeAlreadyExistsException))]
        public async Task Create_CreateFormTemplateWithExistingSourceType_FailureAsync()
        {
            Setup();
            this.formTemplatesService.Setup(x => x.GetAsync("TestSource")).Returns(Task.FromResult(GetFormTemplate()));

            var formTemplateDTO = GetFormTemplateDTO(1001);
            await formTemplatesController.Create(formTemplateDTO);
        }

        #region HelperMethods

        private FormTemplateDTO GetFormTemplateDTO(int fieldCount = 2)
        {
            var formTemplateDTO = new FormTemplateDTO();
            formTemplateDTO.Type = "TestSource";
            formTemplateDTO.Fields = GetFormTemplateDTOFields(fieldCount);
            return formTemplateDTO;
        }

        private FormInputGeneric GetGenericTextFieldWithInvalidRegex()
        {
            return new FormInputGeneric()
            {
                Type = InputType.Text,
                Label = "text3",
                Required = true,
                RegexErrorMessage = "Invalid text input",
                Placeholder = "Enter text",
                Regex = "["
            };
        }

        private FormInputGeneric GetGenericFieldWithInvalidInputType()
        {
            return new FormInputGeneric()
            {
                Type = InputType.Hidden,
                Label = "text3",
                Required = true
            };
        }

        private Dictionary<string, FormInputGeneric> GetFormTemplateDTOFields(int count = 2)
        {
            var fields = new Dictionary<string, FormInputGeneric>();
            for(int i = 0; i < count; ++i)
            {
                fields[$"field{i}"] = new FormInputGeneric()
                {
                    Type = InputType.CheckBox,
                    Label = $"checkbox{i}",
                    Required = true,
                    Selection = false
                };
            }
            return fields;
        }

        private void Setup()
        {
            this.formTemplatesService = new Mock<IFormTemplatesService>();
            this.formTemplatesController = new FormTemplatesController(formTemplatesService.Object);

            this.formTemplatesService.Setup(x => x.GetAsync()).Returns(GetFormTemplates());
        }

        private FormTemplate GetFormTemplate()
        {
            return new FormTemplate()
            {
                Type = "TestSource",
                Fields = GetFormTemplateFields()
            };
        }

        private Task<List<FormTemplate>> GetFormTemplates()
        {
            var formTemplatesList = new List<FormTemplate>
            {
                GetFormTemplate()
            };
            return Task.FromResult(formTemplatesList);
        }

        private Dictionary<string, FormInput> GetFormTemplateFields()
        {
            var fields = new Dictionary<string, FormInput>();
            fields["field1"] = new FormCheckboxInput("checkbox1", true, false);
            fields["field2"] = new FormTextInput("text2", true, "Invalid text input", "Enter text", "[a-z0-9]");
            return fields;
        }

        #endregion HelperMethods


    }
}
