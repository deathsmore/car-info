using DVG.AP.Cms.CarInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces
{
    public interface INewCarVariantService
    {
        Task CalPrice(NewCarVariant entity);
    }
}
