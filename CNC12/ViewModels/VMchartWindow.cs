using LiveCharts;
using LiveCharts.Wpf;
using System.Net;
using System.IO;
using System.Windows.Threading;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using System;
using CNC12.Model;
using System.Linq;
using System.Globalization;

namespace CNC12.ViewModels
{
    class VMchartWindow : BaseVM
    {
        #region value
        private double _Segments1;
        public double Segments1
        {
            get { return _Segments1; }
            set
            {
                _Segments1 = value;
                OnPropertyChanged("Segments1");
            }
        }
        private double _Segments2;
        public double Segments2
        {
            get { return _Segments2; }
            set
            {
                _Segments2 = value;
                OnPropertyChanged("Segments2");
            }
        }
        private double _Segments3;
        public double Segments3
        {
            get { return _Segments3; }
            set
            {
                _Segments3 = value;
                OnPropertyChanged("Segments3");
            }
        }
        private double _Segments4;
        public double Segments4
        {
            get { return _Segments4; }
            set
            {
                _Segments4 = value;
                OnPropertyChanged("Segments4");
            }
        }
        private double _Segments5;
        public double Segments5
        {
            get { return _Segments5; }
            set
            {
                _Segments5 = value;
                OnPropertyChanged("Segments5");
            }
        }
        private double _Segments6;
        public double Segments6
        {
            get { return _Segments6; }
            set
            {
                _Segments6 = value;
                OnPropertyChanged("Segments6");
            }
        }
        private double _Segments7;
        public double Segments7
        {
            get { return _Segments7; }
            set
            {
                _Segments7 = value;
                OnPropertyChanged("Segments7");
            }
        }
        private double _Segments8;
        public double Segments8
        {
            get { return _Segments8; }
            set
            {
                _Segments8 = value;
                OnPropertyChanged("Segments8");
            }
        }
        #endregion

        readonly DispatcherTimer timer;
        public VMchartWindow()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 5000); // update
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            #region oldCode
            //var run1 = DataProvider.Ins.DB.EventManagerCNCs.Where(x=> x.IdCNC == 1 && x.IdHienTrangMayCNC == 1).Count();
            //var allstates1 = DataProvider.Ins.DB.EventManagerCNCs.Where(x => x.IdCNC == 1).Count();
            //if(allstates1 > 0)
            //{
            //    Segments1 = ((int)(((float)run1 / (float)allstates1)*10000))/100;
            //}
            #endregion

            var TheDayFocus = DateTime.Now.ToString("dd/MM/yyyy");
            TimeSpan TimeMachineRun = new System.TimeSpan(0, 0, 0, 0);

            var ListEvenToday = DataProvider.Ins.DB.EventManagerCNCs.Where(x => x.IdCNC == 1 && x.Ngay == TheDayFocus);
            var t1 = ListEvenToday.FirstOrDefault().ThoiDiem;
            var t2 = DateTime.Now.ToString("HH:mm:ss");
            var TimeSurvey = Convert.ToDateTime(t2).Subtract(Convert.ToDateTime(t1));

            var ListRunToday = DataProvider.Ins.DB.EventManagerCNCs.Where(x => x.IdCNC == 1 && x.IdHienTrangMayCNC == 1 && x.Ngay == TheDayFocus);         
            foreach (var run in ListRunToday)
            {
                DateTime d1, d2;
                int IdEventBefore = run.Id - 1;
                if(1 != DataProvider.Ins.DB.EventManagerCNCs.Where(y => y.Id == IdEventBefore).ToArray().LastOrDefault().IdHienTrangMayCNC
                   || DataProvider.Ins.DB.EventManagerCNCs.Where(y => y.Id == IdEventBefore).ToArray().LastOrDefault() == null)
                {
                    d1 = Convert.ToDateTime(run.ThoiDiem/*,new CultureInfo("HH:mm:SS", true)*/);
                    int i = 1;
                    while (true)
                    {
                        int IdEventBehind = run.Id + i;
                        if (IdEventBehind <= ListEvenToday.ToArray().LastOrDefault().Id)
                        {
                            var EventBehind = DataProvider.Ins.DB.EventManagerCNCs.Where(x => x.Id == IdEventBehind).ToArray().LastOrDefault();
                            if (EventBehind.IdHienTrangMayCNC != 1)
                            {
                                d2 = Convert.ToDateTime(EventBehind.ThoiDiem/*, new CultureInfo("HH:mm:SS", true)*/);
                                var PeriodRun = d2.Subtract(d1);
                                TimeMachineRun += PeriodRun;
                                break;
                            }
                            i++;
                        }
                        else
                        {
                            d2 = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                            var PeriodRun = d2.Subtract(d1);
                            TimeMachineRun += PeriodRun;
                            break;
                        }
                    }                    
                }               
            }
            Segments1 = Math.Round(TimeMachineRun.TotalSeconds / TimeSurvey.TotalSeconds *100, 2); // cc

            #region oldCode
            //var run2 = DataProvider.Ins.DB.EventManagerCNCs.Where(x => x.IdCNC == 2 && x.IdHienTrangMayCNC == 1).Count();
            //var allstates2 = DataProvider.Ins.DB.EventManagerCNCs.Where(x => x.IdCNC == 2).Count();
            //if(allstates2 > 0)
            //{
            //    Segments2 = ((int)(((float)run2 / (float)allstates2) * 10000))/100;
            //}
            #endregion

            Segments2 = 100;
            Segments3 = 90;
            Segments4 = 94;
            Segments5 = 55;
            Segments6 = 97;
            Segments7 = 79;
            Segments8 = 68;
        }
    }
}
