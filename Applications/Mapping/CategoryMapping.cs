using Applications.Dtos.CategoryDtos;
using AutoMapper;
using Core.Entities;

namespace Applications.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping ( )
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
            
        }
    }
}
