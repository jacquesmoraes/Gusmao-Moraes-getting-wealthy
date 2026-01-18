using Applications.Dtos.ExpenseDtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class ExpenseController ( IExpenseService expenseService, IMapper mapper ) : ControllerBase
    {
        private readonly IExpenseService _expenseService = expenseService;
        private readonly IMapper _mapper = mapper;

        // POST api/<ExpenseController>
        [HttpPost]
        public async Task<IActionResult> Create ( CreateExpenseDto dto )
        {
            try
            {
                var mapped = _mapper.Map<Expense> ( dto );
                var created = await _expenseService.CreateExpense ( mapped );
                return CreatedAtAction ( nameof ( Create ), new { id = created.ExpenseId }, _mapper.Map<ExpenseResponseDto> ( created ) );
            }
            catch ( ArgumentException ex )
            {
                return BadRequest ( ex.Message );
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses ( [FromQuery] long? categoryId = null, [FromQuery] DateOnly? startDate = null, [FromQuery] DateOnly? endDate = null )
        {
            var list = await _expenseService.GetAllExpenses(categoryId, startDate, endDate);
            var response = _mapper.Map<IEnumerable<ExpenseResponseDto>>(list);
            return Ok ( response );
        }

        [HttpGet ( "{id:long}" )]
        public async Task<IActionResult> GetById ( long id )
        {
            var expense = await _expenseService.GetExpenseById ( id );
            if ( expense == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<ExpenseResponseDto> ( expense ) );
        }

        [HttpPut ( "{id:long}" )]
        public async Task<IActionResult> Update ( long id, UpdateExpenseDto dto )
        {
            try
            {
                var mapped = _mapper.Map<Expense> ( dto );
                var updated = await _expenseService.UpdateExpense ( id, mapped );
                if ( updated == null )
                {
                    return NotFound ( );
                }

                return Ok ( _mapper.Map<ExpenseResponseDto> ( updated ) );
            }
            catch ( ArgumentException ex )
            {
                return BadRequest ( ex.Message );
            }
        }

        [HttpDelete ( "{id:long}" )]
        public async Task<IActionResult> Delete ( long id )
        {
            var removed = await _expenseService.DeleteExpense ( id );
            if ( removed == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<ExpenseResponseDto> ( removed ) );
        }


    }
}
