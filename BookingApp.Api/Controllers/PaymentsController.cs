using BookingApp.Core.Dtos;
using BookingApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] AddPaymentDto addPaymentDto)
    {
        var result = await _paymentService.AddPaymentAsync(addPaymentDto);
        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayments([FromQuery] string? status)
    {
        var payments = await _paymentService.GetPaymentsAsync(status);
        return Ok(payments);
    }
}
