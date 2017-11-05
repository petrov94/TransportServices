using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Transport_Library;
namespace TransportServ
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransportServ" in both code and config file together.
    [ServiceContract]
    public interface ITransportServ
    {
        [OperationContract]
        Company FindCompanybyName(String Name);

        [OperationContract]
        Company FindCompanybyID(int ID);

        [OperationContract]
        List<String> AllCities();

        [OperationContract]
        List<Prevozi> GetAllLinesFromCompany(int ID);

        [OperationContract]
        String Citybyid(int id);

        [OperationContract]
        int CitybyName(string name);

        [OperationContract]
        bool Login(String Name , String Password);

        [OperationContract]
        List<Prevozi> GetAllLines();

        [OperationContract]
        List<Prevozi> GetAllLinesFromTo(String StartPoint,String EndPoint);

        [OperationContract]
        void AddnewCompany(Company prevoz);

        [OperationContract]
        void AddnewLine(Prevozi line);

        [OperationContract]
        void UpdateCompanybyName(String name, String password, String telefone);
    }
}
