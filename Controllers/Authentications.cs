using ClassLibrary1;
using ClassLibrary1.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    public class Authentications
    {
        public LoginToken<Administrator> AdminToken { get; set; }
        public LoggedInAdministratorFacade AdminFacade { get; set; }
        public LoginToken<AirlineCompany> AirlineToken { get; set; }
        public LoggedInAirlineFacade AirlineFacade { get; set; }
        public LoginToken<Customer> CustomerToken { get; set; }
        public LoggedInCustomerFacade CustomerFacade { get; set; }


        public Authentications()
        {
             AdminToken = new LoginToken<Administrator>();
             AdminFacade = new LoggedInAdministratorFacade();
             AirlineToken = new LoginToken<AirlineCompany>();
             AirlineFacade = new LoggedInAirlineFacade();
             CustomerToken = new LoginToken<Customer>();
             CustomerFacade = new LoggedInCustomerFacade();
        }


    }
}