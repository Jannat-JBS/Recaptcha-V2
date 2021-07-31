using Domain.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Business.IAppServices
{
    public interface ISearchNumberPlate
    {
        public CarModel testSearchNumberPlate(string no,string proxy);
    }
}
