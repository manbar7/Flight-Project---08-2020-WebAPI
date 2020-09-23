using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightSystemWebAPI.Models
{
    public class Status
    {
        private StatusState s_state { get; set; }

        public Status()
        {
            this.s_state = StatusState.On_Time;
        }
    }
}