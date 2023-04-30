using System.Threading;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks
{
    public interface IRequestHandlerBase<TEntity, TRequest>
    {
        Task<TEntity> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
