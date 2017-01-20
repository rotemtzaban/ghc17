using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class DronesSolver : ISolver<DronesInput, DronesOutput>
    {
		private List<Drone> m_AvailableDrones;
		private List<WorkItem> m_RequestedItems;

		private int CalcNumOfTurnsToCompleteRequest(Drone d, WorkItem item)
		{
			throw new NotImplementedException ();
		}

        public DronesOutput Solve(DronesInput input)
        {
			for (int t = 0; t < input.NumOfTurns; t++) {
				foreach (Drone d in input.Drones)
				{
					d.TurnsUntilAvailable--;
					if (d.TurnsUntilAvailable <= 0) {
						m_AvailableDrones.Add (d);
					}


				}

				foreach (Drone d in m_AvailableDrones) {
					for (int i = 0; i < m_RequestedItems.Count; i++) {

						if (input.MaxWeight - d.WeightLoad > m_RequestedItems [i].Item.Weight) {
							continue;
						}

						int turns = CalcNumOfTurnsToCompleteRequest (d, item);
						d.TurnsUntilAvailable += turns;
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
        }
    }
}
