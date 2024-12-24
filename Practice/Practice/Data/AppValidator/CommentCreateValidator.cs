using FluentValidation;
using Practice.Data.Dto;

namespace Practice.Data.AppValidator
{
    public class CommentCreateValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateValidator() 
        {
            RuleFor(x => x.Comment)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(2000);
        }
    }
}