// File: ClinicBooking.Shared/Validators/DoctorRegisterDtoValidator.cs

using FluentValidation;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.DAL.Repositories.Interfaces; 
using System.Threading.Tasks;
using System.Threading;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Repositories.Implemetations; // For Role enum (if needed for internal validation)

namespace ClinicBooking.Shared.Validators
{
    public class DoctorRegisterDtoValidator : AbstractValidator<DoctorRegisterDto>
    {
        private readonly IUserRepo _userRepo;
        private readonly ISpecialtyRepo _specialtyRepo; // ✅ Inject ISpecialtyRepo

        public DoctorRegisterDtoValidator(IUserRepo userRepo, ISpecialtyRepo specialtyRepo)
        {
            _userRepo = userRepo;
            _specialtyRepo = specialtyRepo;

            // Validation rules for Name (Doctor's Name)
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Doctor's name is required.")
                .NotNull().WithMessage("Doctor's name cannot be null.")
                .MinimumLength(3).WithMessage("Doctor's name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Doctor's name cannot exceed 100 characters.");

            // Validation rules for UserName (User's Username) - similar to RegisterRequestDto
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .NotNull().WithMessage("Username cannot be null.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            // Validation rules for Email (User's Email) - similar to RegisterRequestDto
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .NotNull().WithMessage("Email cannot be null.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            // Validation rules for Password (User's Password) - similar to RegisterRequestDto
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .NotNull().WithMessage("Password cannot be null.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            // Validation rules for ConfirmPassword (User's ConfirmPassword) - moved from service
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .NotNull().WithMessage("Confirm Password cannot be null.")
                .Equal(x => x.Password).WithMessage("Password and confirmation do not match."); 

            // Validation rules for Bio
            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.")
                .When(x => x.Bio != null); // Only validate if Bio is provided

            // Validation rules for ConsultationFee
            RuleFor(x => x.ConsultationFee)
                .GreaterThanOrEqualTo(0).WithMessage("Consultation fee cannot be negative.");

            // Validation rules for PhoneNumber (User's PhoneNumber) - similar to RegisterRequestDto
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .NotNull().WithMessage("Phone number cannot be null.")
                 .Matches(@"^[0-9]{10,15}$")
        .WithMessage("Phone number is not valid. It should contain only digits and be 10 to 15 digits long."); ;

            // Validation rules for SpecialtyId
            RuleFor(x => x.SpecialtyId)
                .NotEmpty().WithMessage("Specialty ID is required.")
                .GreaterThan(0).WithMessage("Specialty ID must be a positive number.");

           
        }
        public void applyCustomValidation()
        {
            // ✅ Asynchronous Validation Rules

            // Rule for Email uniqueness
            RuleFor(x => x.Email)
                .MustAsync(async (email, cancellation) => !await _userRepo.UserExistByEmailAsync(email))
                .WithMessage("Email is already registered.");

            // Rule for Username uniqueness
            RuleFor(x => x.UserName)
                .MustAsync(async (userName, cancellation) => !await _userRepo.UserExistByNameAsync(userName))
                .WithMessage("Username is already taken.");

            // Rule for SpecialtyId existence
            RuleFor(x => x.SpecialtyId)
                .MustAsync(async (specialtyId, cancellation) => await _specialtyRepo.SpecialtyExistsAsync(specialtyId))
                .WithMessage("Specialty not found.");
        }
    }
}
