using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TransportLines.Classes;
using TransportLines.View;
namespace TransportLines
{
    class TransportVM : INotifyPropertyChanged
    {
        String LoginName;
        String RegistrationName;
        String startpoint;
        String endpoint;
        public WcfMethods methods = new WcfMethods();
        private ICommand loginCommand { get; set; }
        private ICommand registerCommand { get; set; }
        private ICommand searchCommand { get; set; }
        private ICommand createCommand { get; set; }
        private ICommand updateCommand { get; set; }
        private ICommand createRoute { get; set; }
        private ICommand updateuserinfo { get; set; }
        public TransportVM()
        {
            loginCommand = new RelayCommand(login);
            searchCommand = new RelayCommand(search);
            createCommand = new RelayCommand(Registration);
            createRoute = new RelayCommand(CreateANewRoute);
            updateuserinfo = new RelayCommand(UpdateInfo);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand;
            }
            set
            {
                loginCommand = value;
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return searchCommand;
            }
            set
            {
                searchCommand = value;
            }
        }
        public ICommand CreateCommand
        {
            get
            {
                return createCommand;
            }
            set
            {
                createCommand = value;
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                return updateCommand;
            }
            set
            {
                updateCommand = value;
            }
        }
        public ICommand CreateARoute
        {
            get
            {
                return createRoute;
            }
            set
            {
                createRoute = value;
            }
        }
        public ICommand UpdateUserInfo
        {
            get
            {
                return updateuserinfo;
            }
            set
            {
                updateuserinfo = value;
            }
        }
        private String _InputNameInLogin;
        public String InputString
        {
            get { return _InputNameInLogin; }
            set
            {
                _InputNameInLogin = value;
                NotifyPropertyChanged();
            }
        }

        private String _InputPasswordInLogin;
        public String InputPassword
        {
            get { return _InputPasswordInLogin; }
            set
            {
                _InputPasswordInLogin = value;
                NotifyPropertyChanged();
            }
        }

        private String _StartDestination;
        public String StartDestination
        {
            get { return _StartDestination; }
            set
            {
                _StartDestination = value;
                NotifyPropertyChanged();
            }
        }
        private String _FinishDestination;
        public String FinishDestination
        {
            get { return _FinishDestination; }
            set
            {
                _FinishDestination = value;
                NotifyPropertyChanged();
            }
        }
        private List<String> _sources;
        public List<String> Sources
        {
            get
            {
                return _sources;
            }
            set
            {
                _sources = value;
                NotifyPropertyChanged();
            }
        }
        private bool isCheckedUser;
        public bool IsCheckedUser
        {
            get
            {
                return this.isCheckedUser;
            }
            set
            {
                this.isCheckedUser = value;
                NotifyPropertyChanged();
            }
        }
        private bool isCheckedCompany;
        public bool IsCheckedCompany
        {
            get
            {
                return this.isCheckedCompany;
            }
            set
            {
                this.isCheckedCompany = value;
                NotifyPropertyChanged();
            }
        }
        private String _UserNameReg;
        public String UsernameInReg
        {
            get { return _UserNameReg; }
            set
            {
                _UserNameReg = value;
                NotifyPropertyChanged();
            }
        }
        private String _PasswordInReg;
        public String PasswordInReg
        {
            get { return _PasswordInReg; }
            set
            {
                _PasswordInReg = value;
                NotifyPropertyChanged();
            }
        }
        private String _TelephoneInReg;
        public String TelephoneInReg
        {
            get { return _TelephoneInReg; }
            set
            {
                _TelephoneInReg = value;
                NotifyPropertyChanged();
            }
        }
        private string _NodeCategory;
        public string NodeCategory
        {
            get
            {
                return _NodeCategory;
            }
            set
            {
                _NodeCategory = value;
                NotifyPropertyChanged();
            }
        }
        private System.Windows.Controls.ComboBoxItem _Day;
        public System.Windows.Controls.ComboBoxItem Day
        {
            get
            {
                return _Day;
            }
            set
            {
                _Day = value;
                NotifyPropertyChanged();
            }
        }
        private System.Windows.Controls.ComboBoxItem _Month;
        public System.Windows.Controls.ComboBoxItem Month
        {
            get
            {
                return _Month;
            }
            set
            {
                _Month = value;
                NotifyPropertyChanged();
            }
        }
        private System.Windows.Controls.ComboBoxItem _Year;
        public System.Windows.Controls.ComboBoxItem Year
        {
            get
            {
                return _Year;
            }
            set
            {
                _Year = value;
                NotifyPropertyChanged();
            }
        }
        private System.Windows.Controls.ComboBoxItem _Minute;
        public System.Windows.Controls.ComboBoxItem Minute
        {
            get
            {
                return _Minute;
            }
            set
            {
                _Minute = value;
                NotifyPropertyChanged();
            }
        }
        private System.Windows.Controls.ComboBoxItem _Hour;
        public System.Windows.Controls.ComboBoxItem Hour
        {
            get
            {
                return _Hour;
            }
            set
            {
                _Hour = value;
                NotifyPropertyChanged();
            }
        }
        private System.Windows.Controls.ComboBoxItem _Second;
        public System.Windows.Controls.ComboBoxItem Second
        {
            get
            {
                return _Second;
            }
            set
            {
                _Second = value;
                NotifyPropertyChanged();
            }
        }
        private System.Windows.Controls.ComboBoxItem _PaymentType;
        public System.Windows.Controls.ComboBoxItem PaymentType
        {
            get
            {
                return _PaymentType;
            }
            set
            {
                _PaymentType = value;
                NotifyPropertyChanged();
            }
        }
        private String _StartDestinationInCreate;
        public String StartDestinationInCreate
        {
            get { return _StartDestinationInCreate; }
            set
            {
                _StartDestinationInCreate = value;
                NotifyPropertyChanged();
            }
        }
        private String _FinalDestinationInCreate;
        public String FinalDestinationInCreate
        {
            get { return _FinalDestinationInCreate; }
            set
            {
                _FinalDestinationInCreate = value;
                NotifyPropertyChanged();
            }
        }
        private String _FreeSpacesInCreate;
        public String FreeSpacesInCreate
        {
            get { return _FreeSpacesInCreate; }
            set
            {
                _FreeSpacesInCreate = value;
                NotifyPropertyChanged();
            }
        }
        private String _PriceInCreate;
        public String PriceInCreate
        {
            get { return _PriceInCreate; }
            set
            {
                _PriceInCreate = value;
                NotifyPropertyChanged();
            }
        }
        private String _PasswordInProfile;
        public String PasswordInProfile
        {
            get { return _PasswordInProfile; }
            set
            {
                _PasswordInProfile = value;
                NotifyPropertyChanged();
            }
        }
        private String _TelephoneInProfile;
        public String TelephoneInProfile
        {
            get { return _TelephoneInProfile; }
            set
            {
                _TelephoneInProfile = value;
                NotifyPropertyChanged();
            }
        }
        private String _NameUserSearch;
        public String NameUserSearch
        {
            get { return _NameUserSearch; }
            set
            {
                _NameUserSearch = value;
                NotifyPropertyChanged();
            }
        }
        private String _PhoneUserSearch;
        public String PhoneUserSearch
        {
            get { return _PhoneUserSearch; }
            set
            {
                _PhoneUserSearch = value;
                NotifyPropertyChanged();
            }
        }
        ObservableCollection<Route> mFileNames = new ObservableCollection<Route>();
        public ObservableCollection<Route> FileNames
        {
            get
            {
                return mFileNames;
            }
        }
        public Route SelectedSources { get; set; }
        private void login(object obj)
        {
            String loginname = InputString;
            LoginName = InputString;
            String password = InputPassword;
            int size;
            while (String.IsNullOrEmpty(loginname) || String.IsNullOrEmpty(password) || (Int32.TryParse(loginname, out size)) || (loginname.Length <= 0 || loginname.Length > 30) || (password.Length <= 0 || password.Length > 30)) { MessageBox.Show("INCORRECT PARAMETERS"); return; }

            bool check = methods.Login(loginname, password);
            if (check)
            {
                MessageBox.Show("WELCOME"); return;
            }
            else
            {
                MessageBox.Show("There is no such user"); return;
            }
        }

        private void search(object obj)
        {
            startpoint = StartDestination;
            endpoint = FinishDestination;
            int size;
            bool city1 = false, city2 = false;
            while (String.IsNullOrEmpty(startpoint) || String.IsNullOrEmpty(endpoint) || (Int32.TryParse(startpoint, out size)) || (Int32.TryParse(endpoint, out size)) || (startpoint.Length <= 0 || startpoint.Length > 30) || (endpoint.Length <= 0 || endpoint.Length > 30)) { MessageBox.Show("INCORRECT PARAMETERS"); return; }
            List<String> allcities = methods.GetAllCities();
            foreach (String city in allcities)
            {
                if (string.Equals(city, startpoint, StringComparison.OrdinalIgnoreCase)) city1 = true;
                if (string.Equals(city, endpoint, StringComparison.OrdinalIgnoreCase)) city2 = true;
            }
            if ((city1 == true) && (city2 == true))
            {
                List<Route> AllRoutes = methods.GetAllLinesFromTo(startpoint, endpoint);
                if (AllRoutes.Capacity == 0)
                {
                    MessageBox.Show("There is no routes in this line");
                    return;
                }
                foreach (Route all in AllRoutes)
                {
                    FileNames.Add(all);
                }
                if (SelectedSources != null)
                {
                    SearchResult win = new SearchResult();
                    win.Show();
                    //CompanyWPF company = methods.FindCompanybyName(SelectedSources.ID_Company);
                    //NameUserSearch = company.Name.ToString();
                    //PhoneUserSearch = company.Phone.ToString();
                    getallstrings();
                }
            }
            else { MessageBox.Show("Invalid locations"); return; }
        }
        public List<String> getallstrings()
        {
            List<string> twostrings = new List<string>();
            twostrings.Add(startpoint);
            twostrings.Add(endpoint);
            return twostrings;
        }
        private void Registration(object obj)
        {
            RegistrationName =UsernameInReg;
            bool CheckCompany = IsCheckedCompany;
            bool CheckUser = IsCheckedUser;
            String nameofUserCompany = UsernameInReg;
            String password = PasswordInReg;
            String phone = TelephoneInReg;
            if (CheckCompany == false && CheckUser == false)
            {
                MessageBox.Show("Choose a type of account");
                return;
            }
            int size;
            while (String.IsNullOrEmpty(nameofUserCompany) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(phone) || (!Int32.TryParse(phone, out size)) || (nameofUserCompany.Length <= 0 || nameofUserCompany.Length > 30) || (password.Length <= 0 || password.Length > 30) || (phone.Length <= 0 || phone.Length > 30)) { MessageBox.Show("INCORRECT PARAMETERS"); return; }
            CompanyWPF company = methods.FindCompanybyName(nameofUserCompany);
            CompanyWPF newcompany = new CompanyWPF();
            if (company == null)
            {
                newcompany.Name = nameofUserCompany;
                newcompany.Password = password;
                newcompany.Phone = phone;
                if (CheckCompany == true) newcompany.Type = 1;
                else { newcompany.Type = 2; }
                methods.AddnewCompany(newcompany);
                MessageBox.Show("Registration is Successful");
            }
            else MessageBox.Show("There is such user");

        }
        private void CreateANewRoute(object obj)
        {
            if (LoginName != null)
            {

                String start = StartDestinationInCreate;
                String finish = FinalDestinationInCreate;
                String freespaces = FreeSpacesInCreate;
                String price = PriceInCreate;
                string day = Day.Content.ToString();
                string month = Month.Content.ToString();
                string year = Year.Content.ToString();
                string hour = Hour.Content.ToString();
                string minute = Minute.Content.ToString();
                string second = Second.Content.ToString();
                int dayint, monthint, yearint, hourint, minuteint, secondint,spaces;
                decimal prices;
                Int32.TryParse(day, out dayint);
                Int32.TryParse(month, out monthint);
                Int32.TryParse(year, out yearint);
                Int32.TryParse(hour, out hourint);
                Int32.TryParse(minute, out minuteint);
                Int32.TryParse(second, out secondint);
                if(!Int32.TryParse(freespaces, out spaces)) { MessageBox.Show("Include a valid number of spaces"); return; }
                else
                {
                    if (spaces > 50) { MessageBox.Show("Include a valid number of spaces"); return; }
                }
                if (!decimal.TryParse(price, out prices)) { MessageBox.Show("Include a valid value for price"); return; }
                else
                {
                    if (prices > 50) { MessageBox.Show("Include a valid number of price"); return; }
                }
                bool city1 = false, city2 = false;
                List<String> allcities = methods.GetAllCities();
                foreach (String city in allcities)
                {
                    if (string.Equals(city, start, StringComparison.OrdinalIgnoreCase)) city1 = true;
                    if (string.Equals(city, finish, StringComparison.OrdinalIgnoreCase)) city2 = true;
                }
                if ((city1 == true) && (city2 == true))
                {
                    CompanyWPF idcompany = methods.FindCompanybyName(LoginName);
                    Route newroute = new Route();
                    newroute.ID_Company = idcompany.ID;
                    newroute.Price = prices;
                    int idcity1 = methods.CitybyName(start);
                    int idcity2 = methods.CitybyName(finish);
                    DateTime dt;
                    try {
                        dt = new DateTime(yearint, monthint, dayint, hourint, minuteint, secondint);
                        newroute.Timeof = dt;
                        newroute.StartPoint = idcity1;
                        newroute.EndPoint = idcity2;
                        List<Route> allroutes = methods.GetAllLines();
                        foreach (Route route in allroutes)
                        {
                            if (route.Timeof.Equals(dt) && route.StartPoint.Equals(idcity1) && route.EndPoint.Equals(idcity2) && route.ID_Company.Equals(idcompany.ID)) { MessageBox.Show("There is such route on this time"); return; }
                        }
                        methods.AddnewLine(newroute);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("INVALID DATE");
                    }
                } else MessageBox.Show("There is a incorrect location");

            }
           else if (RegistrationName != null)
            {
                String start = StartDestinationInCreate;
                String finish = FinalDestinationInCreate;
                String freespaces = FreeSpacesInCreate;
                String price = PriceInCreate;
                string day = Day.Content.ToString();
                string month = Month.Content.ToString();
                string year = Year.Content.ToString();
                string hour = Hour.Content.ToString();
                string minute = Minute.Content.ToString();
                string second = Second.Content.ToString();
                int dayint, monthint, yearint, hourint, minuteint, secondint, spaces;
                decimal prices;
                Int32.TryParse(day, out dayint);
                Int32.TryParse(month, out monthint);
                Int32.TryParse(year, out yearint);
                Int32.TryParse(hour, out hourint);
                Int32.TryParse(minute, out minuteint);
                Int32.TryParse(second, out secondint);
                if (!Int32.TryParse(freespaces, out spaces)) { MessageBox.Show("Include a valid number of spaces"); return; }
                else
                {
                    if (spaces > 50) { MessageBox.Show("Include a valid number of spaces"); return; }
                }
                if (!decimal.TryParse(price, out prices)) { MessageBox.Show("Include a valid value for price.INSERT WITH A DOT NOT A COMMA"); return; }
                else
                {
                    if (prices > 50) { MessageBox.Show("Include a valid number of price"); return; }
                }
                bool city1 = false, city2 = false;
                List<String> allcities = methods.GetAllCities();
                foreach (String city in allcities)
                {
                    if (string.Equals(city, start, StringComparison.OrdinalIgnoreCase)) city1 = true;
                    if (string.Equals(city, finish, StringComparison.OrdinalIgnoreCase)) city2 = true;
                }
                if ((city1 == true) && (city2 == true))
                {
                    CompanyWPF idcompany = methods.FindCompanybyName(LoginName);
                    Route newroute = new Route();
                    newroute.ID_Company = idcompany.ID;
                    newroute.Price = prices;
                    int idcity1 = methods.CitybyName(start);
                    int idcity2 = methods.CitybyName(finish);
                    DateTime dt;
                    try
                    {
                        dt = new DateTime(yearint, monthint, dayint, hourint, minuteint, secondint);
                        newroute.Timeof = dt;
                        newroute.StartPoint = idcity1;
                        newroute.EndPoint = idcity2;
                        List<Route> allroutes = methods.GetAllLines();
                        foreach (Route route in allroutes)
                        {
                            if (route.Timeof.Equals(dt) && route.StartPoint.Equals(idcity1) && route.EndPoint.Equals(idcity2) && route.ID_Company.Equals(idcompany.ID)) { MessageBox.Show("There is such route on this time"); return; }
                        }
                        methods.AddnewLine(newroute);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("INVALID DATE");
                    }
                } else MessageBox.Show("There is a incorrect location");
            }
            else MessageBox.Show("Your have to login or register user to create new route");
        }

        private void UpdateInfo(object obj)
        {

            String passwordupdate = PasswordInProfile;
            String Telephone = TelephoneInProfile;
            if (passwordupdate != null && Telephone!=null)
            {
                methods.UpdateCompanybyName(LoginName, passwordupdate, Telephone);
                MessageBox.Show("The data is update Successfully");
            }
            else MessageBox.Show("Have to include all information for update");


        }

    }
}


