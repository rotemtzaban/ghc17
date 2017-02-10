using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DronesProblem.Commands;

namespace DronesProblem
{
    public class DronesSolver : SolverBase<DronesInput, DronesOutput>
	{
		private List<Drone> m_AvailableDrones;
		private List<WorkItem> m_RequestedItems;

		public DronesSolver()
		{
			m_AvailableDrones = new List<Drone> ();
			m_RequestedItems = new List<WorkItem> ();	
		}

		private IEnumerable<CommandBase> GetCommands(Drone d, WorkItem item, DronesInput input)
		{
			// For now, all will be load deliver heuristic.
			List<CommandBase> result = new List<CommandBase>();

			foreach (Warehouse w in input.WareHouses) {
				int itemCount;
				if (w.Products.TryGetValue (item.Item, out itemCount) && itemCount > 0) {
					w.Products[item.Item]=itemCount-1;
					LoadCommand loadCmd = new LoadCommand (d, w, item.Item, /*productCount=*/ 1);
					result.Add (loadCmd);
					DeliverCommand deliverCommand = new DeliverCommand (d, item.ParentOrder, item.Item, /*productCount=*/1);
					result.Add (deliverCommand);

					break;
				}								
			}		

			return result;
		}

        protected override DronesOutput Solve(DronesInput input)
        {
			DronesOutput result = new DronesOutput ();

			// TODO: populate m_RequestedItems according to input
			foreach (Order order in input.Orders) {				
				foreach (Product product in order.WantedProducts) {
					WorkItem workItem = new WorkItem (product, order.Location, order);
					m_RequestedItems.Add (workItem);
				}
			}

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

				HashSet<Drone> dronesToRemove = new HashSet<Drone> ();

				foreach (Drone d in m_AvailableDrones) {
					for (int i = 0; i < m_RequestedItems.Count; i++) {

						if (input.MaxWeight - d.WeightLoad < m_RequestedItems [i].Item.Weight) {
							continue;
						}

						IEnumerable<CommandBase> cmds = GetCommands(d, m_RequestedItems[i], input);
						d.Commands.AddRange (cmds);
						if (cmds.Any ()) {
							dronesToRemove.Add (d);
						}
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

				foreach (Drone d in dronesToRemove) {
					m_AvailableDrones.Remove (d);
				}
			}


			return result;
        }
    }
}