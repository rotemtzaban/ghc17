using HashCodeCommon;

namespace _2020_SecondPractice
{
    public class Server : IndexedObject
    {
        public Server(int index) : base(index)
        {
        }

        public int Capacity { get; set; }
        public int Size { get; set; }
        public int Row { get; set; }
        public int SlotInRow { get; set; }
    }
}