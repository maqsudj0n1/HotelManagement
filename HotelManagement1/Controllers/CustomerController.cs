using Application.DTOs.Customer;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entities;
using HotelManagement1.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelManagement1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiControllerBase<Customer>
    {
        private readonly ICustomerRepo _customerRepository;
        public CustomerController(ICustomerRepo customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllCustomer")]
        [LazyCache(5, 10)]
        [EnableRateLimiting("sliding")]
        public async Task<ActionResult<ResponseCore<IEnumerable<CustomerGetDto>>>> GetAll()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAsync(x => true);

            IEnumerable<CustomerGetDto> mappedCustomers = _mapper.Map<IEnumerable<CustomerGetDto>>(customers);

            return Ok(new ResponseCore<IEnumerable<CustomerGetDto>>(mappedCustomers));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdCustomer")]
        public async Task<ActionResult<ResponseCore<CustomerGetDto>>> GetById(Guid id)
        {
            Customer? obj = await _customerRepository.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound(new ResponseCore<Customer?>(false, id + " not found!"));
            }
            CustomerGetDto mappedCustomer = _mapper.Map<CustomerGetDto>(obj);
            return Ok(new ResponseCore<CustomerGetDto?>(mappedCustomer));
        }

        [HttpPut("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "UpdateCustomer")]
        public async Task<ActionResult<ResponseCore<CustomerGetDto>>> Update([FromBody] CustomerUpdateDto customer)
        {
            Customer? mappedCustomer = _mapper.Map<Customer>(customer);
            var validationResult = _validator.Validate(mappedCustomer);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Customer>(false, validationResult.Errors));
            }
            mappedCustomer = await _customerRepository.UpdateAsync(mappedCustomer);
            if (mappedCustomer != null)
                return Ok(new ResponseCore<CustomerGetDto>(_mapper.Map<CustomerGetDto>(mappedCustomer)));
            return BadRequest(new ResponseCore<Customer>(false, customer + " not found"));

        }

        [HttpPost("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "CreateCustomer")]
        public async Task<ActionResult<ResponseCore<CustomerCreateDto>>> Create([FromBody] CustomerCreateDto customer)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(customer);
            var validationResult = _validator.Validate(mappedCustomer);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }
            mappedCustomer = await _customerRepository.CreateAsync(mappedCustomer);
            var res = _mapper.Map<CustomerCreateDto>(mappedCustomer);
            return Ok(new ResponseCore<CustomerCreateDto>(res));

        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeleteCustomer")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _customerRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
