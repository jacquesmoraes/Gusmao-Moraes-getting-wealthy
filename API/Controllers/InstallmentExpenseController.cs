using Applications.Dtos.InstallmentDtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class InstallmentExpenseController (
        IInstallmentExpenseService installmentExpenseService,
        IMapper mapper ) : ControllerBase
    {
        private readonly IInstallmentExpenseService _installmentExpenseService = installmentExpenseService;
        private readonly IMapper _mapper = mapper;

        // POST api/<InstallmentExpenseController>
        [HttpPost]
        public async Task<IActionResult> Create ( CreateInstallmentDto dto )
        {
            try
            {
                var installment = _mapper.Map<InstallmentExpense> ( dto );
                installment.Expense = _mapper.Map<Expense> ( dto );

                var created = await _installmentExpenseService.CreateInstallmentExpense ( installment );
                var response = _mapper.Map<InstallmentExpenseResponseDto> ( created );
                return CreatedAtAction ( nameof ( GetById ), new { id = response.InstallmentExpenseId }, response );
            }
            catch ( ArgumentException ex )
            {
                return BadRequest ( ex.Message );
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ( )
        {
            var list = await _installmentExpenseService.GetAllInstallmentExpenses ( );
            var response = _mapper.Map<IEnumerable<InstallmentExpenseResponseDto>> ( list );
            return Ok ( response );
        }

        [HttpGet ( "{id:long}" )]
        public async Task<IActionResult> GetById ( long id )
        {
            var installment = await _installmentExpenseService.GetInstallmentExpenseById ( id );
            if ( installment == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<InstallmentExpenseResponseDto> ( installment ) );
        }

        [HttpPut ( "{id:long}" )]
        public async Task<IActionResult> Update ( long id, UpdateInstallmentDto dto )
        {
            try
            {
                var installment = _mapper.Map<InstallmentExpense> ( dto );
                installment.InstallmentExpenseId = id;
                installment.Expense = _mapper.Map<Expense> ( dto );

                var updated = await _installmentExpenseService.UpdateInstallmentExpense ( installment );
                return Ok ( _mapper.Map<InstallmentExpenseResponseDto> ( updated ) );
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

        [HttpDelete ( "{id:long}" )]
        public async Task<IActionResult> Delete ( long id )
        {
            var removed = await _installmentExpenseService.DeleteInstallmentExpense ( id );
            if ( removed == null )
            {
                return NotFound ( );
            }

            return Ok ( _mapper.Map<InstallmentExpenseResponseDto> ( removed ) );
        }
    }
}
