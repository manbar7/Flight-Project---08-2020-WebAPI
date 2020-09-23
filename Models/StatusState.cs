using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightSystemWebAPI.Models
{
    public enum StatusState         
    {
        Unavailable = 0,
        On_Time = 1,
        Delayed = 2,
        Landed = 3,
        Final = 4,
        Not_Final = 5,
        Cancelled = 9       
    }
    
}