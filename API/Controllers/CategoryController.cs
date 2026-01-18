using Applications.Dtos.CategoryDtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController ( ICategoryService categoryService, IMapper mapper ) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IMapper _mapper = mapper;


        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Create ( CreateCategoryDto dto )
        {
            
            var mapped = _mapper.Map<Category> ( dto );

            var created = await _categoryService.CreateCategory ( mapped );
            return CreatedAtAction ( nameof ( Create ), new { id = created.CategoryId }, _mapper.Map<CategoryResponseDto>(created ));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories ( )
        {
            var list = await _categoryService.GetAllCategories ( );
            var response = _mapper.Map<IEnumerable<CategoryResponseDto>> ( list );
            return Ok ( response );
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById ( long id )
        {
            var category = await _categoryService.GetCategoryById ( id );
            if ( category == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<CategoryResponseDto> ( category ) );
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update ( long id, UpdateCategoryDto dto )
        {
            var mapped = _mapper.Map<Category> ( dto );
            var updated = await _categoryService.UpdateCategory ( id, mapped );
            if ( updated == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<CategoryResponseDto> ( updated ) );
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete ( long id )
        {
            var removed = await _categoryService.DeleteCategory ( id );
            if ( removed == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<CategoryResponseDto> ( removed ) );
        }

    }
}
