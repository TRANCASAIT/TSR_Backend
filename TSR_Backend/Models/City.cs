namespace TSR_Backend.Models
{
    public class City
    {
        public string CityName { get; set; }
        public int StateId { get; set; }

        public class CityUpdate: City
        {
            public int CityId { get; set; }
        }

        public class CityGet: CityUpdate
        {
            public string StateName { get; set; }
            public int State { get; set; }
            public string? Message { get; set; }
        }
    }
}
