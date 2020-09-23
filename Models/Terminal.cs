using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightSystemWebAPI.Models
{
    public class Terminal
    {
        private TerminalState t_state { get; set; }

        public Terminal()
        {
            this.t_state = TerminalState.International;
        }
    }
}