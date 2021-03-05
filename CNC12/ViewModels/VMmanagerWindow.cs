using CNC12.Model;
using CNC12.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static CNC12.Model.DataRequest;
using static CNC12.Model.MachinesCNC;

namespace CNC12.ViewModels
{
    class VMmanagerWindow : BaseVM
    {
        #region Dang Nhap
        public bool IsLogin { get; set; }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        #endregion

        #region Chon noi dung
        private string _CaLam;
        public string CaLam { get => _CaLam; set { _CaLam = value; OnPropertyChanged(); } }      
        #endregion
        public ICommand ChonCaLamCommand { get; set; }       
        //private readonly VMmainWindow _VMmainWindow;        
        public VMmanagerWindow()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });          
        }
       
        #region login
        void Login(Window p)
        {
            if (p == null)
                return;
            string passEncode = MD5Hash(Base64Encode(Password));
            var accCount = DataProvider.Ins.DB.NhanViens.Where(x=>x.FullName == UserName && x.Password == passEncode).Count();
            if (accCount > 0)
            {
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }         
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        #endregion       
    }
}
