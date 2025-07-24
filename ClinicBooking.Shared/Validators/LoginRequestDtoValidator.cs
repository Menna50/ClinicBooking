using ClinicBooking.Shared.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Validators
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            applyValidation();
        }
        private void applyValidation()
        {
            RuleFor(x => x.Identifier)
                .NotEmpty().WithMessage("Identifier (username or email) is required.")
                .NotNull().WithMessage("Identifier cannot be null.")
                .MaximumLength(100).WithMessage("Identifier cannot exceed 100 characters.");
           

           
            RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .NotNull().WithMessage("Password cannot be null.");
        }

    }
}
