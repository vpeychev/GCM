using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCM.DataAccess
{
    public class MultiProductOrder
    {
        public int Id { get; set; }
        public EnumDayTime DayTime { get; set; }
        public int ProductId { get; set; }
    }
}
