using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Validators
{
    public class AddAvailabilityRequestDtoValidator:AbstractValidator<AddAvailabilityRequestDto>
    {
       
        public AddAvailabilityRequestDtoValidator()
        {
            applyValidation();
            applyCustomValidation();   
        }
        private void applyValidation()
        {
            RuleFor(x => x.Day)
                .IsInEnum().WithMessage("\"Invalid day of the week specified.");
            RuleFor(x => x.StartTime)
               .NotNull().WithMessage("Start time is required."); // Ensure it's not null before comparison

            RuleFor(x => x.EndTime)
                .NotNull().WithMessage("End time is required.")
                .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
            RuleFor(x => x.EffectiveFrom)
                  .NotNull().WithMessage("Effective from date is required.")
                  .Must(date => date.Date >= DateTime.Today).WithMessage("Effective from date cannot be in the past.");

            RuleFor(x => x.EffectiveTo)
                .NotNull().WithMessage("Effective to date is required.")
                .GreaterThan(x => x.EffectiveFrom).WithMessage("Effective to date must be after effective from date.");
            RuleFor(x => x.SlotDurationMinutes).GreaterThan(0).WithMessage("Slot duration minutes must be more thant 0");
        }
        private void applyCustomValidation()
        {

        }
    }
}
