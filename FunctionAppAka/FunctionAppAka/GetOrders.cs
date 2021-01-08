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
    public static class GetOrders
    {
        [FunctionName("GetOrders")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var buyerName = req.Query["BuyerName"].ToString();
            var purchaseOrderNumber = req.Query["PurchaseOrderNumber"].ToString();
            var billingZipCode = req.Query["BillingZipCode"].ToString();
            var orderAmount = req.Query["OrderAmount"].ToString();
            var ser = new VibOrderRepository();
            var param = new VibOrder
            {
                BillingZipCode = billingZipCode,
                BuyerName = buyerName,
                PurchaseOrderNumber = purchaseOrderNumber
            };

            if (orderAmount.IsNotNullOrEmpty())
            {
                param.OrderAmount = orderAmount.ToDecimal();
            }

            var orders = ser.GetOrders(param);
            return new OkObjectResult(orders);
        }
    }
}
