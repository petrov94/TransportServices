using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Transport_Library;
namespace TransportServ
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TransportServ" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TransportServ.svc or TransportServ.svc.cs at the Solution Explorer and start debugging.
    public class TransportServ : ITransportServ
    {
        Transport_DataEntities1 data = new Transport_DataEntities1();
        public List<String> AllCities()
        {
            List<string> allcities = new List<string>();
            List<City> allformdb = data.Cities.ToList();

            foreach (City st in allformdb)
            {
                allcities.Add(st.Name);
            }
            return allcities;
        }

        public string Citybyid(int id)
        {
            String City = (from city in data.Cities where city.Id == id select city.Name).FirstOrDefault();
            if (City == null) return null;
            return City;
        }

        public int CitybyName(string name)
        {
            int City = (from city in data.Cities where city.Name == name select city.Id).FirstOrDefault();
            return City;
        }

        public Company FindCompanybyName(String Name)
        {
            Company company = new Company();
            var companyfromdb = (from companydb in data.Companies where  companydb.Name == Name select companydb).FirstOrDefault();
            if (companyfromdb == null) return null;
            company.ID = companyfromdb.ID;
            company.Name = companyfromdb.Name;
            company.Password = companyfromdb.Password;
            company.Phone = companyfromdb.Phone;
            company.Type = companyfromdb.Type;
            return company;
        }
        public bool Login (String Name,String Password)
        {
            var companyfromdb = (from companydb in data.Companies where companydb.Name == Name && companydb.Password==Password select companydb).First();
            if (companyfromdb == null) return false;
            return true;
        }

        public List<Prevozi> GetAllLinesFromCompany(int ID)
        {
            List<Prevozi> allprevozifromCompany = new List<Prevozi>();
            var allprevozi = (from prevoziall in data.Prevozis where prevoziall.ID_Company == ID select prevoziall).ToList();
            foreach (Prevozi prevoz in allprevozi)
            {
                Prevozi prevozin = new Prevozi();
                prevozin.ID = prevoz.ID;
                prevozin.ID_Company = prevoz.ID_Company;
                prevozin.Price = prevoz.Price;
                prevozin.Timeof = prevoz.Timeof;
                prevozin.StartPoint = prevoz.StartPoint;
                prevozin.EndPoint = prevoz.EndPoint;
                prevozin.Freespaces = prevoz.Freespaces;
                allprevozifromCompany.Add(prevozin);
            }
            return allprevozifromCompany;
        }

        public List<Prevozi> GetAllLines()
        {
            List<Prevozi> allLines = new List<Prevozi>();
            List<Prevozi> allines = data.Prevozis.ToList();
            foreach (Prevozi prevoz in allines)
            {
                Prevozi tmp = new Prevozi();
                tmp.ID = prevoz.ID;
                tmp.ID_Company = prevoz.ID_Company;
                tmp.Freespaces = prevoz.Freespaces;
                tmp.StartPoint = prevoz.StartPoint;
                tmp.EndPoint = prevoz.EndPoint;
                tmp.Price = prevoz.Price;
                tmp.Timeof = prevoz.Timeof;
                allLines.Add(tmp);
            }
            return allLines;
        }

        public Company FindCompanybyID(int ID)
        {
            Company company = new Company();
            var companyfromdb = (from companydb in data.Companies where companydb.ID == ID select companydb).FirstOrDefault();
            if (companyfromdb == null) return null;
            try {
                company.ID = companyfromdb.ID;
                company.Name = companyfromdb.Name;
                company.Password = companyfromdb.Password;
                company.Phone = companyfromdb.Phone;
                company.Type = companyfromdb.Type;
            }catch(Exception e)
            {
                return null;
            }
            return company;
        }

        public List<Prevozi> GetAllLinesFromTo(string StartPoint, string EndPoint)
        {
            List<Prevozi> allprevozifromCompany = new List<Prevozi>();
            int id1 = CitybyName(StartPoint);
            int id2 = CitybyName(EndPoint);
            var allprevozi = (from prevoziall in data.Prevozis where prevoziall.StartPoint == id1 && prevoziall.EndPoint == id2  select prevoziall).ToList();
            foreach (Prevozi prevoz in allprevozi)
            {
                Prevozi prevozin = new Prevozi();
                prevozin.ID = prevoz.ID;
                prevozin.ID_Company = prevoz.ID_Company;
                prevozin.Price = prevoz.Price;
                prevozin.Timeof = prevoz.Timeof;
                prevozin.StartPoint = prevoz.StartPoint;
                prevozin.EndPoint = prevoz.EndPoint;
                prevozin.Freespaces = prevoz.Freespaces;
                allprevozifromCompany.Add(prevozin);
            }
            return allprevozifromCompany;
        }

        public void AddnewCompany(Company company)
        {
            data.Companies.Add(company);
            data.SaveChanges();
        }

        public void AddnewLine(Prevozi line)
        {
            data.Prevozis.Add(line);
            data.SaveChanges();
        }

        public void UpdateCompanybyName(String Name,String password,String telefone)
        {
            Company companyfromdb = (from companydb in data.Companies where companydb.Name == Name select companydb).FirstOrDefault();
            if (companyfromdb != null)
            {
                if (password != null) companyfromdb.Password = password;
                if (telefone != null) companyfromdb.Phone = telefone;
                data.Entry(companyfromdb).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
            }
        }
    }
}

