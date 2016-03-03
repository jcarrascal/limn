using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBytes.Limn.Charting
{
    public class Series<TX>
    {
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Tuple<TX, double>> Data { get; private set; }

        public Series(IEnumerable<Tuple<TX, double>> data)
        {
            this.Data = data;
        }
    }
}
