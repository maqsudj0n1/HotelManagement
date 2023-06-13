using Application.DTOs.RoomClass;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entities;
using HotelManagement1.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomClassController : ApiControllerBase<RoomClass>
    {
        private readonly IRoomClassRepo _roomClassRepository;
        public RoomClassController(IRoomClassRepo roomClassRepository)
        {
            _roomClassRepository = roomClassRepository;
            Console.WriteLine("aaaaa");
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllRoomClass")]
        public async Task<ActionResult<ResponseCore<IEnumerable<RoomClassGetDto>>>> GetAll()
        {
            IEnumerable<RoomClass> RoomClasss = await _roomClassRepository.GetAsync(x => true);

            IEnumerable<RoomClassGetDto> mappedRoomClasss = _mapper.Map<IEnumerable<RoomClassGetDto>>(RoomClasss);

            return Ok(new ResponseCore<IEnumerable<RoomClassGetDto>>(mappedRoomClasss));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdRoomClass")]
        public async Task<ActionResult<ResponseCore<RoomClassGetDto>>> GetById(Guid id)
        {
            RoomClass? obj = await _roomClassRepository.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound(new ResponseCore<RoomClass?>(false, id + " not found!"));
            }
            RoomClassGetDto mappedRoomClass = _mapper.Map<RoomClassGetDto>(obj);
            return Ok(new ResponseCore<RoomClassGetDto?>(mappedRoomClass));
        }

        [HttpPut("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "UpdateRoomClass")]
        public async Task<ActionResult<ResponseCore<RoomClassGetDto>>> Update([FromBody] RoomClassUpdateDto RoomClass)
        {
            RoomClass? mappedRoomClass = _mapper.Map<RoomClass>(RoomClass);
            var validationResult = _validator.Validate(mappedRoomClass);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<RoomClass>(false, validationResult.Errors));
            }
            mappedRoomClass = await _roomClassRepository.UpdateAsync(mappedRoomClass);
            if (mappedRoomClass != null)
                return Ok(new ResponseCore<RoomClassGetDto>(_mapper.Map<RoomClassGetDto>(mappedRoomClass)));
            return BadRequest(new ResponseCore<RoomClass>(false, RoomClass + " not found"));

        }

        [HttpPost("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "CreateRoomClass")]
        public async Task<ActionResult<ResponseCore<RoomClassCreateDto>>> Create([FromBody] RoomClassCreateDto RoomClass)
        {
            RoomClass mappedRoomClass = _mapper.Map<RoomClass>(RoomClass);
            var validationResult = _validator.Validate(mappedRoomClass);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }
            mappedRoomClass = await _roomClassRepository.CreateAsync(mappedRoomClass);
            var res = _mapper.Map<RoomClassCreateDto>(mappedRoomClass);
            return Ok(new ResponseCore<RoomClassCreateDto>(res));

        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteRoomClass")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _roomClassRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
