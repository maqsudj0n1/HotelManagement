using Application.DTOs.Employee;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using HotelManagement1.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelManagement1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ApiControllerBase<Employee>
    {
        private readonly IEmployeeRepo _employeeRepository;
        private readonly IRoleRepository _roleRepository;

        public EmployeeController(IEmployeeRepo employeeRepository, IRoleRepository roleRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllEmployee")]
        [LazyCache(5, 10)]
        [EnableRateLimiting("token")]
        public async Task<ActionResult<ResponseCore<IEnumerable<EmployeeGetDto>>>> GetAll()
        {
            IEnumerable<Employee> owners = await _employeeRepository.GetAsync(x => true);

            IEnumerable<EmployeeGetDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeGetDto>>(owners);

            return Ok(new ResponseCore<IEnumerable<EmployeeGetDto>>(mappedEmployees));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdEmployee")]
        public async Task<ActionResult<ResponseCore<EmployeeGetDto>>> GetById(Guid id)
        {
            Employee? obj = await _employeeRepository.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound(new ResponseCore<Employee?>(false, id + " not found!"));
            }
            EmployeeGetDto mappedEmployee = _mapper.Map<EmployeeGetDto>(obj);
            return Ok(new ResponseCore<EmployeeGetDto?>(mappedEmployee));
        }

        [HttpPut("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "UpdateEmployee")]
        public async Task<ActionResult<ResponseCore<EmployeeGetDto>>> Update([FromBody] EmployeeUpdateDto owner)
        {
            Employee? mappedEmployee = _mapper.Map<Employee>(owner);
            var validationResult = _validator.Validate(mappedEmployee);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Employee>(false, validationResult.Errors));
            }
            mappedEmployee.Roles = new List<Role>();
            foreach (Guid item in owner.Roles)
            {
                Role? role = await _roleRepository.GetByIdAsync(item);
                if (role != null)
                    mappedEmployee.Roles.Add(role);
                else return BadRequest(new ResponseCore<Employee>(false, item + " Id not found"));
            }
            mappedEmployee = await _employeeRepository.UpdateAsync(mappedEmployee);
            if (mappedEmployee != null)
                return Ok(new ResponseCore<EmployeeGetDto>(_mapper.Map<EmployeeGetDto>(mappedEmployee)));
            return BadRequest(new ResponseCore<Employee>(false, owner + " not found"));

        }

        [HttpPost("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "CreateEmployee")]
        public async Task<ActionResult<ResponseCore<EmployeeCreateDto>>> Create([FromBody] EmployeeCreateDto owner)
        {
            Employee mappedEmployee = _mapper.Map<Employee>(owner);
            var validationResult = _validator.Validate(mappedEmployee);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }
            mappedEmployee.Roles = new List<Role>();
            foreach (Guid item in owner.Roles)
            {
                Role? role = await _roleRepository.GetByIdAsync(item);
                if (role != null)
                    mappedEmployee.Roles.Add(role); 
                else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
            }
            mappedEmployee = await _employeeRepository.CreateAsync(mappedEmployee);
            var res = _mapper.Map<EmployeeCreateDto>(mappedEmployee);
            return Ok(new ResponseCore<EmployeeCreateDto>(res));

        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteEmployee")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _employeeRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
