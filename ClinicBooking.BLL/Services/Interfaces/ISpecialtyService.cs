using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface ISpecialtyService
    {
        public Task<ResultT<List<SpecialtyDto>>> GetAllAsync();

        Task<ResultT<SingleSpecialtyDto>> GetByIdAsync(int id);
        Task<Result> AddAsync(CreateSpecialtyDto dto);
        Task<Result> UpdateAsync(UpdateSpecialtyDto dto);
        Task<Result> DeleteAsync(int id);
    }
}
