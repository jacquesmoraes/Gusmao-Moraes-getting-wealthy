using Applications.Dtos.ExpenseDtos;
using Applications.Dtos.RecurringExpenseDto;
using AutoMapper;
using Core.Entities;

namespace Applications.Mapping
{
    public class RecurringExpenseMapping : Profile
    {
        public RecurringExpenseMapping ( )
        {
            CreateMap<CreateRecurringExpenseDto, RecurringExpense> ( )
                .ForMember ( dest => dest.NextDate, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.CreationDate, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.Expense, opt => opt.Ignore ( ) );

            CreateMap<UpdateRecurringExpenseDto, RecurringExpense> ( )
                .ForMember ( dest => dest.NextDate, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.CreationDate, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.Expense, opt => opt.Ignore ( ) );

            CreateMap<CreateRecurringExpenseDto, Expense> ( );
                

            CreateMap<UpdateRecurringExpenseDto, Expense> ( );
                

            CreateMap<RecurringExpense, RecurringExpenseResponseDto> ( )
                .ForMember ( dest => dest.ExpenseResponse, opt => opt.MapFrom ( src => src.Expense ) );
        }
    }
}
