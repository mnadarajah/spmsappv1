using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.Services.Repository
{
    public class PaymentTypeService : BaseBusinessService<PaymentTypeModel>, IPaymentTypeService
    {
        public PaymentTypeService()
        {
            _route = "PaymentTypes";
        }
    }
}
