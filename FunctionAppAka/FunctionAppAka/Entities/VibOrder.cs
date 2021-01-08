using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppAka.Entities
{
    public class VibOrder
    {
        public string BuyerName { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string BillingZipCode { get; set; }

        public decimal OrderAmount { get; set; }
    }
}
