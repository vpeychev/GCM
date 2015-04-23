using System.Collections.Generic;

namespace GCM.DataAccess
{

    public class DataContext //: IContext
    {
        public IList<DishType> DishTypes { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Menu> Menus { get; set; }
        public IList<MultiProductOrder> MultiProductOrders { get; set; }

        public DataContext()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.DishTypes = new List<DishType> {
                new DishType { Id = 1, Name = "Entrée" },
                new DishType { Id = 2, Name = "Side" },
                new DishType { Id = 3, Name = "Drink" },
                new DishType { Id = 4, Name = "Dessert" }
            };

            this.Products = new List<Product> {
                new Product { Id = 1, Name = "Eggs"},
                new Product { Id = 2, Name = "Toast"},
                new Product { Id = 3, Name = "Coffee"},
                new Product { Id = 4, Name = "Steak"},
                new Product { Id = 5, Name = "Potato"},
                new Product { Id = 6, Name = "Wine"},
                new Product { Id = 7, Name = "Cake"}
            };

            this.Menus = new List<Menu> {
                new Menu { Id = 1, DayTime = EnumDayTime.Morning, DishTypeProduct = new Dictionary<int, int?> { [1] = 1, [2] = 2, [3] = 3, [4] = null } },
                new Menu { Id = 1, DayTime = EnumDayTime.Night, DishTypeProduct = new Dictionary<int, int?> { [1] = 4, [2] = 5, [3] = 6, [4] = 7 } }
            };

            this.MultiProductOrders = new List<MultiProductOrder> {
                new MultiProductOrder { Id = 1, DayTime = EnumDayTime.Morning, ProductId = 3 },
                new MultiProductOrder { Id = 2, DayTime = EnumDayTime.Night, ProductId = 5 }
            };
        }

    }
}
