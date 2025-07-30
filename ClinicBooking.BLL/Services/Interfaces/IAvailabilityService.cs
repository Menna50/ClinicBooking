using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IAvailabilityService
    {
        public Task<ResultT<AvailabilityListDto>> Add(AddAvailabilityRequestDto addAvailabilityRequestDto,int doctorId);
        public Task<ResultT<List<AvailabilityListDto>>> GetAllByDoctorId(int id);
        public Task<ResultT<AvailabilityDto>> GeById(int availabilityId, int doctorId);

        public Task<Result> UpdateAvailabilityAsync(int availabilityId, UpdateAvailabilityRequestDto dto, int doctorId);
        public Task<Result> DeleteAsync(int availabilityId,int doctorId);
        Task<ResultT<List<DateTime>>> GetAvailableSlotsByDoctorIdAsync(int doctorId, DateTime fromDate, DateTime toDate);


    }
}
