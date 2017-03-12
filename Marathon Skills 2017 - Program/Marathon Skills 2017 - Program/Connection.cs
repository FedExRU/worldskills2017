using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon_Skills_2017___Program
{
    class Connection
    {
        // Класс с методом GetString, возвращающий строку подключения
        public static string GetString()
        {
            return "Data Source=DESKTOP-BHOPD7U;Initial Catalog=MarathonDB;Integrated Security=True";
        }
    }
}
