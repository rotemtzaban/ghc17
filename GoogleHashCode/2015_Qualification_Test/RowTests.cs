using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2015_Qualification;
using _2015_Qualification_Test.Properties;

namespace _2015_Qualification_Test
{
	[TestClass]
	public class RowTests
	{
		[TestMethod]
		public void MyTestMethod()
		{
			var parser = new Parser();
			var input = parser.ParseFromData(Resources.RealInput);
			var row = new Row(input, 2);

			row.GetAndAcquireSlot(2);
			row.GetAndAcquireSlot(2);

			
		}
	}
}
