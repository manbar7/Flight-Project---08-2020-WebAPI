using ClassLibrary1;
using FlightSystemWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemWebAPI
{
    
    public interface IWebClientAdapter
    {
        IList<WebFlight> ReturnDepartures(List<Flight> flights);
        
        IList<WebFlight> ReturnLandings(List<Flight> flights);
    }
    
}
