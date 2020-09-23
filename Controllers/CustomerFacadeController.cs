using ClassLibrary1;
using ClassLibrary1.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication1.Controllers
{
    [BasicAuthentication]
    public class CustomerFacadeController : ApiController
    {


        
       // private LoginToken<Customer> CustomerLoginToken;
        private Authentications authentication;
       // private LoggedInCustomerFacade CustomerFacade;
        
        public CustomerFacadeController()
        {
            authentication = new Authentications();

        }
        
        public Authentications GetLoginToken()
        {
            Request.Properties.TryGetValue("CustomerUser", out object token);
            Request.Properties.TryGetValue("CustomerFacade", out object facade);
            LoginToken<Customer> customerToken = (LoginToken<Customer>)token;
            LoggedInCustomerFacade customerFacade = (LoggedInCustomerFacade)facade;
            authentication.CustomerToken = customerToken;
            authentication.CustomerFacade = customerFacade;
            return authentication;
        }
        
        [ResponseType(typeof(List<Flight>))]
        [Route("api/customerfacade/GetAllMyFlights")]
        [HttpGet]
        public IHttpActionResult GetAllMyFlights()
        {
            GetLoginToken();

            List<Flight> AllMyFlights = authentication.CustomerFacade.GetAllMyFlights(authentication.CustomerToken).ToList();
            if (AllMyFlights.Count == 0)
                return Content(HttpStatusCode.NotAcceptable, "No flights found!");

            else
                return Ok(AllMyFlights);
        }

        [ResponseType(typeof(List<Customer>))]
        [Route("api/customerfacade/customer/PurchaseTicket")]
        [HttpPost]
        public IHttpActionResult PurchaseTicket(Flight flight)
        {
            GetLoginToken();
            if (flight == null || flight.FLIGHT_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "flight information invalid!");
            try
            {
                authentication.CustomerFacade.PurchaseTicket(authentication.CustomerToken, flight);
                return Ok($"{flight} purchased by {authentication.CustomerToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.Unauthorized, $"error purchasing a new flight");
            }

        }

        [ResponseType(typeof(List<Customer>))]
        [Route("api/customerfacade/customer/CancelTicket")]
        [HttpDelete]
        public IHttpActionResult CancelTicket(Ticket ticket)
        {
            GetLoginToken();
            if (ticket == null || ticket.FLIGHT_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "ticket information invalid!");

            try
            {
                authentication.CustomerFacade.CancelTicket(authentication.CustomerToken, ticket);
                return Ok($"{ticket} canceld by {authentication.CustomerToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"ticket cancelation error");
            }
        }

    }
}
