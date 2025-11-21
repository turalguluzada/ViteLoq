using FluentValidation;
using ViteLoq.Application.DTOs.UserManagement;

namespace ViteLoq.Application.Validators.UserManagement
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(8);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(0);
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(0);
            
            // RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(100);

            // When(x => x.InitialProfile != null, () =>
            // {
            //     RuleFor(x => x.InitialProfile).SetValidator(new UserDetailDtoValidator());
            // });
        }
    }
}