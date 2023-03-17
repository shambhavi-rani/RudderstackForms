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

        public FormTemplatesController(FormTemplatesService formTemplatesService)
        {
            _formTemplatesService = formTemplatesService;
        }

        [HttpGet]
        public async Task<List<FormTemplate>> Get()
        {
            return await _formTemplatesService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(FormTemplateDTO formTemplateRequest)
        {
            //TODO: validate request 
            //-> key does not exist in db currently
            //-> all fields have different keys

            var newFormTemplate = FormTemplatesHelper.GetFormTemplateFromFormTemplateDTO(formTemplateRequest);

            await _formTemplatesService.CreateAsync(newFormTemplate);

            return CreatedAtAction(nameof(Get), new {type = newFormTemplate.Type}, newFormTemplate);
        }
    }
}
