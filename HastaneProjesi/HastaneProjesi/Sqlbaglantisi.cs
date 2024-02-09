using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace HastaneProjesi
{
    internal class Sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=MUSTA;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }   
    }
}
