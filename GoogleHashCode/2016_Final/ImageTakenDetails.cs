namespace _2016_Final
{
    public class ImageTakenDetails
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public long TurnTaken { get; set; }
        public long SatelliteId { get; set; }

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + Latitude.GetHashCode();
            hash = hash * 31 + Longitude.GetHashCode();
            return hash;
        }
    }
}