using HashCodeCommon;
using HashCodeCommon.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');
            input.Rows = long.Parse(firstLineSplited[0]);
            input.Columns = long.Parse(firstLineSplited[1]);
            input.NumberOfDrones = long.Parse(firstLineSplited[2]);
            input.NumberOfIterations = long.Parse(firstLineSplited[3]);
            input.MaxDrownLoad = long.Parse(firstLineSplited[4]);

            input.NumOfProducts = Convert.ToInt32(reader.ReadLine());

            var thirdLineSplitted = reader.ReadLine().Split(' ');
            input.Products = new List<Products>();
            for (int i = 0; i < input.NumOfProducts; i++)
                input.Products.Add(new Products(long.Parse(thirdLineSplitted[i]), i));


            input.NumOfWarehouses = Convert.ToInt32(reader.ReadLine());
            input.Warehouses = new List<Warehouse>();
            for (int i = 0; i < input.NumOfWarehouses; i++)
            {
                var location = reader.ReadLine().Split(' ');
                var itemsPerProduct = reader.GetIntList().ToArray();

                input.Warehouses.Add(new Warehouse(Convert.ToInt32(location[1]), Convert.ToInt32(location[0]), itemsPerProduct, i));
            }

            input.NumOfOrders = Convert.ToInt32(reader.ReadLine());
            input.Orders = new List<Order>();
            for (int i = 0; i < input.NumOfOrders; i++)
            {
                var location = reader.GetIntList().ToArray();
                var numOfItems = Convert.ToInt32(reader.ReadLine());
                var itemsIds = reader.GetIntList().GroupBy(x => x).ToDictionary(v => v.Key, v => v.Count());

                input.Orders.Add(new Order(location[1], location[0], itemsIds, i));
            }







            return input;

        }
    }
}
