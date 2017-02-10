namespace _2015_Qualification
{
	public class ServerAllocation
	{
		public override string ToString()
		{
			return string.Format("S{0}: R{1};C{2} ({3}) - P{4}", Server.Index, Row, InitialColumn, Server.Slots, Pool.Index);
		}

		public Server Server { get; set; }
		public Pool Pool { get; set; }
		public int Row { get; set; }
		public int InitialColumn { get; set; }
	}
}