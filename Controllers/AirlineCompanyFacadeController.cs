using ClassLibrary1;
using ClassLibrary1.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication1.Controllers
{
    [BasicAuthentication]
    public class AirlineCompanyFacadeController : ApiController
    {
        private static List <AirlineCompany> airlineCompanies = new List<AirlineCompany>();

        private LoginToken<AirlineCompany> AirlineLoginToken;
        private Authentications authentication;
        private LoggedInAirlineFacade AirlineFacade;

        
        public AirlineCompanyFacadeController()
        {
            authentication = new Authentications();
           /* airlineCompanies.Add(new AirlineCompany { AIRLINE_ID = 6, AIRLINE_NAME = "Arkia", COUNTRY_CODE = 2, USERNAME = "arkia", PASSWORD = "arkia" });
            airlineCompanies.Add(new AirlineCompany { AIRLINE_ID = 7, AIRLINE_NAME = "El Al", COUNTRY_CODE = 2, USERNAME = "elal", PASSWORD = "elal" });
            airlineCompanies.Add(new AirlineCompany { AIRLINE_ID = 8, AIRLINE_NAME = "Turkish", COUNTRY_CODE = 5, USERNAME = "turkish", PASSWORD = "turkish" });
            */
        }


        public Authentications GetLoginToken()
        {
            Request.Properties.TryGetValue("AirlineUser", out object token);
            Request.Properties.TryGetValue("AirlineFacade", out object facade);
            LoginToken<AirlineCompany> airlineToken = (LoginToken<AirlineCompany>)token;
            LoggedInAirlineFacade airlineFacade = (LoggedInAirlineFacade)facade;
            authentication.AirlineToken = airlineToken;
            authentication.AirlineFacade = airlineFacade;
            return authentication;
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/GetAllTickets")]
        [HttpGet]
        public IHttpActionResult GetAllTickets()
        {
            GetLoginToken();

            List<Ticket> AllTickets = authentication.AirlineFacade.GetAllTickets(AirlineLoginToken).ToList();
            if (AllTickets.Count == 0)
                return Content(HttpStatusCode.NotAcceptable, "No Tickets Found!");

            else
                return Ok(AllTickets);
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/GetAllAirlines")]
        [HttpGet]
        public IHttpActionResult GetAllAirlines()
        {
            GetLoginToken();

            List<AirlineCompany> AllAirlines = authentication.AirlineFacade.GetAllAirlineCompanies().ToList();
            if (AllAirlines.Count == 0 || AllAirlines == null)
                return Content(HttpStatusCode.NotAcceptable, "No Airlines Found!");

            else
                return Ok(AllAirlines);
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/GetAllFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            GetLoginToken();

            List<Flight> AllFlights = authentication.AirlineFacade.GetAllFlights(authentication.AirlineToken).ToList();
            if (AllFlights.Count == 0)
                return Content(HttpStatusCode.NotAcceptable, "No flights found!");

            else
                return Ok(AllFlights);
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/airline/CancelFlight")]
        [HttpDelete]
        public IHttpActionResult CancelFlight(Flight flight)
        {
            GetLoginToken();
            if (flight == null || flight.FLIGHT_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "flight information invalid!");

            try
            {
                authentication.AirlineFacade.CancelFlight(authentication.AirlineToken, flight);
                return Ok($"{flight} deleted by {authentication.AirlineToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"airline cancelation error");
            }
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/airline/CreateFlight")]
        [HttpPost]
        public IHttpActionResult CreateFlight(Flight flight)
        {
            GetLoginToken();
            if (flight == null || flight.FLIGHT_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "flight information invalid!");
            try
            {
                authentication.AirlineFacade.CreateFlight(authentication.AirlineToken, flight);
                return Ok($"{flight} Added by {authentication.AirlineToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.Unauthorized, $"error creating a new flight");
            }


        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/airline/UpdateFlight")]
        [HttpPost]
        public IHttpActionResult UpdateFlight(Flight flight)
        {
            GetLoginToken();
            if (flight == null || flight.FLIGHT_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "flight information invalid!");

            try
            {
                authentication.AirlineFacade.UpdateFlight(authentication.AirlineToken, flight);
                return Ok($"{flight} Added by {authentication.AirlineToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"flight update error");
            }
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/airline/ChangePassword")]
        [HttpPost]
        public IHttpActionResult ChangeMyPassword(string oldPassword, string newPassword)

        {
            GetLoginToken();
            if (oldPassword == null)
                return Content(HttpStatusCode.NotAcceptable, "old password is invalid!");
            if (newPassword == null)
                return Content(HttpStatusCode.NotAcceptable, "new password is invalid!");
            try
            {
                if (authentication.AirlineToken.User.PASSWORD == oldPassword)
                authentication.AirlineToken.User.PASSWORD = newPassword;
                authentication.AirlineFacade.ChangeMyPassword(authentication.AirlineToken, oldPassword,newPassword);
                return Ok($"password changed successfully by {authentication.AirlineToken.User}!");
            }
            catch (Exception)
            {
               return Content(HttpStatusCode.NotAcceptable, $"password changed unsuccessfully!");
            }
        }

       
        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/airlinecompanyfacade/airline/ModifyAirline")]
        [HttpPost]
        public IHttpActionResult MofidyAirlineDetails(AirlineCompany airline)
        {
            GetLoginToken();
            if (airline == null || airline.AIRLINE_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "airline information invalid!");

            try
            {
                authentication.AirlineFacade.MofidyAirlineDetails(authentication.AirlineToken, airline);
                return Ok($"{airline} modified by {authentication.AirlineToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"airline modification error");
            }
        }

    }
}
