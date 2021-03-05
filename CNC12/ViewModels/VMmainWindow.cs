using CNC12.Model;
using CNC12.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static CNC12.Model.DataRequest;
using static CNC12.Model.MachinesCNC;

namespace CNC12.ViewModels
{
    class VMmainWindow : BaseVM
    {
        #region Mess       

        #region Status Machine
        private string _statusMachine1;
        public string StatusMachine1
        {
            get { return _statusMachine1; }
            set
            {
                _statusMachine1 = value;
                OnPropertyChanged("StatusMachine1");
            }
        }
        private string _statusMachine2;
        public string StatusMachine2
        {
            get { return _statusMachine2; }
            set
            {
                _statusMachine2 = value;
                OnPropertyChanged("StatusMachine2");
            }
        }
        private string _statusMachine3;
        public string StatusMachine3
        {
            get { return _statusMachine3; }
            set
            {
                _statusMachine3 = value;
                OnPropertyChanged("StatusMachine3");
            }
        }
        private string _statusMachine4;
        public string StatusMachine4
        {
            get { return _statusMachine4; }
            set
            {
                _statusMachine4 = value;
                OnPropertyChanged("StatusMachine4");
            }
        }
        #endregion

        #region Status Door
        private string _statusDoor1;
        public string StatusDoor1
        {
            get { return _statusDoor1; }
            set
            {
                _statusDoor1 = value;
                OnPropertyChanged("StatusDoor1");
            }
        }
        private string _statusDoor2;
        public string StatusDoor2
        {
            get { return _statusDoor2; }
            set
            {
                _statusDoor2 = value;
                OnPropertyChanged("StatusDoor2");
            }
        }
        private string _statusDoor3;
        public string StatusDoor3
        {
            get { return _statusDoor3; }
            set
            {
                _statusDoor3 = value;
                OnPropertyChanged("StatusDoor3");
            }
        }
        private string _statusDoor4;
        public string StatusDoor4
        {
            get { return _statusDoor4; }
            set
            {
                _statusDoor4 = value;
                OnPropertyChanged("StatusDoor4");
            }
        }
        #endregion

        #region Lights
        public enum Colorss
        {
            Red, Green, Yellow
        }
        private Colorss _Light1;
        public Colorss Light1
        {
            get { return _Light1; }
            set
            {
                _Light1 = value;
                OnPropertyChanged("Light1");
            }
        }
        private Colorss _Light2;
        public Colorss Light2
        {
            get { return _Light2; }
            set
            {
                _Light2 = value;
                OnPropertyChanged("Light2");
            }
        }
        private Colorss _Light3;
        public Colorss Light3
        {
            get { return _Light3; }
            set
            {
                _Light3 = value;
                OnPropertyChanged("Light3");
            }
        }
        private Colorss _Light4;
        public Colorss Light4
        {
            get { return _Light4; }
            set
            {
                _Light4 = value;
                OnPropertyChanged("Light4");
            }
        }
        #endregion

        #region WISE
        private string _Wise1;
        public string Wise1 { get => _Wise1; set { _Wise1 = value; OnPropertyChanged(); } }
        private string _Wise2;
        public string Wise2 { get => _Wise2; set { _Wise2 = value; OnPropertyChanged(); } }
        private string _Wise3;
        public string Wise3 { get => _Wise3; set { _Wise3 = value; OnPropertyChanged(); } }
        private string _Wise4;
        public string Wise4 { get => _Wise4; set { _Wise4 = value; OnPropertyChanged(); } }
        #endregion

        #endregion

        #region Command

        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ChartCommand { get; set; }
        public ICommand ReportCommand { get; set; }
        public ICommand ChonMayCNC1Command { get; set; }
        public ICommand ChonMayCNC2Command { get; set; }
        public ICommand ChonMayCNC3Command { get; set; }
        public ICommand ChonMayCNC4Command { get; set; }
        #endregion

        readonly AdvantechHttpWebUtility m_httpRequest;
        readonly DispatcherTimer timer;
        public VMmainWindow()
        {
            #region command
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                ManagerWindow loginWindow = new ManagerWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as VMmanagerWindow;                
                if (loginVM.IsLogin)
                {
                    p.Show();                   
                }
                else
                {
                    p.Close();
                }               
            });
            ChartCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ChartWindow wd = new ChartWindow(); wd.ShowDialog(); });
            ReportCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ReportWindow wd = new ReportWindow(); wd.ShowDialog(); });
            #endregion

            m_httpRequest = new AdvantechHttpWebUtility();
            m_httpRequest.ResquestResponded += this.OnGetRequestData;
            m_httpRequest.ResquestOccurredError += this.OnGetErrorRequest;
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //timer.Start();
            ChonMayCNC1Command = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                //if(p.IsChecked == true)
                {
                    Wise1 = Convert.ToString(p.Content);
                    StatusMachine1 = StatusDoor1 = "Requesting http...";
                    timer.Start();
                    #region oldCode
                    //Timer_Tick();
                    //try
                    //{                       
                    //    string account = m_httpRequest.BasicAuthAccount,
                    //    password = m_httpRequest.BasicAuthPassword,
                    //    URL = m_httpRequest.URL;
                    //    StatusMachine1 = StatusDoor1 = "Requesting http...";
                    //    //while (true)
                    //    {
                    //        m_httpRequest.SendGETRequest(account, password, URL);
                    //string Jsonstr = m_httpRequest.JsonifyString;
                    //try
                    //{
                    //    if (Jsonstr == null)
                    //    {
                    //        StatusMachine1 = "ERROR LOAD DATA";
                    //        StatusDoor1 = "ERROR LOAD DATA";
                    //    }
                    //    else DeserialiseJson(Jsonstr);
                    //}
                    //        //catch { }
                    //    }                      
                    //}
                    //catch (Exception err)
                    //{
                    //    StatusMachine1 = StatusDoor1 = err.ToString();
                    //}
                    #endregion
                }
            });
            ChonMayCNC2Command = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                //if (p.IsChecked == true)
                {
                    Wise2 = Convert.ToString(p.Content);
                    StatusMachine2 = StatusDoor2 = "Requesting http...";
                }
            });
            ChonMayCNC3Command = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                //if (p.IsChecked == true)
                {
                    Wise3 = Convert.ToString(p.Content);
                    StatusMachine3 = StatusDoor3 = "Requesting http...";
                }
            });
            ChonMayCNC4Command = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                //if (p.IsChecked == true)
                {
                    Wise4 = Convert.ToString(p.Content);
                    StatusMachine4 = StatusDoor4 = "Requesting http...";
                }
            });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                string account = m_httpRequest.BasicAuthAccount,
                password = m_httpRequest.BasicAuthPassword,
                URL = m_httpRequest.URL;                                
                {
                    m_httpRequest.SendGETRequest(account, password, URL);                   
                }
            }
            catch (Exception err)
            {
                StatusMachine1 = StatusDoor1 = err.ToString();
            }
        }

        private void OnGetErrorRequest(Exception e)
        {          
            StatusMachine1 = "ERROR LOAD DATA";
            StatusDoor1 = "ERROR LOAD DATA";
            Light1 = Colorss.Red;
        }

        private void OnGetRequestData(string Jsonstr)
        {
            #region oldCode
            //string account = m_httpRequest.BasicAuthAccount,
            //       password = m_httpRequest.BasicAuthPassword,
            //       URL = m_httpRequest.URL;
            //m_httpRequest.SendGETRequest(account, password, URL);
            // string Jsonstr = m_httpRequest.JsonifyString;
            #endregion
            try
            {
                if (Jsonstr == null)
                {
                    StatusMachine1 = StatusMachine2 = StatusMachine3 = StatusMachine4 = "ERROR LOAD DATA";
                    StatusDoor1 = StatusDoor2 = StatusDoor3 = StatusDoor4 = "ERROR LOAD DATA";
                }
                else DeserialiseJson(Jsonstr, Wise1);

                var eventCNC = new EventManagerCNC();

                if (Wise1 == "CNC 1") { eventCNC.IdCNC = 1; }
                else if (Wise1 == "CNC 2") { eventCNC.IdCNC = 2; }

                if (StatusMachine1 == "RUNNING") { eventCNC.IdHienTrangMayCNC = 1; }
                else if (StatusMachine1 == "STOPPING") { eventCNC.IdHienTrangMayCNC = 2; }
                else if (StatusMachine1 == "FALLING") { eventCNC.IdHienTrangMayCNC = 3; }

                if (StatusDoor1 == "OPENNING") { eventCNC.IdHienTrangCuaMayCNC = 1; }
                else if (StatusDoor1 == "CLOSING") { eventCNC.IdHienTrangCuaMayCNC = 2; }

                eventCNC.Ngay = DateTime.Now.ToString("dd/MM/yyyy");
                eventCNC.ThoiDiem = DateTime.Now.ToString("HH:mm:ss");

                var currentEventCNC = DataProvider.Ins.DB.EventManagerCNCs.ToArray().LastOrDefault();

                using (var db = new ManagerCNCEntities())
                {
                    if(currentEventCNC == null)
                    {
                        db.EventManagerCNCs.Add(eventCNC);
                        db.SaveChanges();
                    }
                    else if(currentEventCNC.IdCNC != eventCNC.IdCNC
                            || currentEventCNC.IdHienTrangCuaMayCNC != eventCNC.IdHienTrangCuaMayCNC
                            || currentEventCNC.IdHienTrangMayCNC != eventCNC.IdHienTrangMayCNC
                            || currentEventCNC.Ngay != eventCNC.Ngay)
                    {
                        db.EventManagerCNCs.Add(eventCNC);
                        db.SaveChanges();
                    }
                }
            }
            catch { }
        }
        private void DeserialiseJson(string Jsonstr, string str)
        {
            try
            {
                var Jwise = JsonConvert.DeserializeObject<Wises>(Jsonstr);
                MachinesCNC.CheckState(Jwise);
                str = Wise1;
                if(str == "CNC 1")
                {
                    if (instance.machinestate == Machine3State.Running)
                    {
                        StatusMachine1 = "RUNNING";
                        Light1 = Colorss.Green;
                    }
                    if (instance.machinestate == Machine3State.Stopping)
                    {
                        StatusMachine1 = "STOPPING";
                        Light1 = Colorss.Red;
                    }
                    if (instance.machinestate == Machine3State.Falling)
                    {
                        StatusMachine1 = "FALLING";
                        Light1 = Colorss.Yellow;
                    }
                    if (instance.statedoor == MachineDoor2Status.Closing)
                    {
                        StatusDoor1 = "CLOSING";
                    }
                    else
                    {
                        StatusDoor1 = "OPENNING";
                    }
                }
                else if(str == "CNC 2")
                {
                    if (instance1.machinestate == Machine3State.Running)
                    {
                        StatusMachine1 = "RUNNING";
                        Light1 = Colorss.Green;
                    }
                    if (instance1.machinestate == Machine3State.Stopping)
                    {
                        StatusMachine1 = "STOPPING";
                        Light1 = Colorss.Red;
                    }
                    if (instance1.machinestate == Machine3State.Falling)
                    {
                        StatusMachine1 = "FALLING";
                        Light1 = Colorss.Yellow;
                    }
                    if (instance1.statedoor == MachineDoor2Status.Closing)
                    {
                        StatusDoor1 = "CLOSING";
                    }
                    else
                    {
                        StatusDoor1 = "OPENNING";
                    }
                }
               
            }
            catch { }
        }
    }
}
