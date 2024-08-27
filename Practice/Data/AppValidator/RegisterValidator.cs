using FluentValidation;
using FluentValidation.Validators;
using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Data.AppValidator
{
    public class RegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public RegisterValidator() 
        {
            RuleFor(x => x.FullName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(24);

            RuleFor(x => x.BirthDay)
                .NotEmpty()
                .NotNull()
                .LessThan(DateTime.Now).WithMessage("Date must be less than now")
                .GreaterThan(new DateTime(1950, 1, 1)).WithMessage("Date must be greater than 1950.1.1");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(64);

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(7)
                .MaximumLength(11);
        }
    }
}