using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using Xunit;
using FunctionAppAka;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using FunctionAppAka.Entities;

namespace FunctionAppAka.Test
{
    public class UnitTest1
    {

        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void Create_Order_Success_Test()
        {
            var request = TestFactory.CreateHttpRequestForms(new Dictionary<string, StringValues> {
                { "BuyerName","test user 04"},
                { "PurchaseOrderNumber", $"aka{DateTime.Now.ToString("yyyymmddhhmmss")}"},
                { "BillingZipCode", "40001"},
                { "OrderAmount","49.09"}
            });

            var response = (ObjectResult)await PostOrder.Run(request, logger);

            Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
            Assert.Equal("Add Success", response.Value);
        }

        [Fact]
        public async void Order_fild_BillingZipCode_missing_Test()
        {
            var request = TestFactory.CreateHttpRequestForms(new Dictionary<string, StringValues> {
                { "BuyerName","test user 04"},
                { "PurchaseOrderNumber", "test user po 04"},
                { "OrderAmount","49.09"}
            });

            var response = (ObjectResult)await PostOrder.Run(request, logger);

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.Equal("BillingZipCode is missing ", response.Value);
        }

        [Fact]
        public async void Order_duplicate_PurchaseOrderNumber_Test()
        {
            var request = TestFactory.CreateHttpRequestForms(new Dictionary<string, StringValues> {
                { "BuyerName","test user 04"},
                { "PurchaseOrderNumber", "test user aka 01"},
                { "BillingZipCode", "40002"},
                { "OrderAmount","49.09"}
            });

            var response = (ObjectResult)await PostOrder.Run(request, logger);

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }


        [Fact]
        public async void get_order_list_Test()
        {
            var request = TestFactory.CreateHttpRequestQuerys(new Dictionary<string, StringValues> {
                { "BuyerName","aka"}
            });

            var response = (ObjectResult)await GetOrders.Run(request, logger);
            var orders = ((List<VibOrder>)response.Value);
            Assert.Equal("aka", orders[0].BuyerName);
        }
    }
}
