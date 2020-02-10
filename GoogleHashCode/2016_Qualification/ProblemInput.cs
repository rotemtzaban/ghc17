using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;

namespace _2016_Qualification
{
    public class ProblemInput
    {
        public long Rows { get; set; }
        public long Columns { get; set; }
        public long NumberOfDrones { get; set; }
        public long NumberOfIterations { get; set; }
        public long MaxDrownLoad { get; set; }

        public List<Products> Products { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        public List<Order> Orders { get; set; }

    }

    public class Order: IndexedObject
    {
        public Order(int index) : base(index)
        {
        }
    }

    public class Warehouse : IndexedObject
    {
        public long ColumnPosition { get; }
        public long RowPosition { get; }
        public int[] NumberOfItemsForProduct { get; }

        public Warehouse(long columnPosition, long rowPosition,int[] numberOfItemsForProduct, int index) : base(index)
        {
            ColumnPosition = columnPosition;
            RowPosition = rowPosition;
            NumberOfItemsForProduct = numberOfItemsForProduct;
        }
    }

    public class Products : IndexedObject
    {
        public long Weight { get; set; }
        public Products(long weight, int index) : base(index)
        {
            Weight = weight;
        }
    }
}
