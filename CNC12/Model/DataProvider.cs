using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNC12.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new DataProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        public ManagerCNCEntities DB { get; set; }

        private DataProvider()
        {
            DB = new ManagerCNCEntities();
        }
    }
    #region DataProvider
    //public class DataProvider
    //{
    //    private string connectionSTR = "Data Source=.\\sqlexpress;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

    //    public DataTable ExecuteQuery(string query, object[] parameter = null)
    //    {
    //        DataTable data = new DataTable();

    //        using (SqlConnection connection = new SqlConnection(connectionSTR))
    //        {
    //            connection.Open();

    //            SqlCommand command = new SqlCommand(query, connection);

    //            if (parameter != null)
    //            {
    //                string[] listPara = query.Split(' ');
    //                int i = 0;
    //                foreach (string item in listPara)
    //                {
    //                    if (item.Contains('@'))
    //                    {
    //                        command.Parameters.AddWithValue(item, parameter[i]);
    //                        i++;
    //                    }
    //                }
    //            }

    //            SqlDataAdapter adapter = new SqlDataAdapter(command);

    //            adapter.Fill(data);

    //            connection.Close();
    //        }

    //        return data;
    //    }

    //    public int ExecuteNonQuery(string query, object[] parameter = null)
    //    {
    //        int data = 0;

    //        using (SqlConnection connection = new SqlConnection(connectionSTR))
    //        {
    //            connection.Open();

    //            SqlCommand command = new SqlCommand(query, connection);

    //            if (parameter != null)
    //            {
    //                string[] listPara = query.Split(' ');
    //                int i = 0;
    //                foreach (string item in listPara)
    //                {
    //                    if (item.Contains('@'))
    //                    {
    //                        command.Parameters.AddWithValue(item, parameter[i]);
    //                        i++;
    //                    }
    //                }
    //            }

    //            data = command.ExecuteNonQuery();

    //            connection.Close();
    //        }

    //        return data;
    //    }

    //    public object ExecuteScalar(string query, object[] parameter = null)
    //    {
    //        object data = 0;

    //        using (SqlConnection connection = new SqlConnection(connectionSTR))
    //        {
    //            connection.Open();

    //            SqlCommand command = new SqlCommand(query, connection);

    //            if (parameter != null)
    //            {
    //                string[] listPara = query.Split(' ');
    //                int i = 0;
    //                foreach (string item in listPara)
    //                {
    //                    if (item.Contains('@'))
    //                    {
    //                        command.Parameters.AddWithValue(item, parameter[i]);
    //                        i++;
    //                    }
    //                }
    //            }

    //            data = command.ExecuteScalar();

    //            connection.Close();
    //        }

    //        return data;
    //    }
    //}
    #endregion
}
