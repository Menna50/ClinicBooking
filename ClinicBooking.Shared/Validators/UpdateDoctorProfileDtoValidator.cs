using ClinicBooking.Shared.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Validators
{
    public class UpdateDoctorProfileDtoValidator : AbstractValidator<UpdateDoctorProfileDto>
    {
        public UpdateDoctorProfileDtoValidator()
        {
            applyValidation();
        }
        private void applyValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Doctor's name is required.")
               .NotNull().WithMessage("Doctor's name cannot be null.")
               .MinimumLength(3).WithMessage("Doctor's name must be at least 3 characters long.")
               .MaximumLength(100).WithMessage("Doctor's name cannot exceed 100 characters.");

            RuleFor(x => x.Bio)
                .NotNull().WithMessage("Bio cannot be null.")
                .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.");

            // Validation rules for PhoneNumber
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .NotNull().WithMessage("Phone number cannot be null.")
                .Matches(@"^[0-9]{10,15}$")
        .WithMessage("Phone number is not valid. It should contain only digits and be 10 to 15 digits long.");


            // Validation rules for ConsultationFee
            RuleFor(x => x.ConsultationFee)
                .GreaterThanOrEqualTo(0).WithMessage("Consultation fee cannot be negative.");

        }
    }
}
