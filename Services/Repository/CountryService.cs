using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SPMSCAV1.Services.Repository
{
    public class CountryService : BaseBusinessService<CountryModel>, ICountryService
    {
        public CountryService()
        {
            _route = "Countries";
        }
    }
}
