using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsSettingsData
    {
        //public static string connectionString = "Server = .; Database = GymManagmentSystem; User Id= sa; Password = sa123456";
        public static string connectionString = ConfigurationManager.ConnectionStrings["GMSConnectionString"].ConnectionString;
    }
}
