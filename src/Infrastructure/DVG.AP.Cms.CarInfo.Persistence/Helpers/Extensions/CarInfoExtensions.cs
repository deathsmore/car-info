using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Helpers.Extensions
{
    public static class CarInfoExtensions
    {
        public static IQueryable<Domain.Entities.CarInfo> ApplyRuleDisplay(
            this IQueryable<Domain.Entities.CarInfo> source)
        {
            return source.Where(carInfo =>
                carInfo.Status == ActiveStatus.Active &&
                carInfo.Variant.Status == ActiveStatus.Active
            );
        }
        public static IQueryable<Domain.Entities.CarInfo> ApplyRuleDisplayWithModel(
            this IQueryable<Domain.Entities.CarInfo> source)
        {
            return source.Where(carInfo =>
                carInfo.Variant.Status == ActiveStatus.Active &&
                carInfo.Variant.Model.Status == ActiveStatus.Active &&
                carInfo.Status == ActiveStatus.Active
            );
        }

        public static IQueryable<Domain.Entities.CarInfo> ApplyRuleDisplayWithBrand(
            this IQueryable<Domain.Entities.CarInfo> source)
        {
            return source.Where(carInfo =>
                carInfo.Status == ActiveStatus.Active &&
                carInfo.Variant.Status == ActiveStatus.Active &&
                carInfo.Variant.Model.Status == ActiveStatus.Active &&
                carInfo.Variant.Model.Brand.Status == ActiveStatus.Active
            );
        }


    }
}
