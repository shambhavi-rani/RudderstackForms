using Microsoft.AspNetCore.Mvc;
using RudderstackForms.Models;
using RudderstackForms.Services.Sources;

namespace RudderstackForms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SourcesController: ControllerBase
    {
        private readonly SourcesService _sourcesService;

        public SourcesController(SourcesService sourcesService)
        {
            _sourcesService = sourcesService;
        }

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
            //TODO: validate request
            //-> validate sourceType exists
            //->validate source data according to template for sourceType

            await _sourcesService.CreateAsync(newSource);

            return CreatedAtAction(nameof(GetAll), new { id = newSource.Id }, newSource);
        }
    }
}
