using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
	public class PizzaSlice
	{
		public Coordinate TopLeft { get; set; }
		public Coordinate BottomRight { get; set; }

		public int Size
		{
			get { return (BottomRight.X - TopLeft.X + 1) + (BottomRight.Y - TopLeft.Y + 1); }
		}
	}
}
