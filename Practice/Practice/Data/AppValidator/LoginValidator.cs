using FluentValidation;
using FluentValidation.Validators;
using Practice.Data.Dto;

namespace Practice.Data.AppValidator
{
    public class LoginValidator : AbstractValidator<LoginCredentials>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(64);
        }
    }
}
