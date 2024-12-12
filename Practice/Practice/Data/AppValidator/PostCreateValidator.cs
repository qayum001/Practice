using FluentValidation;
using Practice.Data.Dto;
using System.Collections;

namespace Practice.Data.AppValidator
{
    public class PostCreateValidator : AbstractValidator<PostCreateDto>
    {
        public PostCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(24);

            RuleFor(x => x.Text)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(2000);

            RuleFor(x => x.TagGuidList)
                .NotEmpty()
                .NotNull()
                .Must(x => x.Count > 0);

            RuleFor(x => x.ReadTime)
                .GreaterThan(0)
                .LessThan(61);
        }
    }
}
