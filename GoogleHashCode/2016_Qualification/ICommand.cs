namespace _2016_Qualification
{
    public class Unload : ICommand
    {
        public int DroneId { get; }
        public int WarehouseId { get; }
        public int ProductId { get; }
        public int CountOfProducts { get; }
        public int Time { get; set; }

        public Unload(int droneId, int warehouseId, int productId, int countOfProducts, int time)
        {
            DroneId = droneId;
            WarehouseId = warehouseId;
            ProductId = productId;
            CountOfProducts = countOfProducts;
            Time = time;
        }

        public override string ToString()
        {
            return $"{DroneId} U {WarehouseId} {ProductId} {CountOfProducts}";
        }
    }

    public class Deliver : ICommand
    {
        public int DroneId { get; }
        public int OrderId { get; }
        public int ProductId { get; }
        public int CountOfProducts { get; }
        public int Time { get; set; }

        public Deliver(int droneId, int orderId, int productId, int countOfProducts, int time)
        {
            DroneId = droneId;
            OrderId = orderId;
            ProductId = productId;
            CountOfProducts = countOfProducts;
            Time = time;
        }

        public override string ToString()
        {
            return $"{DroneId} D {OrderId} {ProductId} {CountOfProducts}";
        }
    }

    public class Wait : ICommand
    {
        public int DroneId { get; }
        public int NumberOfTurnsToWait { get; }
        public int Time { get; set; }

        public Wait(int droneId, int numberOfTurnsToWait, int time)
        {
            DroneId = droneId;
            NumberOfTurnsToWait = numberOfTurnsToWait;
            Time = time;
        }

        public override string ToString()
        {
            return $"{DroneId} W {NumberOfTurnsToWait}";
        }
    }

    public interface ICommand
    {
        int Time { get; set; }
    }
}