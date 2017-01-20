using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DronesProblem.Commands;

namespace DronesProblem
{
    public class DronesSolver : ISolver<DronesInput, DronesOutput>
	{
		private List<Drone> m_AvailableDrones;
		private List<WorkItem> m_RequestedItems;

		public DronesSolver()
		{
			m_AvailableDrones = new List<Drone> ();
			m_RequestedItems = new List<WorkItem> ();	
		}

		private IEnumerable<CommandBase> GetCommands(Drone d, WorkItem item)
		{
			CommandBase cmdToIssue = null;

			throw new NotImplementedException ();
		}

        public DronesOutput Solve(DronesInput input)
        {
			DronesOutput result = new DronesOutput ();

			// TODO: populate m_RequestedItems according to input

			for (int t = 0; t < input.NumOfTurns; t++) {
				foreach (Drone d in input.Drones)
				{
					d.TurnsUntilAvailable--;
					if (d.TurnsUntilAvailable <= 0) {
						m_AvailableDrones.Add (d);
					}

					var cmd = d.Commands.FirstOrDefault();
					if (cmd != null) {
						cmd.TurnsToComplete--;

						if (cmd.TurnsToComplete <= 0) {
							d.Commands.RemoveAt (0); // remove this "first" cmd
							DeliverCommand deliverCmd = cmd as DeliverCommand;
							if (deliverCmd != null) {
								d.WeightLoad -= deliverCmd.Product.Weight * (uint)deliverCmd.ProductCount;
							}
						}
					
					}
				}

				foreach (Drone d in m_AvailableDrones) {
					for (int i = 0; i < m_RequestedItems.Count; i++) {

						if (input.MaxWeight - d.WeightLoad > m_RequestedItems [i].Item.Weight) {
							continue;
						}

						IEnumerable<CommandBase> cmds = GetCommands(d, m_RequestedItems[i]);
						d.Commands.AddRange (cmds);
						result.Commands.AddRange (cmds);
						foreach (CommandBase cmd in cmds) {
							d.TurnsUntilAvailable += cmd.TurnsToComplete;
						}

						d.WeightLoad += m_RequestedItems [i].Item.Weight;					

						// remove from list
						m_RequestedItems.RemoveAt(i);
						i--; // removed, hack

						if (d.WeightLoad == input.MaxWeight)
						{
							break; // do not add new work to this drone for now
						}
					}
				}
			}

			return result;
        }
    }
}