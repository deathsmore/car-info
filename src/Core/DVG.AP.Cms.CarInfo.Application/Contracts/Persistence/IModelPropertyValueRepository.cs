using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface IModelPropertyValueRepository : IRepository<Domain.Entities.ModelPropertyValue>
    {
        Task<IReadOnlyList<Domain.Entities.ModelPropertyValue>> ListAsync(int modelId);
    }
}
