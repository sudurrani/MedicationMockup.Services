using AutoMapper;
using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Application.Shared.Common.Interfaces;
using MedicationMockup.Application.Shared.Dtos.Medication;
using MedicationMockup.Application.Shared.Interfaces;
using MedicationMockup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application
{
    public class MedicationAppService : IMedicationAppService
    {
        #region Services to be injected
        private readonly IMapper _mapper;
        private readonly IEFCoreRepository<Medication> _repository;
        #endregion
        #region Objects to be inialized
        ResponseOutputDto _responseOutputDto = new ResponseOutputDto();
        #endregion
        public MedicationAppService
            (
            IEFCoreRepository<Medication> repository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseOutputDto> GetAll()
        {
            var medicationEntities = await _repository.GetAll()
                                                   .Include(createdBy => createdBy.CreatedBy)
                                                   .Include(updatedBy => updatedBy.UpdatedBy)
                                                   .ToListAsync();
            var medicationOutputDtos = _mapper.Map<List<MedicationOutputDto>>(medicationEntities);
            _responseOutputDto.Success<List<MedicationOutputDto>>(medicationOutputDtos, $"Total {medicationOutputDtos.Count} record(s)");
            return _responseOutputDto;
        }
        public async Task<ResponseOutputDto> GetMy(long id)
        {
            var medicationEntities = await _repository.GetAll()
                                                   .Where(filter => filter.CreatedById == id)
                                                   .Include(createdBy => createdBy.CreatedBy)
                                                   .Include(updatedBy => updatedBy.UpdatedBy)
                                                   .ToListAsync();
            var medicationOutputDtos = _mapper.Map<List<MedicationOutputDto>>(medicationEntities);
            _responseOutputDto.Success<List<MedicationOutputDto>>(medicationOutputDtos, $"Total {medicationOutputDtos.Count} record(s)");
            return _responseOutputDto;
        }
        public async Task<ResponseOutputDto> GetById(long id)
        {
            if (id <= 0)
            {
                _responseOutputDto.Invalid("id field is required");
            }
            else
            {
                var medicationExist = await _repository.GetAll()
                                                   .Include(createdBy => createdBy.CreatedBy)
                                                   .Include(updatedBy => updatedBy.UpdatedBy)
                                                   .Where(filter => filter.Id == id).FirstOrDefaultAsync();
                if (medicationExist == null)
                {
                    _responseOutputDto.Warning($"Medication with id {id} does not exist please provide a valid Medication id");
                }
                else
                {
                    var medicationOutputDto = _mapper.Map<MedicationOutputDto>(medicationExist);
                    _responseOutputDto.Success<MedicationOutputDto>(medicationOutputDto);
                }

            }
            return _responseOutputDto;
        }
        public async Task<ResponseOutputDto> Create(MedicationInputDto medicationInputDto)
        {

            var medicationEntity = _mapper.Map<Medication>(medicationInputDto);
            var createdRowId = await _repository.Create(medicationEntity);
            if (createdRowId > 0)
            {
                medicationInputDto.Id = medicationEntity.Id;
                _responseOutputDto.Success<object>(medicationEntity, "Medication has been created successfully");

            }
            else
            {
                _responseOutputDto.Error();
            }


            return _responseOutputDto;

        }
        public async Task<ResponseOutputDto> Update(MedicationInputDto medicationInputDto)
        {
            var medicationExist = await _repository.GetAll()
                                               .Where(filter => filter.Id == medicationInputDto.Id)
                                               .FirstOrDefaultAsync();
            if (medicationExist == null)
            {
                _responseOutputDto.Warning($"Medication with id {medicationInputDto.Id} does not exist please provide a valid Medication");
            }
            else
            {
                var medicationEntity = _mapper.Map<Medication>(medicationInputDto);

                var affectedRows = await _repository.Update(medicationEntity);
                if (affectedRows > 0)
                {
                    medicationInputDto.Id = medicationEntity.Id;
                    _responseOutputDto.Success<object>(medicationEntity, "Medication has been updated successfully");

                }
                else
                {
                    _responseOutputDto.Error();
                }
            }
            return _responseOutputDto;
        }
        public async Task<ResponseOutputDto> Delete(MedicationInputDto medicationInputDto)
        {
            var medicationExist = await _repository.GetAll()
                                               .Where(filter => filter.Id == medicationInputDto.Id)
                                               .FirstOrDefaultAsync();
            if (medicationExist == null)
            {
                _responseOutputDto.Warning($"Medication with id {medicationInputDto.Id} does not exist please provide a valid Medication id");
            }
            else
            {
                medicationExist.IsDeleted = true;
                var affectedRows = await _repository.Delete(medicationExist);
                if (affectedRows > 0)
                {
                    _responseOutputDto.Success<object>(medicationExist, "Medication has been deleted successfully");
                }
                else
                {
                    _responseOutputDto.Error();
                }
            }

            return _responseOutputDto;
        }
    }
}

