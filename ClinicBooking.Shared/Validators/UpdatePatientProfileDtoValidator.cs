using ClinicBooking.Shared.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Validators
{
    public class UpdatePatientProfileDtoValidator:AbstractValidator<UpdatePatientProfileDto>
    {
        public UpdatePatientProfileDtoValidator()
        {
            applyValidation();
        }
        public void applyValidation()
        {
            RuleFor(x => x.FName)
          .NotEmpty().WithMessage("First name is required.")
          .NotNull().WithMessage("First name cannot be null.")
          .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
          .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LName)
                .NotEmpty().WithMessage("Last name is required.")
                .NotNull().WithMessage("Last name cannot be null.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(x => x.PhoneNumber)
              .NotEmpty().WithMessage("Phone number is required.")
              .NotNull().WithMessage("Phone number cannot be null.")
               .Matches(@"^[0-9]{10,15}$")
        .WithMessage("Phone number is not valid. It should contain only digits and be 10 to 15 digits long."); ;
        }
    }
}
