using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Category.Features.Dtos;
using FluentValidation;
using MediatR;
using ValidationException = BuildingBlocks.Commons.Exceptions.ValidationException;

namespace Meals.Features.Category.Commands.UpdateCategory.v1;

sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<UpdateCategoryDto> _validator;
    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IValidator<UpdateCategoryDto> validator)
    {
        _categoryRepository = categoryRepository;
        _validator = validator;
    }
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetValue(x => x.Id.ToString() == request.CategoryId, AsNoTracking: false)
            ?? throw new NotFoundException($"Category with Id '{request.CategoryId}' was not found");

        UpdateCategoryDto categoryToUpdate = new(category.Name);

        request.UpdateCategory.ApplyTo(categoryToUpdate, (err) => {
            throw new ConflictException("Error in JsonPatchDocument " + err.ErrorMessage);
        });

        var exisitingCategory = await _categoryRepository.GetValue(
            x => x.Name.ToLower() == categoryToUpdate.Name,
            x => new {x.Id}
        );
        if(exisitingCategory is not null) 
            throw new ConflictException($"Ingredient with Name '{categoryToUpdate.Name}' already exists.");

        var validationResults = await _validator.ValidateAsync(categoryToUpdate, cancellationToken);

        if(!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);

        category.Name = categoryToUpdate.Name;

        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}