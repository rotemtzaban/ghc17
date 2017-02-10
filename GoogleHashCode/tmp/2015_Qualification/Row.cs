using System.Collections.Generic;
using System.Text;

namespace _2015_Qualification
{
	public class Row
	{
		public  readonly int _rowIndex;
		private bool[] _isAvailable;
		private int _columns;

		public Row(ProblemInput input, int rowIndex)
		{
			_rowIndex = rowIndex;
			_columns = input.Columns;
			_isAvailable = new bool[input.Columns];
			for (int i = 0; i < input.Columns; i++)
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

        public int GetAndAcquireSlot(int size)
        {
            int index = GetSpace(size);
			if(index != -1)
				AcquireSlots(index, size);
            return index;
        }

        public void AcquireSlots(int index, int size)
        {
            for (int i = 0; i < size; i++)
            {
                _isAvailable[index + i] = false;
            }
        }

		public int GetSpace(int size)
		{
			// TODO: optimize this
			for (int i = 0; i < _columns; i++)
			{
				if (i + size > _isAvailable.Length)
					break;
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

		public override string ToString()
		{
			var builder = new StringBuilder();
			for (int i = 0; i < _columns; i++)
				builder.Append(_isAvailable[i] ? "." : "x");

			return builder.ToString();
		}
	}
}