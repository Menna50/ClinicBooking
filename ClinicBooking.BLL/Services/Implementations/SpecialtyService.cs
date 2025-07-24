using AutoMapper;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class SpecialtyService:ISpecialtyService
    {
        private readonly IGenericRepository<Specialty> _genericRepo;
        private readonly IMapper _mapper;
        public SpecialtyService(IMapper mapper, IGenericRepository<Specialty> genericRepo)
        {
          
            _mapper = mapper;
            _genericRepo = genericRepo;
        }
        public async Task<ResultT<List<SpecialtyDto>>> GetAllAsync()
        {
            var spes= await _genericRepo.GetAllAsync();
            var dtos = _mapper.Map<List<SpecialtyDto>>(spes); 

            return ResultT<List<SpecialtyDto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<ResultT<SingleSpecialtyDto>> GetByIdAsync(int id)
        {
            var specialty = await _genericRepo.GetByIdAsync(id);
            if (specialty == null)
                return ResultT<SingleSpecialtyDto>.Failure(StatusCodes.Status404NotFound, 
                    new Error("Specialty not found", "Specialty not found"));

            var dto = new SingleSpecialtyDto
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description
            };

            return ResultT<SingleSpecialtyDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<Result> AddAsync(CreateSpecialtyDto dto)
        {
            var specialty = new Specialty
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _genericRepo.AddAsync(specialty);
            await _genericRepo.SaveChangesAsync();
            return Result.Success(StatusCodes.Status201Created);
        }

        public async Task<Result> UpdateAsync(UpdateSpecialtyDto dto)
        {
            var existing = await _genericRepo.GetByIdAsync(dto.Id);
            if (existing == null)
                return Result.Failure(StatusCodes.Status404NotFound, 
                    new Error("Specialty not found", "Specialty not found"));

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            await _genericRepo.SaveChangesAsync();
            return Result.Success(StatusCodes.Status200OK);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var specialty = await _genericRepo.GetByIdAsync(id);
            if (specialty == null)
                return Result.Failure(StatusCodes.Status404NotFound, 
                    new Error("Specialty not found", "Specialty not found"));

         await   _genericRepo.DeleteAsync(specialty);
            await _genericRepo.SaveChangesAsync();
            return Result.Success(StatusCodes.Status200OK);
        }
    }
}
