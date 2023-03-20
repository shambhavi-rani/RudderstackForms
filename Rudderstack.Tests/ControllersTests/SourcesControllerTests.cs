using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Moq;
using RudderstackForms.Common.Exceptions;
using RudderstackForms.Controllers;
using RudderstackForms.Models;
using RudderstackForms.Models.FormInputs;
using RudderstackForms.Services.FormTemplates;
using RudderstackForms.Services.Sources;

namespace Rudderstack.Tests.ControllersTests
{
    [TestClass]
    public class SourcesControllerTests
    {
        private SourcesController sourcesController;
        private Mock<ISourcesService> sourcesService;
        private Mock<IFormTemplatesService> formTemplatesService;

        [TestMethod]
        public async Task GetAll_SuccessAsync()
        {
            Setup();

            var result = await sourcesController.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("TestSource", result[0].Type);
            Assert.AreEqual(3, result[0].UserData.Count);
        }

        [TestMethod]
        public async Task Create_SuccessAsync()
        {
            Setup();
            this.formTemplatesService.Setup(x => x.GetAsync("TestSource")).Returns(Task.FromResult(GetFormTemplate()));

            var result = await sourcesController.Create(GetSource());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.IsNotNull(createdAtActionResult.Value);
            Assert.IsInstanceOfType(createdAtActionResult.Value, typeof(Source));
            var sourceInResponse = (Source)createdAtActionResult.Value;
            Assert.AreEqual("GetAll", createdAtActionResult.ActionName);
            Assert.AreEqual("TestSource", sourceInResponse.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(SourceTypeNotFoundException))]
        public async Task Create_CreateWhenSourceTypeDoesNotExist_FailureAsync()
        {
            Setup();

            await sourcesController.Create(GetSource());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSourceCheckboxInputDataException))]
        public async Task Create_CreateWithInvalidCheckboxUserData_FailureAsync()
        {
            Setup();
            this.formTemplatesService.Setup(x => x.GetAsync("TestSource")).Returns(Task.FromResult(GetFormTemplate()));

            var source = GetSource();
            //checkboxInput is checkbox type input so expects boolean value
            source.UserData["checkboxInput"] = "value1";
            await sourcesController.Create(source);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSourceRadioInputDataException))]
        public async Task Create_CreateWithInvalidRadioUserData_FailureAsync()
        {
            Setup();
            this.formTemplatesService.Setup(x => x.GetAsync("TestSource")).Returns(Task.FromResult(GetFormTemplate()));

            var source = GetSource();
            //radioInput is radio type input and expects value to be one out of its options
            source.UserData["radioInput"] = "randomOptionValue";
            await sourcesController.Create(source);
        }

        [TestMethod]
        [ExpectedException(typeof(SourceUserDataMissingRequiredFormFields))]
        public async Task Create_CreateWithRequiredUserDataMissing_FailureAsync()
        {
            Setup();
            this.formTemplatesService.Setup(x => x.GetAsync("TestSource")).Returns(Task.FromResult(GetFormTemplate()));

            var source = GetSource();
            //radioInput is radio type input and expects value to be one out of its options
            source.UserData.Remove("checkboxInput");
            await sourcesController.Create(source);
        }

        #region HelperMethods

        private void Setup()
        {
            this.sourcesService = new Mock<ISourcesService>();
            this.formTemplatesService = new Mock<IFormTemplatesService>();
            this.sourcesController = new SourcesController(sourcesService.Object, formTemplatesService.Object);

            this.sourcesService.Setup(x => x.GetAsync()).Returns(GetSources());
        }

        private Task<List<Source>> GetSources()
        {
            var sources = new List<Source>
            {
                GetSource()
            };
            return Task.FromResult(sources);
        }


        private Source GetSource()
        {
            return new Source("TestSource", GetSourceUserData());
        }

        private Dictionary<string, string> GetSourceUserData()
        {
            var userData = new Dictionary<string, string>();
            userData["checkboxInput"] = "true";
            userData["textInput"] = "textValue";
            userData["radioInput"] = "optionValue1";
            return userData;
        }

        private FormTemplate GetFormTemplate()
        {
            return new FormTemplate()
            {
                Type = "TestSource",
                Fields = GetFormTemplateFields()
            };
        }

        private Dictionary<string, FormInput> GetFormTemplateFields()
        {
            var fields = new Dictionary<string, FormInput>();
            fields["checkboxInput"] = new FormCheckboxInput("checkbox1", true, false);
            fields["textInput"] = new FormTextInput("text2", true, "Invalid text input", "Enter text", "[a-z0-9]");
            fields["radioInput"] = new FormRadioInput("radio3", true, new List<FormRadioInputOption>()
            {
                new FormRadioInputOption("option1", "optionValue1"),
                new FormRadioInputOption("option2", "optionValue2")
            });
            return fields;
        }


        #endregion HelperMethods


    }
}
