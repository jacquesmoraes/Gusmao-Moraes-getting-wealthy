using Applications.Dtos.RecurringExpenseDto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class RecurringExpenseController (
        IRecurringExpenseService recurringExpenseService,
        IMapper mapper ) : ControllerBase
    {
        private readonly IRecurringExpenseService _recurringExpenseService = recurringExpenseService;
        private readonly IMapper _mapper = mapper;

        // POST api/<RecurringExpenseController>
        [HttpPost]
        public async Task<IActionResult> Create ( CreateRecurringExpenseDto dto )
        {
            try
            {
                var recurring = _mapper.Map<RecurringExpense> ( dto );
                recurring.Expense = _mapper.Map<Expense> ( dto );

                var created = await _recurringExpenseService.CreateRecurringExpense ( recurring );
                var response = _mapper.Map<RecurringExpenseResponseDto> ( created );
                return CreatedAtAction ( nameof ( GetById ), new { id = response.RecurringExpenseId }, response );
            }
            catch ( ArgumentException ex )
            {
                return BadRequest ( ex.Message );
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ( )
        {
            var list = await _recurringExpenseService.GetAllRecurringExpenses ( );
            var response = _mapper.Map<IEnumerable<RecurringExpenseResponseDto>> ( list );
            return Ok ( response );
        }

        [HttpGet ( "{id:long}" )]
        public async Task<IActionResult> GetById ( long id )
        {
            var recurring = await _recurringExpenseService.GetRecurringExpenseById ( id );
            if ( recurring == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<RecurringExpenseResponseDto> ( recurring ) );
        }

        [HttpPut ( "{id:long}" )]
        public async Task<IActionResult> Update ( long id, UpdateRecurringExpenseDto dto )
        {
            try
            {
                var recurring = _mapper.Map<RecurringExpense> ( dto );
                recurring.RecurringExpenseId = id;
                recurring.Expense = _mapper.Map<Expense> ( dto );

                var updated = await _recurringExpenseService.UpdateRecurringExpense ( recurring );
                return Ok ( _mapper.Map<RecurringExpenseResponseDto> ( updated ) );
            }
            catch ( KeyNotFoundException )
            {
                return NotFound ( );
            }
            catch ( ArgumentException ex )
            {
                return BadRequest ( ex.Message );
            }
        }

        [HttpPatch ( "{id:long}/toggle" )]
        public async Task<IActionResult> ToggleActive ( long id )
        {
            var toggled = await _recurringExpenseService.ToggleActive ( id );
            if ( toggled == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<RecurringExpenseResponseDto> ( toggled ) );
        }

        [HttpDelete ( "{id:long}" )]
        public async Task<IActionResult> Delete ( long id )
        {
            var removed = await _recurringExpenseService.DeleteRecurringExpense ( id );
            if ( removed == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<RecurringExpenseResponseDto> ( removed ) );
        }
    }
}
