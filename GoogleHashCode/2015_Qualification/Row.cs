using System.Collections.Generic;

namespace _2015_Qualification
{
	public class Row
	{
		private readonly int _rowIndex;
		private IList<bool> _isAvailable;
		private int _columns;

		public Row(ProblemInput input, int rowIndex)
		{
			_rowIndex = rowIndex;
			_columns = input.Columns;
			_isAvailable = new List<bool>(input.Columns);
			for (int i = 0; i < input.Rows; i++)
			{
				_isAvailable[i] = true;
			}

			foreach (var slot in input.UnavilableSlots)
			{
				if (slot.Y != rowIndex)
					continue;

				_isAvailable[slot.X] = false;
			}
		}

		public int GetSpace(int size)
		{
			// TODO: optimize this
			for (int i = 0; i < _columns; i++)
			{
				if (!_isAvailable[i])
					continue;

				bool found = true;
				for (int j = i + 1; j < i + size; j++)
				{
					if (!_isAvailable[j])
					{
						i = j;
						found = false;
						break;
					}
				}

				if (found)
					return i;
			}

			return -1;
		}
	}
}