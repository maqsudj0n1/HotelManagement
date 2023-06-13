using Application.DTOs.Payment;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entities;
using HotelManagement1.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ApiControllerBase<Payment>
    {
        private readonly IPaymentRepo _paymentRepository;
        public PaymentController(IPaymentRepo paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetAllPayment")]
        public async Task<ActionResult<ResponseCore<IEnumerable<PaymentGetDto>>>> GetAll()
        {
            IEnumerable<Payment> Payments = await _paymentRepository.GetAsync(x => true);

            IEnumerable<PaymentGetDto> mappedPayments = _mapper.Map<IEnumerable<PaymentGetDto>>(Payments);

            return Ok(new ResponseCore<IEnumerable<PaymentGetDto>>(mappedPayments));
        }

        [HttpGet("[action]")]
        //[Authorize(Roles = "GetByIdPayment")]
        public async Task<ActionResult<ResponseCore<PaymentGetDto>>> GetById(Guid id)
        {
            Payment? obj = await _paymentRepository.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound(new ResponseCore<Payment?>(false, id + " not found!"));
            }
            PaymentGetDto mappedPayment = _mapper.Map<PaymentGetDto>(obj);
            return Ok(new ResponseCore<PaymentGetDto?>(mappedPayment));
        }

        [HttpPut("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "UpdatePayment")]
        public async Task<ActionResult<ResponseCore<PaymentGetDto>>> Update([FromBody] PaymentUpdateDto Payment)
        {
            Payment? mappedPayment = _mapper.Map<Payment>(Payment);
            var validationResult = _validator.Validate(mappedPayment);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<Payment>(false, validationResult.Errors));
            }
            mappedPayment = await _paymentRepository.UpdateAsync(mappedPayment);
            if (mappedPayment != null)
                return Ok(new ResponseCore<PaymentGetDto>(_mapper.Map<PaymentGetDto>(mappedPayment)));
            return BadRequest(new ResponseCore<Payment>(false, Payment + " not found"));

        }

        [HttpPost("[action]")]
        [ActionModelValidation]
        //[Authorize(Roles = "CreatePayment")]
        public async Task<ActionResult<ResponseCore<PaymentCreateDto>>> Create([FromBody] PaymentCreateDto Payment)
        {
            Payment mappedPayment = _mapper.Map<Payment>(Payment);
            var validationResult = _validator.Validate(mappedPayment);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
            }
            mappedPayment = await _paymentRepository.CreateAsync(mappedPayment);
            var res = _mapper.Map<PaymentCreateDto>(mappedPayment);
            return Ok(new ResponseCore<PaymentCreateDto>(res));

        }

        [HttpDelete("[action]")]
        //[Authorize(Roles = "DeletePayment")]
        public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
        {
            return await _paymentRepository.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

        }
    }
}
