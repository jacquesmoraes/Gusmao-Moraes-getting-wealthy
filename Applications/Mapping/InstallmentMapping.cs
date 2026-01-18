using Applications.Dtos.InstallmentDtos;
using AutoMapper;
using Core.Entities;

namespace Applications.Mapping
{
    public class InstallmentMapping : Profile
    {
        public InstallmentMapping ( )
        {
            CreateMap<CreateInstallmentDto, InstallmentExpense> ( )
                .ForMember ( dest => dest.FirstInstallmentDate, opt => opt.MapFrom ( src => src.FirstInstallmentDate ) )
                .ForMember ( dest => dest.InstallmentAmount, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.Items, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.Expense, opt => opt.Ignore ( ) );

            CreateMap<UpdateInstallmentDto, InstallmentExpense> ( )
                .ForMember ( dest => dest.FirstInstallmentDate, opt => opt.MapFrom ( src => src.FisrtInstallmentDate ) )
                .ForMember ( dest => dest.InstallmentAmount, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.Items, opt => opt.Ignore ( ) )
                .ForMember ( dest => dest.Expense, opt => opt.Ignore ( ) );

            CreateMap<CreateInstallmentDto, Expense> ( )
                .ForMember ( dest => dest.Date, opt => opt.MapFrom ( src => src.FirstInstallmentDate ) )
                .ForMember ( dest => dest.CategoryId, opt => opt.MapFrom ( src => ( long ) src.CategoryId ) );

            CreateMap<UpdateInstallmentDto, Expense> ( )
                .ForMember ( dest => dest.Date, opt => opt.MapFrom ( src => src.FisrtInstallmentDate ) )
                .ForMember ( dest => dest.CategoryId, opt => opt.MapFrom ( src => ( long ) src.CategoryId ) );

            CreateMap<InstallmentExpenseItem, InstallmentExpenseItemDto> ( )
                .ForMember ( dest => dest.Amount, opt => opt.MapFrom ( src => src.Value ) );

            CreateMap<InstallmentExpense, InstallmentExpenseResponseDto> ( )
                .ForMember (
                    dest => dest.ExpenseId,
                    opt => opt.MapFrom ( src => src.Expense == null ? null : ( long? ) src.Expense.ExpenseId ) )
                .ForMember ( dest => dest.Items, opt => opt.MapFrom ( src => src.Items ) );
        }
    }
}
