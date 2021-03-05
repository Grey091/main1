using CNC12.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNC12.Model
{
    public class Chart_Model : BaseVM
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private int _IdHienTrangMayCNC;
        public int IdHienTrangMayCNC { get => _IdHienTrangMayCNC; set { _IdHienTrangMayCNC = value; OnPropertyChanged(); } }

        private int _IdCNC;
        public int IdCNC { get => _IdCNC; set { _IdCNC = value; OnPropertyChanged(); } }

        private int _IdHienTrangCuaMayCNC;
        public int IdHienTrangCuaMayCNC { get => _IdHienTrangCuaMayCNC; set { _IdHienTrangCuaMayCNC = value; OnPropertyChanged(); } }

        private string _Ngay;
        public string Ngay { get => _Ngay; set { _Ngay = value; OnPropertyChanged(); } }

        private string _ThoiDiem;
        public string ThoiDiem { get => _ThoiDiem; set { _ThoiDiem = value; OnPropertyChanged(); } }

    }
}
