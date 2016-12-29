using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class AllFixtures
    {
        public string timeFrameStart { get; set; }
        public string timeFrameEnd { get; set; }
        public int count { get; set; }
        public List<Fixture> fixtures { get; set; }
    }
}
