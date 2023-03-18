﻿using Microsoft.AspNetCore.Mvc;
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

        //TODO: add in readme that max sourceType length is 200
        [HttpGet("{sourceType}")]
        public async Task<ActionResult<FormTemplate>> Get(string sourceType)
        {
            var formTemplate = await _formTemplatesService.GetAsync(sourceType);

            if (formTemplate is null)
            {
                return NotFound();
            }

            return formTemplate;
        }

        [HttpPost]
        public async Task<IActionResult> Post(FormTemplateDTO formTemplateRequest)
        {
            //TODO: validate request 
            //-> key does not exist in db currently
            //-> all fields have different keys
            //-> sourceType length max 200

            var newFormTemplate = FormTemplatesHelper.GetFormTemplateFromFormTemplateDTO(formTemplateRequest);

            await _formTemplatesService.CreateAsync(newFormTemplate);

            return CreatedAtAction(nameof(Get), new {type = newFormTemplate.Type}, newFormTemplate);
        }
    }
}
