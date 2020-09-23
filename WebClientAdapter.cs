using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ClassLibrary1;
using FlightSystemWebAPI.Models;
using static ClassLibrary1.DAOMSSQL.DeparturesRetrieveDAO;

namespace FlightSystemWebAPI
{
    public class WebClientAdapter : IWebClientAdapter
    {
        public static SqlCommand cmd = new SqlCommand();

        List<WebFlight> WebFlights = new List<WebFlight>();

        public WebClientAdapter()
        {
           
        }



        //  StatusState result;
        // if flight should departure more than 8 hours ahead, status 
        // is unavailable
        // if flight should departure in the next 8 hours status coud be:
        // on-time or delayed
        //webFlight.status = StatusState.On_Time;
        private StatusState CalculateFlightStatus(Flight webFlight)
            {               
                if (webFlight != null)
                      {
                      System.DateTime x1 = new System.DateTime();
                      System.DateTime x2 = webFlight.DEPARTURE_TIME;
                      System.TimeSpan TimePassed = x1 - x2;
                      if (TimePassed.Hours >= 8)
                         return StatusState.Unavailable;
                      if (TimePassed.Hours <= 8)
                         return StatusState.On_Time;
                      if (TimePassed.Hours <= 2)
                         return StatusState.Final;
                   else
                   {
                    return StatusState.Not_Final;
                   }
                }
                return StatusState.Cancelled;

             
            }

            public IList<WebFlight> ReturnDepartures(List<Flight> flights)
            {
                List<WebFlight> WebFlights = new List<WebFlight>();
                Airport AirportInfoForFlight = new Airport();                
                using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                        foreach (Flight flight in flights)
                        {
                            WebFlight CurrentresultFlight = new WebFlight()
                            {
                             flight_id = flight.FLIGHT_ID,
                             airlinecompany_name = GetAirlineName(flight.ORIGIN_COUNTRY_CODE),
                             origincountry_name = (GetAirportInfo(flight.ORIGIN_COUNTRY_CODE)).AIRPORT_CITY,
                             destination_country_name = (GetAirportInfo(flight.DESTINATION_COUNTRY_CODE)).AIRPORT_CITY,
                             departure_time = ($"{flight.DEPARTURE_TIME.Hour}:{flight.DEPARTURE_TIME.Minute}"),
                             status = CalculateFlightStatus(flight),
                             terminal = CalculateTerminal(flight)
                            };
                           WebFlights.Add(CurrentresultFlight);
                           return WebFlights;
                        }
                    return null;
                }

            
            }

           private TerminalState CalculateTerminal(Flight flight)
           {
            //int state;
            Airport Origin;
            Airport Destination;
               if (flight != null)
               {
                Origin = GetAirportInfo(flight.ORIGIN_COUNTRY_CODE);
                Destination = GetAirportInfo(flight.DESTINATION_COUNTRY_CODE);
                if (Origin.COUNTRY_CODE == Destination.COUNTRY_CODE)
                    return TerminalState.Domestic;
                if (Origin.COUNTRY_CODE != Destination.COUNTRY_CODE)
                    return TerminalState.International;

               }
              else
              {
                return TerminalState.Unavailable;
              }
            return TerminalState.Unavailable;

           }

            public static Airport GetAirportInfo(long Countrycode)
            {
                Airport resultAirport = new Airport();
                using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM Airports WHERE COUNTRY_CODE = '{Countrycode}'";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultAirport = new Airport()
                            {
                                AIRPORT_ID = (int)reader["AIRPORT_ID"],
                                AIRPORT_CODE = (string)reader["AIRPORT_CODE"],
                                COUNTRY_CODE = (int)reader["COUNTRY_CODE"],
                                AIRPORT_CITY = (string)reader["AIRPORT_CITY"],
                                AIRPORT_NAME = (string)reader["AIRPORT_NAME"],
                                COUNTRY_NAME = (string)reader["COUNTRY_NAME"]


                            };
                            return resultAirport;
                        }
                        return null;
                    }
                    
                }
               
            }



            public static string GetAirlineName(long id)
            {
                string airlinename = null;
                // AirlineCompany resultAirlineCompany = new AirlineCompany();
                using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT AIRLINE_NAME FROM AirlineCompanies WHERE AIRLINE_ID = '{id}'";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            airlinename = (string)reader["AIRLINE_NAME"];

                        }

                    }
                    return airlinename;
                }
            }


            

            public IList<WebFlight> ReturnLandings(List<Flight> flights)
            {
                List<WebFlight> WebFlights = new List<WebFlight>();
                
                Airport AirportInfoForFlight = new Airport();
                using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = System.Data.CommandType.Text;

                    foreach (Flight flight in flights)
                    {


                        WebFlight CurrentresultFlight = new WebFlight()
                        {
                            flight_id = flight.FLIGHT_ID,
                            airlinecompany_name = GetAirlineName(flight.ORIGIN_COUNTRY_CODE),
                            origincountry_name = (GetAirportInfo(flight.ORIGIN_COUNTRY_CODE)).AIRPORT_CITY,
                            destination_country_name = (GetAirportInfo(flight.DESTINATION_COUNTRY_CODE)).AIRPORT_CITY,
                            departure_time = ($"{flight.DEPARTURE_TIME.Hour}:{flight.DEPARTURE_TIME.Minute}"),
                            status = CalculateFlightStatus(flight),
                            terminal = CalculateTerminal(flight)

                        };
                        WebFlights.Add(CurrentresultFlight);
                        return WebFlights;

                    }
                    return null;
                }


            }
            /*
            public IList<WebFlight> ReturnDepartures(List<Flight> flights)
            {
                List<WebFlight> WebFlights = new List<WebFlight>();
                Airport AirportInfoForFlight = new Airport();
                using (cmd.Connection = new SqlConnection(FlightCenterConfig.ConnectionString))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.CommandText = $"SELECT * FROM Flights";

                    foreach (Flight flight in flights)
                    {


                        WebFlight CurrentresultFlight = new WebFlight()
                        {
                            flight_id = flight.FLIGHT_ID,
                            airlinecompany_name = GetAirlineName(flight.ORIGIN_COUNTRY_CODE),
                            origincountry_name = (GetAirportInfo(flight.ORIGIN_COUNTRY_CODE)).AIRPORT_CITY,
                            destination_country_name = (GetAirportInfo(flight.DESTINATION_COUNTRY_CODE)).AIRPORT_CITY,
                            departure_time = ($"{flight.DEPARTURE_TIME.Hour}:{flight.DEPARTURE_TIME.Minute}"),
                            terminal = TerminalState.International,
                            status = StatusState.On_Time

                        };
                        WebFlights.Add(CurrentresultFlight);
                        return WebFlights;

                    }
                    return null;
                }

            }
            */

        
    }
    
}