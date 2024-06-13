using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagZoneToXlsConverter
{
    public class DataContainer
    {
        public string[] ChannelNames { get; set; }

        public double[][] Data { get; set; }
    }
}
