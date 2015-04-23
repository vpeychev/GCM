using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GCM.DataAccess;

namespace GCM.BL.Core
{
    public class BusinessProcess : IBusinessProcess
    {
        private DataAccess.DataContext _Context;
        private Tuple<EnumDayTime, string> _InputValue;

        public BusinessProcess(string inputValue)
        {
            this.InInitializer(inputValue);
        }

        #region Methods: Public

        /// <summary>
        /// Generate output data 
        /// </summary>
        /// <returns></returns>
        public string CreateOutput()
        {
            Menu requestedMenu = this._Context.Menus
                .Where(k => k.DayTime == this._InputValue.Item1)
                .FirstOrDefault();
            var loadedData = requestedMenu.DishTypeProduct
                .Join(this._Context.DishTypes, m => m.Key, d => d.Id, (m, d) => new { ProductId = m.Value, DishTypeId = d.Id, DishTypeName = d.Name })
                .Join(this._Context.Products, b => b.ProductId, p => p.Id, (b, p) => new { b.DishTypeId, b.DishTypeName, b.ProductId, ProductName = p.Name })
                .GroupJoin(this._Context.MultiProductOrders
                        .Where(k => k.DayTime == this._InputValue.Item1)
                        .ToList()
                    , p => p.ProductId
                    , c => c.ProductId
                    , (p, g) => g
                    .Select(c => new { DishTypeId = p.DishTypeId, DishTypeName = p.DishTypeName, ProductId = p.ProductId, ProductName = p.ProductName, IsMultiple = true })
                    .DefaultIfEmpty(new { DishTypeId = p.DishTypeId, DishTypeName = p.DishTypeName, ProductId = p.ProductId, ProductName = p.ProductName, IsMultiple = false }))
                .SelectMany(g => g);
            var requestedDishTypes = this._InputValue.Item2.Split(',')
                .GroupBy(k => k)
                .Select(k => new { DishTypeId = int.Parse(k.Key), Count = k.Count() });
            var dataToProcess = requestedDishTypes
                .GroupJoin(loadedData, p => p.DishTypeId, c => c.DishTypeId, (p, g) => g
                    .Select(c => new { p.Count, DishTypeId = c.DishTypeId, DishTypeName = c.DishTypeName, ProductId = c.ProductId, ProductName = c.ProductName, IsMultiple = c.IsMultiple, HasError = !c.IsMultiple ? p.Count > 1 : false })
                    .DefaultIfEmpty(new { p.Count, DishTypeId = -1, DishTypeName = string.Empty, ProductId = (int?)-1, ProductName = string.Empty, IsMultiple = false, HasError = true })
                )
                .SelectMany(g => g);

            StringBuilder sb = new StringBuilder();
            foreach (var a in dataToProcess)
            {
                if (a.HasError)
                {
                    sb.AppendFormat(", {0}error", (string.IsNullOrEmpty(a.ProductName) ? string.Empty : string.Format("{0}, ", a.ProductName)));
                    break;
                }
                sb.AppendFormat(", {0}{1}", a.ProductName, (a.IsMultiple && a.Count > 1 ? string.Format("(x{0})", a.Count) : string.Empty));
            }
            string result = sb.ToString().Trim(new char[] { ',', ' ' }).ToLower();

            return result;
        }

        #endregion                      //Methods: Public

        #region Methods: Private

        /// <summary>
        /// Separate time of day and list of dishes
        /// </summary>
        /// <param name="inputValue"></param>
        private void ParseInput(string inputValue)
        {
            IList<string> tmp = inputValue.Split(',').ToList();
            EnumDayTime dayTime = (EnumDayTime)Enum.Parse(typeof(EnumDayTime), tmp[0], true);
            tmp.RemoveAt(0);
            this._InputValue = new Tuple<EnumDayTime, string>(dayTime, string.Join(",", tmp.OrderBy(k => k)));
        }

        #region Helper

        /// <summary>
        /// Class initializer
        /// </summary>
        /// <param name="inputValue"></param>
        private void InInitializer(string inputValue)
        {
            this._Context = new DataAccess.DataContext();
            this.ParseInput(inputValue);
        }

        #endregion                          //Helper

        #endregion                      //Methods: Private
    }

}
