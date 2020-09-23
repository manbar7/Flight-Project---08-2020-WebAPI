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
    //api/administratorfacade
    [BasicAuthentication]
    public class AdministratorFacadeController : ApiController
    {
       
        private LoginToken<Administrator> AdminLoginToken;
        private Authentications authentication;

        public AdministratorFacadeController()
        {
            authentication = new Authentications();
        }


        public Authentications GetLoginToken()
        {
           
            Request.Properties.TryGetValue("AdminUser", out object token);
            Request.Properties.TryGetValue("AdminFacade", out object facade);
            LoginToken<Administrator> adminToken = (LoginToken<Administrator>)token;
            LoggedInAdministratorFacade adminFacade = (LoggedInAdministratorFacade)facade;
            authentication.AdminToken = adminToken;
            authentication.AdminFacade = adminFacade;
            


            return authentication;
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/administratorfacade/airline/CreateNewAirline")]
        [HttpPost]
        public IHttpActionResult CreateNewAirline(AirlineCompany airline)
        {
            GetLoginToken();
            if (airline == null)
                return Content(HttpStatusCode.NotAcceptable, "No airline to create!");
            try
            {
                authentication.AdminFacade.CreateNewAirline(authentication.AdminToken, airline);
                return Ok($"{airline} Added by {authentication.AdminToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.Unauthorized, $"error creating a new Airline");
            }
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/administratorfacade/airline/UpdateAirlineDetails")]
        [HttpPost]
        public IHttpActionResult UpdateAirlineDetails(AirlineCompany airline)
        {
            GetLoginToken();
            if (airline == null || airline.AIRLINE_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "airline information invalid!");

            try
            {
                authentication.AdminFacade.UpdateAirlineDetails(authentication.AdminToken, airline);
                return Ok($"{airline} Added by {authentication.AdminToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"update error");
            }
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/administratorfacade/airline/RemoveAirline")]
        [HttpDelete]
        public IHttpActionResult RemoveAirline(AirlineCompany airline)
        {
            GetLoginToken();
            if (airline == null || airline.AIRLINE_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "airline information invalid!");

            try
            {
                authentication.AdminFacade.RemoveAirline(authentication.AdminToken, airline);
                return Ok($"{airline} deleted by {authentication.AdminToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"airline remove error");
            }
        }

        [ResponseType(typeof(List<Customer>))]
        [Route("api/administratorfacade/customer/CreateNewCustomer")]
        [HttpPost]
        public IHttpActionResult CreateNewCustomer(Customer customer)
        {
            GetLoginToken();
            if (customer == null)
                return Content(HttpStatusCode.NotAcceptable, "No customer to create!");
            try
            {
                authentication.AdminFacade.CreateNewCustomer(authentication.AdminToken, customer);
                return Ok($"{customer} Added by {authentication.AdminToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.Unauthorized, $"error creating a new Customer");
            }


        }

        [ResponseType(typeof(List<Customer>))]
        [Route("api/administratorfacade/customer/UpdateCustomerDetails")]
        [HttpPost]
        public IHttpActionResult UpdateCustomerDetails(Customer customer)
        {
            GetLoginToken();
            if (customer == null || customer.CUSTOMER_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "customer information invalid!");

            try
            {
                authentication.AdminFacade.UpdateCustomerDetails(authentication.AdminToken, customer);
                return Ok($"{customer} Added by {authentication.AdminToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"customer update error");
            }
        }

        [ResponseType(typeof(List<Customer>))]
        [Route("api/administratorfacade/customer/RemoveCustomer")]
        [HttpDelete]
        public IHttpActionResult RemoveCustomer(Customer customer)
        {
            GetLoginToken();
            if (customer == null || customer.CUSTOMER_ID == 0)
                return Content(HttpStatusCode.NotAcceptable, "customer information invalid!");

            try
            {
                authentication.AdminFacade.RemoveCustomer(authentication.AdminToken, customer);
                return Ok($"{customer} deleted by {authentication.AdminToken.User}");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable, $"customer remove error");
            }
        }
    }
}
