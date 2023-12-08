using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialServiceApplication
{
    public class SqlQueries
    {
        //SqlQuery to send user details to the database
        public static string SAVE_USER_TO_DATABASE = "INSERT INTO [USER] (firstName, lastName, email, password, gender, mobile, address, postcode, country, role)" +
            " VALUES (@firstName, @lastName, @email, @password, @gender, @mobile, @address, @postcode, @country, @role)";

        public static string VALIDATE_LOGIN_DETAILS = "SELECT firstname, password, role FROM [USER] WHERE email = @email";

        public static string UPDATE_USER_ROLE = "UPDATE [USER] SET role = @role WHERE User_id = @user_id";

        public static string LINK_QUERY = "INSERT INTO CompanySoftware (ref_no, software_id) VALUES (@ref_no, @software_id)";

        public static string ADD_NEW_VENDOR = "INSERT INTO COMPANY([company_name], [company_website], [company_established], [no_of_employees] ) VALUES(@company_name, @company_website, @company_established, @no_of_employees)";

        public static string ADD_NEW_SOFTWARE = " INSERT INTO SOFTWARE ([software_name], [description], [document_to_attach]) VALUES (@software_name, @description, @document_to_attach)";
        public static string joinquery = "SELECT software_id, software_name, description " +
                                  "FROM SOFTWARE " +
                                  "INNER JOIN COMPANY ON SOFTWARE.ref_no = COMPANY.ref_no";

        public static string ADD_NEW_Products = "INSERT INTO Products([name], Rate,VendorId ) VALUES(@name, @Rate,@VendorId)";


        public string displayVendor()
        {
            string DISPLAY_VENDORS = "SELECT COMPANY.ref_no, COMPANY.company_name, COMPANY.company_website, COMPANY.company_established, COMPANY.no_of_employees, SOFTWARE.software_id, SOFTWARE.software_name, SOFTWARE.description " +
                "FROM COMPANY " +
                "LEFT JOIN CompanySoftware ON COMPANY.ref_no = CompanySoftware.ref_no " +
                "LEFT JOIN SOFTWARE ON CompanySoftware.software_id = SOFTWARE.software_id ";
            return DISPLAY_VENDORS;
        }
        public string displaySoftware()
        {
            string DISPLAY_SOFTWARE = "SELECT * FROM SOFTWARE";
            return DISPLAY_SOFTWARE;
        }
        public string displaySoftware(int id)
        {
            string DISPLAY_SOFTWARE = "SELECT * FROM SOFTWARE where software_id='"+id+"' ";
            return DISPLAY_SOFTWARE;
        }
        public string displayProducts()
        {
            string DISPLAY_Products = "SELECT * FROM Products  ";
            return DISPLAY_Products;
        }
        public string displayUser()
        {
            string DISPLAY_USERS = "SELECT * FROM [USER]";
            return DISPLAY_USERS;
        }
     

        public string updateVendor(int ref_no)
        {

            return $"UPDATE COMPANY SET company_name= @company_name, company_website=@company_website, company_established= @company_established WHERE ref_no = '{ref_no}'";
        }

        public string deleteVendor(int ref_no)
        {
            return $"DELETE FROM COMPANY WHERE ref_no = {ref_no}";
        }


        public string updateSoftware(int software_id)
        {

            return $"UPDATE SOFTWARE SET software_name= @software_name, description=@description, document_to_attach= @document_to_attach WHERE software_id = '{software_id}'";
        }

        public string deleteSoftware(int software_id)
        {
            return $"DELETE FROM SOFTWARE WHERE software_id = {software_id}";
        }

        public string updateUserRole(int user_id)
        {
            return $"UPDATE USER SET role = @role WHERE user_id = {user_id}";
        }

        public string deleteUser(int user_id)
        {
            return $"DELETE FROM [USER] WHERE user_id = {user_id}";
        }
    }

}

