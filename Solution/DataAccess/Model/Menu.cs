using System.Collections.Generic;

namespace GCM.DataAccess
{
    public class Menu
    {
        public int Id { get; set; }
        public IDictionary<int, int?> DishTypeProduct { get; set; }
        public EnumDayTime DayTime { get; set; }
    }
}
