using AutoMapper;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IGenericRepository<Availability> _genericRepo;
        private readonly IAvailabilityRepo _availabilityRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;
        public AvailabilityService(IGenericRepository<Availability> genericRepo, IMapper mapper, IDoctorRepo doctorRepo, IAvailabilityRepo availabilityRepo)
        {
            _genericRepo = genericRepo;
            _mapper = mapper;
            _doctorRepo = doctorRepo;
            _availabilityRepo = availabilityRepo;
        }
        public async Task<ResultT<AvailabilityListDto>> Add(AddAvailabilityRequestDto dto, int userId)
        {
            var doctor = await _doctorRepo.GetByUserIdAsync(userId);
            if (doctor == null)
            {
                return ResultT<AvailabilityListDto>.Failure(
                    StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound", "Doctor not found")
                );
            }
           
            var isOverLapping = await _availabilityRepo.IsOverlappingAsync(doctor.Id, dto.Day, 
                dto.StartTime, dto.EndTime,dto.EffectiveFrom,dto.EffectiveTo);
            if (isOverLapping)
            {
                return ResultT<AvailabilityListDto>.Failure(StatusCodes.Status409Conflict,
                   new Error("AVAILABILITY.OVERLAPPING", "This time slot overlaps with another one."));

            }
            var availability = _mapper.Map<Availability>(dto);
            availability.DoctorId = doctor.Id;
            await _genericRepo.AddAsync(availability);
            await _genericRepo.SaveChangesAsync();
            var dtoAfterAdded = _mapper.Map<AvailabilityListDto>(availability);
            return ResultT<AvailabilityListDto>.Success(StatusCodes.Status201Created, dtoAfterAdded);

        }

        public async Task<Result> DeleteAsync(int availabilityId,int userId)
        {
            var doctor = await _doctorRepo.GetByUserIdAsync(userId);
            if (doctor == null)
            {
                return ResultT<AvailabilityListDto>.Failure(
                    StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound", "Doctor not found")
                );
            }
            var toDeleteAvailabilty = await _availabilityRepo.GetByIdAndDoctorIdAsync(availabilityId,doctor.Id);
            if (toDeleteAvailabilty == null)
            {
                return Result.Failure(StatusCodes.Status404NotFound, new Error("AvailabilityNotFound"
                 , "Availability not found"));
            }
            await _genericRepo.DeleteAsync(toDeleteAvailabilty);
            await _genericRepo.SaveChangesAsync();
            return Result.Success(StatusCodes.Status200OK);


        }

        public async Task<ResultT<AvailabilityDto>> GeById(int availabilityId, int userId)
        {
            var doctor = await _doctorRepo.GetByUserIdAsync(userId);
            if (doctor == null)
            {
                return ResultT<AvailabilityDto>.Failure(
                    StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound", "Doctor not found")
                );
            }
            var availability = await _availabilityRepo.GetByIdAndDoctorIdAsync(availabilityId, doctor.Id);
            if (availability == null)
                return ResultT<AvailabilityDto>.Failure(StatusCodes.Status404NotFound, new Error("AvailabilityNotFound"
                    , "Availability not found"));
            var dto = _mapper.Map<AvailabilityDto>(availability);
            return ResultT<AvailabilityDto>.Success(StatusCodes.Status200OK, dto);
        }


        public async Task<ResultT<List<AvailabilityListDto>>> GetAllByDoctorId(int userId)
        {
            var doctor = await _doctorRepo.GetByUserIdAsync(userId);
            if (doctor == null)
            {
                return ResultT<List<AvailabilityListDto>>.Failure(
                    StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound", "Doctor not found")
                );
            }
            var availbilities = await _availabilityRepo.GetAllByDoctorIdAsync(doctor.Id);
            if (availbilities == null)
                return ResultT<List<AvailabilityListDto>>.Failure(StatusCodes.Status404NotFound, new Error("No Availabilites", "No Availabilites"));
            var availabilitiesDto = _mapper.Map<List<AvailabilityListDto>>(availbilities);
            return ResultT<List<AvailabilityListDto>>.Success(StatusCodes.Status200OK, availabilitiesDto);
        }

        public async Task<Result> UpdateAvailabilityAsync(int availabilityId, UpdateAvailabilityRequestDto dto, int userId)
        {
            // 1. Get availability record
            var doctor = await _doctorRepo.GetByUserIdAsync(userId);
            var doctorId = doctor.Id;
            var availability = await _availabilityRepo.GetByIdAndDoctorIdAsync(availabilityId, doctorId);
            if (availability == null)
            {
                return Result.Failure(StatusCodes.Status404NotFound,
                    new Error("AVAILABILITY.NOT_FOUND", "Availability slot not found."));
            }

            // 2. Validate logic
            if (dto.StartTime >= dto.EndTime)
            {
                return Result.Failure(StatusCodes.Status400BadRequest,
                    new Error("AVAILABILITY.INVALID_TIME", "Start time must be earlier than end time."));
            }

            if (dto.SlotDurationMinutes <= 0)
            {
                return Result.Failure(StatusCodes.Status400BadRequest,
                    new Error("AVAILABILITY.INVALID_DURATION", "Slot duration must be greater than zero."));
            }

            // 3. Check overlapping (optional: exclude current ID)
            var isOverlapping = await _availabilityRepo.IsOverlappingAsync(
                doctorId, dto.Day, dto.StartTime, dto.EndTime,
                dto.EffectiveFrom,dto.EffectiveTo,
                excludeId: availabilityId);

            if (isOverlapping)
            {
                return Result.Failure(StatusCodes.Status409Conflict,
                    new Error("AVAILABILITY.OVERLAPPING", "This time slot overlaps with another one."));
            }

            // 4. Update properties
            availability.Day = dto.Day;
            availability.StartTime = dto.StartTime;
            availability.EndTime = dto.EndTime;
            availability.SlotDurationMinutes = dto.SlotDurationMinutes;
            availability.EffectiveFrom = dto.EffectiveFrom;
            availability.EffectiveTo = dto.EffectiveTo;

            // 5. Save
            await _genericRepo.SaveChangesAsync();

            return Result.Success(StatusCodes.Status204NoContent);
        }
        public async Task<ResultT<List<DateTime>>> GetAvailableSlotsByDoctorIdAsync(int doctorId, DateTime fromDate, DateTime toDate)
        {
            var availabilities = await _availabilityRepo.GetDoctorAvailabilitiesAsync(doctorId, fromDate, toDate);
            var bookedSlots = await _availabilityRepo.GetDoctorBookedAppointmentsAsync(doctorId, fromDate, toDate);

            var availableSlots = new List<DateTime>();

            foreach (var availability in availabilities)
            {
                for (var date = fromDate.Date; date <= toDate.Date; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != availability.Day)
                        continue;

                    var startTime = date.Add(availability.StartTime.ToTimeSpan());
                    var endTime = date.Add(availability.EndTime.ToTimeSpan());
                    var slotDuration = TimeSpan.FromMinutes(availability.SlotDurationMinutes);

                    for (var slot = startTime; slot + slotDuration <= endTime; slot = slot.Add(slotDuration))
                    {
                        if (!bookedSlots.Contains(slot))
                        {
                            availableSlots.Add(slot);
                        }
                    }
                }
            }

            return ResultT<List<DateTime>>.Success(StatusCodes.Status200OK, availableSlots.OrderBy(s => s).ToList());
        }
    }
}
