using DVG.AP.Cms.CarInfo.GrpcPersistence.Infrastructures.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.GrpcPersistence.Config
{
    public static class GrpcPersistenceSetting
    {
        private static GrpcSetting? _grpcSetting;

        public static GrpcSetting GrpcSetting
        {
            get { return _grpcSetting ?? new GrpcSetting(); }
            set { _grpcSetting = value; }
        }
    }
    public class GrpcSetting
    {
        private short _deadLineTimeInSecond;

        public short DeadLineTimeInSecond
        {
            get
            {
                return _deadLineTimeInSecond == 0
                    ? GrpcPersistenceConst.DefaultDeadLineTimeInSecond
                    : _deadLineTimeInSecond;
            }
            set { _deadLineTimeInSecond = value; }
        }

        public UrlsConfig Urls { get; set; }

        public GrpcSetting()
        {
            Urls = new UrlsConfig();
            DeadLineTimeInSecond = GrpcPersistenceConst.DefaultDeadLineTimeInSecond;
        }
    }
}
