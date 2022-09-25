using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Application.Shared.Common.Interfaces;
using MedicationMockup.Application.Shared.Dtos.User;
using MedicationMockup.Application.Shared.Interfaces;
using MedicationMockup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application
{
    public class UserAppService : IUserAppService
    {
        #region Services to be injected
        private readonly IMapper _mapper;
        private readonly IEFCoreRepository<User> _repository;
        #endregion
        #region Objects to be inialized
        ResponseOutputDto _responseOutputDto = new ResponseOutputDto();
        #endregion
        public UserAppService
            (
            IEFCoreRepository<User> repository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _repository = repository;
        }

        #region GET Requests
        public async Task<ResponseOutputDto> GetAll()
        {
            //Using stored procedure
            //var sqlDynamicParameters = new SqlDynamicParameters();
            ///* Add paramerter(s) like below if any
            // * sqlDynamicParameters.Add("Id", value: id,dbType:DbType.Int32);                        
            // */
            //var result = await _repository.GetAll<UserOutputDto>("User_GetAll", sqlDynamicParameters, true);
            //_responseOutputDto.Success(result, $"Total {result.ToList().Count} record(s)");
            //return _responseOutputDto;


            var userEntities = await _repository.GetAll().ToListAsync();
            //var userEntities = await _repository.GetAll().Include(user => user.Country).Select(row => new UserOutputDto
            //{
            //    Username = row.Username,
            //    Email = row.Email,
            //    Password = row.Password,
            //    ImageNewName = row.ImageNewName,
            //    ImageOrignalName = row.ImageOrignalName,
            //    ImageExtension = row.ImageExtension,
            //    ImageAbsolutePath = row.ImageAbsolutePath,
            //    ImageRelatvePath = row.ImageRelatvePath,
            //    CountryId = row.CountryId,
            //    CountryName = row.Country.Name,
            //
            //
            //
            //}).ToListAsync();
            var userOutputDtos = _mapper.Map<List<UserOutputDto>>(userEntities);
            _responseOutputDto.Success(userOutputDtos, $"Total {userEntities?.Count} record(s)");
            return _responseOutputDto;
        }
        public async Task<ResponseOutputDto> GetById(long id)
        {
            var userEntity = await _repository.GetAll().Where(condition => condition.Id == id).FirstOrDefaultAsync();
            var userOutputDto = new UserOutputDto();
            if (userEntity != null)
            {
                userOutputDto = _mapper.Map<UserOutputDto>(userEntity);
                _responseOutputDto.Success<UserOutputDto>(userOutputDto);
            }
            else
            {
                _responseOutputDto.Invalid($"User not found for provided id {id}");
            }

            return _responseOutputDto;
        }
        #endregion
        #region POST Requests

        public async Task<ResponseOutputDto> Create(UserInputDto userInputDto)
        {
            var nickNameExist = await _repository.GetAll().Where(condition => condition.Username == userInputDto.Username).FirstOrDefaultAsync();
            if (nickNameExist != null)
            {
                _responseOutputDto.Warning($"Username {userInputDto.Username} already exist please try another one");
            }           
            else
            {

                var allUsers = await _repository.GetAll().ToListAsync();
                int userCount = allUsers.Count();
                string pin = "XS";
                pin = pin + userCount.ToString().PadLeft(3, '0');

                var userEntity = _mapper.Map<User>(userInputDto);
                userEntity.PIN = pin;
                var createdRowId = await _repository.Create(userEntity);
                if (createdRowId > 0)
                {
                    userInputDto.Id = userEntity.Id;
                    _responseOutputDto.Success<object>(userEntity, "User has been created successfully");

                }
                else
                {
                    _responseOutputDto.Error();
                }
            }
            return _responseOutputDto;

        }
        public async Task<ResponseOutputDto> Update(UserInputDto userInputDto)
        {
            var userExist = await _repository.GetAll().Where(condition => condition.Id == userInputDto.Id).FirstOrDefaultAsync();
            if (userExist == null)
            {
                _responseOutputDto.Warning($"User with id {userInputDto.Id} does no exist please provide a valid user");
            }
            else
            {
                userInputDto.Username = userExist.Username;
               

                var userEntity = _mapper.Map<User>(userInputDto);

                var affectedRows = await _repository.Update(userEntity);
                if (affectedRows > 0)
                {
                    _responseOutputDto.Success<object>(userEntity, "User has been modified successfully");
                }
                else
                {
                    _responseOutputDto.Error();
                }
            }
            return _responseOutputDto;

        }
        public async Task<ResponseOutputDto> Delete(UserInputDto userInputDto)
        {
            
            var userExist = await _repository.GetAll().Where(condition => condition.Id == userInputDto.Id).FirstOrDefaultAsync();
            if (userExist == null)
            {
                _responseOutputDto.Warning($"User with id {userInputDto.Id} does no exist please provide a valid user");
            }
            else
            {
                //userInputDto.Username = userExist.Username;
                //if (string.IsNullOrEmpty(userInputDto.ImagePath))
                //{
                //    userInputDto.ImageOrignalName = userExist.ImageOrignalName;
                //    userInputDto.ImageNewName = userExist.ImageNewName;
                //    userInputDto.ImageExtension = userExist.ImageExtension;
                //    userInputDto.ImagePath = userExist.ImagePath;
                //}
                userExist.IsDeleted = true;
                var affectedRows = await _repository.Delete(userExist);
                if (affectedRows > 0)
                {
                    _responseOutputDto.Success<object>(userInputDto, "User has been deleted successfully");
                }
                else
                {
                    _responseOutputDto.Error();
                }
            }
            return _responseOutputDto;

        }
        public async Task<ResponseOutputDto> Login(UserInputDto userInputDto)
        {
            var userEntityByUsername = await _repository.GetAll().Where(filtter => filtter.Username == userInputDto.Username).FirstOrDefaultAsync();//.ToListAsync();
            if (userEntityByUsername == null)
            {
                _responseOutputDto.Invalid("incorrect username");
            }
            else
            {

                if (userEntityByUsername.Password != userInputDto.Password)
                {
                    _responseOutputDto.Invalid("incorrect password");
                }
                else
                {
                    var userOutputDto = _mapper.Map<UserLoginOutputDto>(userEntityByUsername);
                    _responseOutputDto.Success<object>(userOutputDto);

                }
            }
            return _responseOutputDto;

        }
        #endregion
    }
}
