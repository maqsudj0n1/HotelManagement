using Application.DTOs.Room;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entities;
using FluentValidation;
using HotelManagement1.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ApiControllerBase<Room>
    {
        private readonly IRoomRepo _roomRepository;
        public RoomController(IRoomRepo roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllRoom")]
        public async Task<ActionResult<ResponseCore<IEnumerable<RoomGetDto>>>> GetAll()
        {
            IEnumerable<Room> Rooms = await _roomRepository.GetAsync(x => true);

            IEnumerable<RoomGetDto> mappedRooms = _mapper.Map<IEnumerable<RoomGetDto>>(Rooms);

            return Ok(new ResponseCore<IEnumerable<RoomGetDto>>(mappedRooms));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdRoom")]
        public async Task<ActionResult<ResponseCore<RoomGetDto>>> GetById(Guid id)
        {
            Room? obj = await _roomRepository.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound(new ResponseCore<Room?>(false, id + " not found!"));
            }
            RoomGetDto mappedRoom = _mapper.Map<RoomGetDto>(obj);
            return Ok(new ResponseCore<RoomGetDto?>(mappedRoom));
        }

        [HttpPut("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "UpdateRoom")]
        public async Task<ActionResult<ResponseCore<RoomGetDto>>> Update([FromBody] RoomUpdateDto Room)
        {
            Room? mappedRoom = _mapper.Map<Room>(Room);
            var validationResult = _validator.Validate(mappedRoom);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Room>(false, validationResult.Errors));
            }
            mappedRoom = await _roomRepository.UpdateAsync(mappedRoom);
            if (mappedRoom != null)
                return Ok(new ResponseCore<RoomGetDto>(_mapper.Map<RoomGetDto>(mappedRoom)));
            return BadRequest(new ResponseCore<Room>(false, Room + " not found"));

        }

        [HttpPost("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "CreateRoom")]
        public async Task<ActionResult<ResponseCore<RoomCreateDto>>> Create([FromBody] RoomCreateDto Room)
        {
            Room mappedRoom = _mapper.Map<Room>(Room);
            var validationResult = _validator.Validate(mappedRoom);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }
            mappedRoom = await _roomRepository.CreateAsync(mappedRoom);
            var res = _mapper.Map<RoomCreateDto>(mappedRoom);
            return Ok(new ResponseCore<RoomCreateDto>(res));

        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteRoom")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _roomRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
