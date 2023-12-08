using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialServiceApplication
{
    internal class VENDOR
    {

        public static void changeVendor(DBConnection dBConnection, SqlQueries query, int ref_no, string company_name, string company_website, string company_established, string no_of_employees)
        {
            dBConnection.AddVendorToDatabase(query.updateVendor(ref_no), company_name, company_website, company_established, no_of_employees);
            //MessageBox.Show("SUCCESS");

            }


        public static void eraseVendor(DBConnection dBConnection, SqlQueries query, int ref_no, string company_name, string company_website, string company_established, string no_of_employees)
        {
            dBConnection.AddVendorToDatabase(query.deleteVendor(ref_no));
            //MessageBox.Show("SUCCESS");         
        }
        
    }
}