using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightSystemWebAPI.Models
{
    public class WebFlight
    {
        public long flight_id { get; set; }
        public string airlinecompany_name { get; set; }
        public string origincountry_name { get; set; }
        public string destination_country_name { get; set; }
        public string departure_time { get; set; }
        public TerminalState terminal { get; set; }
        public StatusState status { get; set; }

        public WebFlight()
        {
        }


    }

}