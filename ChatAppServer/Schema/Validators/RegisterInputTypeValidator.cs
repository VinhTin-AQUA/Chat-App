
using ChatAppServer.Schema.Types.DTOTypes;
using FluentValidation;

namespace ChatAppServer.Schema.Validators
{
    public class RegisterInputTypeValidator : AbstractValidator<RegisterInputType>
    {
        public RegisterInputTypeValidator()
        {
            RuleFor(c => c.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull()
                .WithMessage("Email is incorrect");

            RuleFor(c => c.Password)
                .MinimumLength(8)
                .MaximumLength(18)
                .WithMessage("Password is at least 8 and max is 18 characters");

            RuleFor(c => c.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Full name is required");

            RuleFor(c => c.ReEnterPassword)
                .Equal(c => c.Password)
                .WithMessage("Re-enter password is not matched");
        }
    }
}
