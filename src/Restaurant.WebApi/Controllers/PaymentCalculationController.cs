using Application.Dtos.PaymentCalculation.Responses;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebApi.Controllers;

[ApiController]
[Route("PaymentCalculation")]
[Authorize(Roles = UserRoles.Admin)]
public class PaymentCalculationController(IPaymentCalculationService calculationService)
    : ControllerBase
{
    [HttpGet]
    public async Task<List<PaymentCalculationResponseModel>> Calculate(int orderId)
    {
        return await calculationService.PaymentCalculation(orderId);
    }
}