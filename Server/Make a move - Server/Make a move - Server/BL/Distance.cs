using Newtonsoft.Json;

namespace Make_a_move___Server.BL
{
    public class Distance
    {
        [JsonProperty("_id")]
        public int Id { get; set; }

        [JsonProperty("קוד מוצא")]
        public int OriginCode { get; set; }

        [JsonProperty("קוד יעד")]
        public int DestinationCode { get; set; }

        [JsonProperty("מרחק ממרכז למרכז")]
        public double DistanceFromCenter { get; set; }

        [JsonProperty("רדיוס מוצא")]
        public double OriginRadius { get; set; }

        [JsonProperty("רדיוס יעד")]
        public double DestinationRadius { get; set; }
    }

    public class ApiResponse
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public List<Distance> records { get; set; }
    }
}
