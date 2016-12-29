using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Table
    {
        public string leagueCaption { get; set; }
        public int matchday { get; set; }
        public List<Club> standing { get; set; }
    }
}
