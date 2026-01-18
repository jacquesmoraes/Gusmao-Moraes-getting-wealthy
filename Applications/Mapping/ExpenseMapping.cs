using Applications.Dtos.ExpenseDtos;
using AutoMapper;
using Core.Entities;

namespace Applications.Mapping
{
     public class ExpenseMapping : Profile
    {
        public ExpenseMapping ( )
        {
            CreateMap<CreateExpenseDto, Expense> ( );
            CreateMap<UpdateExpenseDto, Expense> ( );
            CreateMap<Expense, ExpenseResponseDto>()
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
    .ForMember(dest => dest.CategoryColor, opt => opt.MapFrom(src => src.Category != null ? src.Category.Color : string.Empty))
    .ForMember(dest => dest.CategoryIcon, opt => opt.MapFrom(src => src.Category != null ? src.Category.Icon : string.Empty));
        }

    }
}
