using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lfglnet
{
    public class AttendanceSet
    {
        public class TimeInfo
        {
            public int id { get; set; }
            public int cid { get; set; }
            //上下班时间
            public int work_s { get; set; }
            public int work_e { get; set; }
            //倍率
            public float rate { get; set; }
            //名称
            public string t_name { get; set; }
            public int t_type { get; set; }
            //


        }

        public class ClassesInfo
        {
            public int id { get; set; }
            public string t_name { get; set; }
            //有效范围
            public int valid_s { get; set; }
            public int valid_e { get; set; }
            //上下班时间
            public int work_s { get; set; }
            public int work_e { get; set; }
            //时间段范围
            public int time_s { get; set; }
            public int time_e { get; set; }
            public TimeInfo[] timeinfo;
        }
    }
}
