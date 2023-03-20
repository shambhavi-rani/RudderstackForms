using Microsoft.AspNetCore.Mvc;
using RudderstackForms.Models;
using RudderstackForms.Services.FormTemplates;
using RudderstackForms.Services.Sources;

namespace RudderstackForms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SourcesController: ControllerBase
    {
        private readonly ISourcesService _sourcesService;

        private readonly SourceHelper _sourceHelper;

        public SourcesController(ISourcesService sourcesService, IFormTemplatesService formTemplatesService)
        {
            _sourcesService = sourcesService;
            _sourceHelper = new SourceHelper(formTemplatesService);
        }

        //TODO: Add logging

        [HttpGet]
        public async Task<List<Source>> GetAll()
        {
            return await _sourcesService.GetAsync();
        }

        [HttpGet]
        [Route("SourceTypes/{sourceType}")]
        public async Task<List<Source>> GetBySourceType(string sourceType)
        {
            return await _sourcesService.GetByTypeAsync(sourceType);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Source>> GetById(string id)
        {
            var source = await _sourcesService.GetAsync(id);
            
            if(source is null)
            {
                return NotFound();
            }

            return source;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Source newSource)
        {
            _sourceHelper.ValidateCreateSourceRequest(newSource);

            await _sourcesService.CreateAsync(newSource);

            return CreatedAtAction(nameof(GetAll), new { id = newSource.Id }, newSource);
        }
    }
}
