using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DotNet_QuanLyThuVien_NguyenDVinhKhang
{
    public class SqlServerConnection
    {
        /*
         Trong Visual Studio, vào Project > Add New Data Source....
            Chọn Database và chọn Next.
            Chọn Dataset và Next.
            Chọn New Connection... để mở hộp thoại kết nối.
            Chọn Microsoft SQL Server rồi nhập thông tin kết nối (tên server, tên cơ sở dữ liệu, phương thức xác thực).
             //private static string connectionString = @"Data Source=LAPTOP-S3EQUTET;Initial Catalog=QLNV;Integrated Security=True";
         */

        //Chuột phải Project chọn add new item => chọn Data => Service-based Database
        //Chuột phải file .mdf chọn properties Copy to Output Directory => Do not copy

        //private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNV.mdf;Integrated Security=True";
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Downloads\Documents\HOC_TAP\Workspace\c#\DotNet_QuanLyThuVien_NguyenDVinhKhang\DotNet_QuanLyThuVien_NguyenDVinhKhang\QLYTHUVIEN.mdf"";Integrated Security=True";
        
        //    private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
        //"AttachDbFilename=\"" + Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "QLNV.mdf") + "\";" +
        //"Integrated Security=True";

        public static string ConnectionString => connectionString;


        private SqlConnection connection;

        // Constructor - nhận vào chuỗi kết nối
        public SqlServerConnection()
        {
            //khởi tạo một đối tượng kết nối đến SQL Server bằng cách sử dụng chuỗi kết nối
            connection = new SqlConnection(connectionString);
        }

        // Mở kết nối tới SQL Server
        public void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
            }
        }

        // Đóng kết nối tới SQL Server
        public void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        // Phương thức thực hiện câu lệnh SQL (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(query, connection);
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                CloseConnection();
            }
        }

        // Phương thức thực hiện câu lệnh SQL và trả về DataTable (SELECT)
        public DataTable ExecuteQuery(string query)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
        // Phương thức thực hiện câu lệnh SQL (COUNT, MAX, MIN)
        public int ExecuteScalar(string query)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(query, connection);

                // ExecuteScalar được sử dụng để thực hiện câu lệnh SQL trả về một giá trị đơn lẻ
                object result = command.ExecuteScalar();

                // Kiểm tra kết quả có hợp lệ không, nếu có thì chuyển sang int
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                return -1;  // Trả về -1 nếu có lỗi
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
