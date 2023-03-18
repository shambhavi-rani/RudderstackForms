using Microsoft.AspNetCore.Mvc;
using RudderstackForms.Models;
using RudderstackForms.Services.FormTemplates;

namespace RudderstackForms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormTemplatesController: ControllerBase
    {
        private readonly FormTemplatesService _formTemplatesService;

        private readonly FormTemplatesHelper _formTemplatesHelper;

        public FormTemplatesController(FormTemplatesService formTemplatesService)
        {
            _formTemplatesService = formTemplatesService;
            _formTemplatesHelper = new FormTemplatesHelper(formTemplatesService);
        }

        [HttpGet]
        public async Task<List<FormTemplate>> GetAll()
        {
            return await _formTemplatesService.GetAsync();
        }

        [HttpGet]
        [Route("SourceTypes")]
        public async Task<List<string>> GetAllSourceTypes()
        {
            return await _formTemplatesService.GetSourceTypesAsync();
        }

        //TODO: add in readme that max sourceType length is 200
        [HttpGet("{sourceType}")]
        public async Task<ActionResult<FormTemplate>> GetBySourceType(string sourceType)
        {
            var formTemplate = await _formTemplatesService.GetAsync(sourceType);

            if (formTemplate is null)
            {
                return NotFound();
            }

            return formTemplate;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FormTemplateDTO formTemplateRequest)
        {
            _formTemplatesHelper.ValidateCreateFormTemplateRequest(formTemplateRequest);

            var newFormTemplate = FormTemplatesHelper.GetFormTemplateFromFormTemplateDTO(formTemplateRequest);

            await _formTemplatesService.CreateAsync(newFormTemplate);

            return CreatedAtAction(nameof(GetAll), new {type = newFormTemplate.Type}, newFormTemplate);
        }
    }
}
