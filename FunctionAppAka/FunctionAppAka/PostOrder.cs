using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionAppAka.Entities;
using FunctionAppAka.Services;

namespace FunctionAppAka
{
    public static class PostOrder
    {
        [FunctionName("PostOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var buyerName = req.Form["BuyerName"].ToString();
            if (buyerName == "")
            {
                return new BadRequestObjectResult("BuyerName is missing ");
            }

            var purchaseOrderNumber = req.Form["PurchaseOrderNumber"].ToString();
            if (purchaseOrderNumber == "")
            {
                return new BadRequestObjectResult("PurchaseOrderNumber is missing ");
            }

            var billingZipCode = req.Form["BillingZipCode"].ToString();
            if (billingZipCode == "")
            {
                return new BadRequestObjectResult("BillingZipCode is missing ");
            }

            var orderAmount = req.Form["OrderAmount"].ToString();
            if (orderAmount == "")
            {
                return new BadRequestObjectResult("OrderAmount is missing ");
            }

            var ser = new VibOrderRepository();

            if (ser.ExistsPurchaseOrderNumber(purchaseOrderNumber))
            {
                return new ObjectResult($"duplicate purchaseOrderNumber: {purchaseOrderNumber}")
                {
                    StatusCode = StatusCodes.Status204NoContent
                };
            }


            ser.CreateOrder(new VibOrder
            {
                BillingZipCode = billingZipCode,
                BuyerName = buyerName,
                PurchaseOrderNumber = purchaseOrderNumber,
                OrderAmount = orderAmount.ToDecimal(),
            });

            return await Task.FromResult(new ObjectResult("Add Success")
            {
                StatusCode = StatusCodes.Status201Created
            });
        }
    }
}
