using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightSystemWebAPI.Models
{
    public class FlightsWebClient
    {
        public int FLIGHT_ID { get; set; }
        public string DEPARTURE_CITY { get; set; }
        public string LANDING_CITY { get; set; }
        public string EST_DEPARTURE_TIME { get; set; }
        public int FLIGHT_TERMINAL { get; set; }
        public string FLIGHT_STATUS { get; set; }

        public FlightsWebClient()
        {

        }
    }
}