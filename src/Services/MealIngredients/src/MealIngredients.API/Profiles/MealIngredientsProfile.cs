using AutoMapper;
using BuildingBlocks.Commons.Models.EventModels;
using BuildingBlocks.Events;
using MealIngredients.API.Entities;

namespace MealIngredients.API.Profiles;

public sealed class MealIngredientsProfile : Profile
{
    public MealIngredientsProfile()
    {
        CreateMap<CreateMealEventDto, MealIngredient>()
            .ForMember(dest => dest.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, src => src.MapFrom(x => DateTime.UtcNow));

    }
}