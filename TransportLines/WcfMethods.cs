using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLines.Classes;
using TransportLines.ServiceReference1;
namespace TransportLines
{
    class WcfMethods
    {
        TransportServClient client = new TransportServClient();
        public CompanyWPF FindCompanybyName(String Name)
        {
            CompanyWPF company = new CompanyWPF();
            Company datafromdb = client.FindCompanybyName(Name);
            if (datafromdb == null) return null;
            company.Name = datafromdb.Name;
            company.Password = datafromdb.Password;
            company.Phone = datafromdb.Phone;
            company.Type = datafromdb.Type;
            return company;
        }

        //public CompanyWPF FindCompanybyName(int id)
        //{
        //    CompanyWPF company = new CompanyWPF();
        //    Company datafromdb = client.FindCompanybyID(id);
        //    company.ID = datafromdb.ID;
        //    company.Name = datafromdb.Name;
        //    company.Password = datafromdb.Password;
        //    company.Phone = datafromdb.Phone;
        //    company.Type = datafromdb.Type;
        //    return company;
        //}

        public List<String> GetAllCities()
        {
            TransportServClient client = new TransportServClient();
            List<String> AllLines = client.AllCities().ToList();
            return AllLines;
        }

        public List<Route> GetAllLinesFromCompany(int ID)
        {
            
            List<Prevozi> alllines = client.GetAllLinesFromCompany(ID).ToList();
            List<Route> all = new List<Route>();
            foreach(Prevozi prevoz in alllines)
            {
                Route temp = new Route();
                temp.ID = prevoz.ID;
                temp.ID_Company = prevoz.ID_Company;
                temp.Price = prevoz.Price;
                temp.StartPoint = prevoz.StartPoint;
                temp.EndPoint = prevoz.EndPoint;
                temp.Freespaces = prevoz.Freespaces;
                temp.Timeof = prevoz.Timeof;
                all.Add(temp);
            }
            return all;
        }

        public String Citybyid(int id)
        {
            String city = client.Citybyid(id);
            return city;
        }

         public int CitybyName(string name)
        {
            int city = client.CitybyName(name);
            return city;
        }

        public bool Login(String Name, String Password)
        {
            bool check = client.Login(Name, Password);
            return check;
        }

        public List<Route> GetAllLines()
        {
            List<Prevozi> alllines = client.GetAllLines().ToList();
            List<Route> all = new List<Route>();
            foreach (Prevozi prevoz in alllines)
            {
                Route tmp = new Route();
                tmp.ID = prevoz.ID;
                tmp.ID_Company = prevoz.ID_Company;
                tmp.Freespaces = prevoz.Freespaces;
                tmp.StartPoint = prevoz.StartPoint;
                tmp.EndPoint = prevoz.EndPoint;
                tmp.Price = prevoz.Price;
                tmp.Timeof = prevoz.Timeof;
                all.Add(tmp);
            }
            return all;
        }

        public List<Route> GetAllLinesFromTo(String StartPoint, String EndPoint)
        {
            List<Prevozi> alllines = client.GetAllLinesFromTo(StartPoint, EndPoint).ToList();
            List<Route> all = new List<Route>();
            foreach (Prevozi prevoz in alllines)
            {
                Route prevozin = new Route();
                prevozin.ID = prevoz.ID;
                prevozin.ID_Company = prevoz.ID_Company;
                prevozin.Price = prevoz.Price;
                prevozin.Timeof = prevoz.Timeof;
                prevozin.StartPoint = prevoz.StartPoint;
                prevozin.EndPoint = prevoz.EndPoint;
                prevozin.Freespaces = prevoz.Freespaces;
                all.Add(prevozin);
            }
            return all;
        }

        public void AddnewCompany(CompanyWPF companynew)
        {
            Company company = new Company();
            company.Name = companynew.Name;
            company.Password = companynew.Password;
            company.Phone = companynew.Phone;
            company.Type = companynew.Type;
            client.AddnewCompany(company);
        }

        public void AddnewLine(Route line)
        {
            Prevozi prevoz = new Prevozi();
            prevoz.ID_Company = line.ID_Company;
            prevoz.Freespaces = line.Freespaces;
            prevoz.StartPoint = line.StartPoint;
            prevoz.EndPoint = line.EndPoint;
            prevoz.Price = line.Price;
            prevoz.Timeof = line.Timeof;

            client.AddnewLine(prevoz);
        }

        public void UpdateCompanybyName(String name, String password, String telefone)
        {
            client.UpdateCompanybyName(name, password, telefone);
        }

    }
}
