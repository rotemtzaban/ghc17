namespace _2016_Qualification
{
    public class Load : ICommand
    {
        public int DroneId { get; }
        public int WarehouseId { get; }
        public int ProductId { get; }
        public int CountOfProducts { get; }

        public Load(int droneId, int warehouseId, int productId, int countOfProducts)
        {
            DroneId = droneId;
            WarehouseId = warehouseId;
            ProductId = productId;
            CountOfProducts = countOfProducts;
        }

        public override string ToString()
        {
            return $"{DroneId} L {WarehouseId} {ProductId} {CountOfProducts}";
        }
    }

    public class Unload : ICommand
    {
        public int DroneId { get; }
        public int WarehouseId { get; }
        public int ProductId { get; }
        public int CountOfProducts { get; }

        public Unload(int droneId, int warehouseId, int productId, int countOfProducts)
        {
            DroneId = droneId;
            WarehouseId = warehouseId;
            ProductId = productId;
            CountOfProducts = countOfProducts;
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

        public Deliver(int droneId, int orderId, int productId, int countOfProducts)
        {
            DroneId = droneId;
            OrderId = orderId;
            ProductId = productId;
            CountOfProducts = countOfProducts;
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

        public Wait(int droneId, int numberOfTurnsToWait)
        {
            DroneId = droneId;
            NumberOfTurnsToWait = numberOfTurnsToWait;
        }

        public override string ToString()
        {
            return $"{DroneId} W {NumberOfTurnsToWait}";
        }
    }

    public interface ICommand
    {
    }
}