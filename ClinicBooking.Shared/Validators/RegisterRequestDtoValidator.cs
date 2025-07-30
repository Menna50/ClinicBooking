using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        private readonly IUserRepo _userRepo;
        public RegisterRequestDtoValidator(IUserRepo userRepo)
        {
            _userRepo = userRepo;
            applyValidation();
            applyCustomValidation();
        }
      
        private void applyValidation()
        {
            //userName
            RuleFor(x => x.UserName)
              .NotEmpty().WithMessage("Username is required.")
              .NotNull().WithMessage("Username cannot be null.")
              .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
              .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            //Email
            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required.")
             .NotNull().WithMessage("Email cannot be null.")
             .EmailAddress().WithMessage("Invalid email format.");

            //phone
            RuleFor(x => x.PhoneNumber)
               .NotEmpty().WithMessage("Phone number is required.")
               .NotNull().WithMessage("Phone number cannot be null.")
                .Matches(@"^[0-9]{10,15}$")
        .WithMessage("Phone number is not valid. It should contain only digits and be 10 to 15 digits long."); ;



          //Password        
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .NotNull().WithMessage("Password cannot be null.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            // Validation rules for ConfirmPassword (User's ConfirmPassword) - moved from service
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .NotNull().WithMessage("Confirm Password cannot be null.")
                .Equal(x => x.Password).WithMessage("Password and confirmation do not match.");


        }
        private void applyCustomValidation()
        {
            RuleFor(x => x.UserName).MustAsync( async (userName, cancellation) =>
            {
                return ! await _userRepo.UserExistByNameAsync(userName);
            }).WithMessage("User name is already exist.");
            RuleFor(x => x.Email).MustAsync(async (emial, cancellation) =>
            {
                return  !await _userRepo.UserExistByEmailAsync(emial);
            }

  ).WithMessage("Email is already exist.");
        }
    }

}
