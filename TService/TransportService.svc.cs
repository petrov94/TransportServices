using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TransportLibrary;
namespace TService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TransportService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TransportService.svc or TransportService.svc.cs at the Solution Explorer and start debugging.
    public class TransportService : ITransportService
    {
        public Company AllCompanies()
        {
                Company comp = new Company();
                Database1Entities data = new Database1Entities();
                //comp.ID = data.Companies.First().ID;
                comp.Name = data.Companies.First().Name;
                //comp.Password = data.Companies.First().Password;
                //comp.Phone = data.Companies.First().Phone;
                //comp.UserType = data.Companies.First().UserType;
                return comp;
        }
    }
}
