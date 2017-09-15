using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace References
{
    public class References
    {

        public static string ServerName
        {
            get
            {
                return "ASUS\\sqlexpress";
            }
        }

        public static string DatabaseName
        {
            get
            {
                return "DistrTranTest";
            }
        }

        public static string ConnectionString
        {
            get
            {
                return "Data Source=ASUS\\sqlexpress;Initial Catalog=DistrTranTest;Integrated Security=True";
            }
        }
    }
}
