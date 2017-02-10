using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.BaseClasses
{
	public abstract class SolverBase<TInput, TOutput> : ISolver<TInput, TOutput>
	{
		public abstract TOutput Solve(TInput input);
	}
}
