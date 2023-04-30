using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Dtos.Common
{
    public class UserFilterParam
    {
        public int[]? UserIds { get; set; }
        public bool IsPaging { get; set; }
    }
}
