using FunctionAppAka.Datas;
using FunctionAppAka.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FunctionAppAka.Services
{
    public class VibOrderRepository
    {

        IDbContext _db;

        public VibOrderRepository()
        {
            var config = new ConfigurationBuilder()
                       .SetBasePath(Environment.CurrentDirectory)
                       .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile("host.json", optional: true, reloadOnChange: true)
                       .AddEnvironmentVariables()
                       .Build();

            var conn = config["ConnectionStrings:DefaultConnectionString"];

            var manager = new ConnectionManager(conn);
            _db = new DbContext(manager);
        }



        public IEnumerable<VibOrder> GetOrders(VibOrder order)
        {
            var sql = "select * from viborder where 1=1 ";


            if (order.PurchaseOrderNumber.IsNotNullOrEmpty())
                sql += "and PurchaseOrderNumber = @PurchaseOrderNumber ";

            if (order.BillingZipCode.IsNotNullOrEmpty())
                sql += "and BillingZipCode = @BillingZipCode ";

            if (order.BuyerName.IsNotNullOrEmpty())
                sql += "and BuyerName = @BuyerName ";

            if (order.OrderAmount != 0)
                sql += "and OrderAmount = @OrderAmount ";

            return _db.Query<VibOrder>(sql, order);
        }

        public bool ExistsPurchaseOrderNumber(string purchaseOrderNumber)
        {
            var order = GetOrders(new VibOrder { PurchaseOrderNumber = purchaseOrderNumber });
            return order.ToList().Count > 0;
        }

        public bool ExistsBillingZipCode(string billingZipCode)
        {
            var order = GetOrders(new VibOrder { BillingZipCode = billingZipCode });
            return order == null;
        }

        public void CreateOrder(VibOrder order)
        {
            var sql = @"insert into VibOrder values(
	                       @PurchaseOrderNumber,
	                       @BillingZipCode,
	                       @BuyerName,
	                       @OrderAmount
                        );";

            _db.Execute(sql, order);
        }
    }
}
