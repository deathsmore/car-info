using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Constant
{
    public static class StaticVariables
    {
        private static AMPSetting? _ampSetting;
        public static AMPSetting? AMPSettings

        {
            get { return _ampSetting ?? new AMPSetting(); }
            set { _ampSetting = value; }
        }
    }

    public class AMPSetting
    {
        public string? StorageDomain { get; set; }
        public string? PublishDomain { get; set; }
    }
}
