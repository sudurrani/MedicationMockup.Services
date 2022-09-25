using AutoMapper;
using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Application.Shared.Dtos.Medication;
using MedicationMockup.Application.Shared.Interfaces;
using MedicationMockup.Application.Shared.Models.Medication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicationMockup.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicationController : ControllerBase
    {
        #region Services to be inject
        //ICountryAppService _countryAppService;
        IMedicationAppService _medicationsAppService;
        IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        #endregion
        ResponseOutputDto _responseOutputDto;
        public MedicationController
            (
            IMedicationAppService medicationsAppService,
            IMapper mapper,
            IWebHostEnvironment environment
            )
        {
            _medicationsAppService = medicationsAppService;
            _mapper = mapper;
            _environment = environment;

            _responseOutputDto = new ResponseOutputDto();
        }
        [HttpGet]
        [Route("GetAll")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> GetAll()
        {
            _responseOutputDto = await _medicationsAppService.GetAll();
            return Ok(_responseOutputDto);
        }
        [HttpGet]
        [Route("GetMy/{id}")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> GetMy(long id)
        {
            _responseOutputDto = await _medicationsAppService.GetMy(id);
            return Ok(_responseOutputDto);
        }
        [HttpGet]
        [Route("GetById/{id}")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> GetById(long id)
        {
            _responseOutputDto = await _medicationsAppService.GetById(id);
            return Ok(_responseOutputDto);
        }
        //[HttpGet]
        //[Route("Search/{text}")]
        //[Produces(typeof(ResponseOutputDto))]
        //public async Task<IActionResult> Search(string text)
        //{
        //    _responseOutputDto = await _medicationsAppService.Search(text);
        //    return Ok(_responseOutputDto);
        //}
        [HttpPost]
        [Route("Create")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> Create(MedicationCreateModel objectsModel)
        {

            var objectsInputDto = _mapper.Map<MedicationInputDto>(objectsModel);
            _responseOutputDto = await _medicationsAppService.Create(objectsInputDto);
            return Ok(_responseOutputDto);
        }
        [HttpPost]
        [Route("Update")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> Update(MedicationUpdateModel updateMedicationModel)
        {

            var objectsInputDto = _mapper.Map<MedicationInputDto>(updateMedicationModel);
            _responseOutputDto = await _medicationsAppService.Update(objectsInputDto);
            return Ok(_responseOutputDto);
        }
        [HttpPost]
        [Route("Delete")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> Delete(MedicationDeleteModel objectDeleteModel)
        {
            var objectsInputDto = _mapper.Map<MedicationInputDto>(objectDeleteModel);
            _responseOutputDto = await _medicationsAppService.Delete(objectsInputDto);
            return Ok(_responseOutputDto);
        }
    }
}
