using FluentValidation;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace ClinicBooking.Shared.Validators
{

    public class PatientRegisterRequestDtoValidator : AbstractValidator<PatientRegisterRequestDto>
    {
        private readonly IUserRepo _userRepo;

        public PatientRegisterRequestDtoValidator(IUserRepo userRepo)
        {
            _userRepo = userRepo;
            applyValidation();
            applyCustomValidation();



        }
        private void applyValidation()
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

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 120).WithMessage("Age must be between 0 and 120."); // Adjust range as needed

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender specified.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .NotNull().WithMessage("Username cannot be null.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .NotNull().WithMessage("Email cannot be null.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

          

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .NotNull().WithMessage("Password cannot be null.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .NotNull().WithMessage("Confirm Password cannot be null.")
                .Equal(x => x.Password).WithMessage("Password and confirmation do not match.");


        }
        private void applyCustomValidation()
        {
            // --- Asynchronous Validation Rules (Uniqueness Checks) ---
            RuleFor(x => x.Email)
                .MustAsync(async (email, cancellation) => !await _userRepo.UserExistByEmailAsync(email))
                .WithMessage("Email is already registered.");

            RuleFor(x => x.UserName)
                .MustAsync(async (userName, cancellation) => !await _userRepo.UserExistByNameAsync(userName))
                .WithMessage("Username is already taken.");
        }
    }
}
