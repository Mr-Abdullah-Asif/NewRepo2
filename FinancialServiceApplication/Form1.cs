using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FinancialServiceApplication
{
    public partial class application : Form
    {
        //
        private readonly List<Panel> listPanel = new List<Panel>();

        //
        private readonly ArrayList parameterList = new ArrayList();

        //
        DBConnection dbconnection = DBConnection.getInstanceOfDBConnection();
        SqlQueries sqlQueries = new SqlQueries();
        


        public application()
        {
            InitializeComponent();
      
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db.Products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter.Fill(this.db.Products);
            //
            this.Controls.Add(menuFunctions);
            listPanel.Add(homePage);
            listPanel.Add(aboutUs);
            listPanel.Add(logInPage);
            listPanel.Add(signUpPage);
            listPanel.Add(header);
            listPanel.Add(footer);

            listPanel.Add(vendorPage);
            listPanel.Add(vendorDisplay);
            listPanel.Add(addVendorPanel);
            listPanel.Add(updateVendorPanel);

            listPanel.Add(softwarePage);
            listPanel.Add(displaySoftwarePanel);
            listPanel.Add(addSoftwarePanel);
            listPanel.Add(softwareUpdatePanel);

            listPanel.Add(ProductPage);
            listPanel.Add(ProductDisplayPanel);

            listPanel.Add(adminDataGridViewPanel);
            listPanel.Add(updateUserRolePanel);
            listPanel.Add(deleteUserPanel);

            listPanel.Add(extraPanel);
        }

        //
        //
        //
        //
        // METHODS
        //
        //
        //
        //

        private void CheckSignUpTextBoxes()
        {
            /* This code checks all the text boxes in the sign up page if it is not null or empty.
            * This ensures the user enter details in all the required fields before displaying the signup button
            */
            if (!string.IsNullOrEmpty(firstnameBox.Text) && !string.IsNullOrEmpty(lastnameBox.Text) &&
                !string.IsNullOrEmpty(emailBox.Text) && !string.IsNullOrEmpty(passwordBox.Text) &&
                !string.IsNullOrEmpty(genderBox.Text) && !string.IsNullOrEmpty(mobileBox.Text) &&
                !string.IsNullOrEmpty(addressBox.Text) && !string.IsNullOrEmpty(postcodeBox.Text) &&
                !string.IsNullOrEmpty(countryBox.Text))
            {
                // The signup button is displayed if the required fields are not null
                signUpButton.Visible = true;
            }
            else
            {
                // Else the button stays invisible
                signUpButton.Visible = false;
            }
        }

        private void CheckLogInTextBoxes()
        {
            /* This code checks all the text boxes in the log in page if it is not null or empty.
            * This ensures the user enter details in all the required fields before displaying the login button
            */
            if (!string.IsNullOrEmpty(logInEmailBox.Text) && !string.IsNullOrEmpty(logInPasswordBox.Text))
            {
                // The login button is displayed if the required fields are not null
                logInButton.Visible = true;
            }
            else
            {
                // Else the button stays hidden
                logInButton.Visible = false;
            }
        }

        private void TextBoxLength(object sender, EventArgs e, int maximumLength)
        {
            // cast the sender as a textBox to access its properties
            TextBox textBox = (TextBox)sender;

            /* This code checks if the length of the text is greater than the maximum specified length,
             * truncate the text to the characters specified and move the cursor to the end */
            if (textBox.Text.Length > maximumLength)
            {
                textBox.Text = textBox.Text.Substring(0, maximumLength);
                textBox.Select(textBox.Text.Length, 0);

            }
        }

        private void PanelVisibility(params Control[] panelsToDisplay)
        {
            foreach (Control panel in listPanel)
            {
                panel.Visible = panelsToDisplay.Contains(panel);
            }
        }

        private void DisplayGreeting(string firstname, string role)
        {
            greetingLabel.Visible = true;
            greetingLabel.Text = $"Hello, {firstname}!  [ {role} ]";
        }

        private void DisplayHomepage()
        {
            menuFunctions.Visible = true;
            PanelVisibility(header, footer, homePage);
        }

        private void DisplayAdminPage()
        {
            PanelVisibility(adminDataGridViewPanel);
            deleteUser.Visible = true;
            updateUser.Visible = true;
            createUser.Visible = true;
        }



        //
        //
        //
        //
        // HOME PAGE AND MENU FUNCTIONS
        //
        //
        //
        //

        private void HomeMenuItem_Click(object sender, EventArgs e)
        {
            DisplayHomepage();
        }

        private void LogInMenuItem_Click(object sender, EventArgs e)
        {
            menuFunctions.Visible = false;
            PanelVisibility(logInPage);
        }

        private void AboutUsMenuItem_Click(object sender, EventArgs e)
        {
            PanelVisibility(aboutUs);
        }

        private void SignUpMenuItem_Click(object sender, EventArgs e)
        {
            menuFunctions.Visible = false;
            joinCommunityLabel.Text = "JOIN THE COMMUNITY";
            goBackButtonS.Visible = true;
            backToAdminPage.Visible = false;
            logInRedirectLabel.Visible = true;
            logInLink.Visible = true;
            signUpButton.Text = "SIGN UP";
            PanelVisibility(signUpPage);
        }

        private void LogOutMenuItem_Click(object sender, EventArgs e)
        {
            // After successful logout
            MessageBox.Show("LOG OUT SUCCESSFULLY!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            greetingLabel.Visible = false;
            PanelVisibility(header, footer, homePage);
            vendorButton.Visible = false;
            softwareButton.Visible = false;
            searchText.Visible = false;
            searchButton.Visible = false;
            menuFunctions.Visible = true;
            logoutMenuItem.Visible = false;
            loginMenuItem.Visible = true;
            signupMenuItem.Visible = true;
            adminDataGridViewPanel.Visible = false;
            adminAccessButton.Visible = true;
        }

        private void CompanyButton_Click(object sender, EventArgs e)
        {
            PanelVisibility(header, vendorPage, vendorDisplay);
        }

        private void SoftwareButton_Click(object sender, EventArgs e)
        {
            PanelVisibility(header, softwarePage, displaySoftwarePanel);
                          
        }

        private void ePanelButton_Click(object sender, EventArgs e)
        {
            PanelVisibility(extraPanel);
        }

        private void adminAccessButton_Click(object sender, EventArgs e)
        {
            adminAccessPanel.Visible = !adminAccessPanel.Visible;
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            string accessCode = "#45ab";
            string enteredCode = adminAccessTextBox.Text;


            if (enteredCode == accessCode)
            {
                menuFunctions.Visible = true;
                HomeMenuItem.Visible = true;               
                loginMenuItem.Visible = true;
                signupMenuItem.Visible = true;
                aboutUsMenuItem.Visible = false;
                logoutMenuItem.Visible = false;
                PanelVisibility(adminDataGridViewPanel);
                deleteUser.Visible = true;
                updateUser.Visible = true;
                createUser.Visible = true;
            }
            else
            {
                MessageBox.Show("INVALID!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                adminAccessPanel.Visible = false;
            }
            
        }

        //
        //
        //
        //
        // LOG IN
        //
        //
        //
        //

        private void LogInEmailBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(logInEmailBox, EventArgs.Empty, 100);

            // Check if all text boxes in the log in page have input and display the log in button
            CheckLogInTextBoxes();
        }

        private void LogInPasswordBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(logInPasswordBox, EventArgs.Empty, 20);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckLogInTextBoxes();
        }

        private void LogInPasswordVisibility_CheckedChanged(object sender, EventArgs e)
        {
            // This code if the checkBox(PanelVisibiliy) is checked
            // It sets the PasswordChar of the checkBox to null(\0) if it is checked, thus displaying the password as plain text
            // If not checked, it masked the password by setting the PasswordChar to '*'
            logInPasswordBox.PasswordChar = LogInPasswordVisibility.Checked ? '\0' : '*';
        }

        private void LogInPageGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayHomepage();
        }

        private void SignUpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PanelVisibility(signUpPage);
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            // Get the user's entered email and password
            string email = logInEmailBox.Text;
            string password = logInPasswordBox.Text;

            // Validate the user, retrieve the firstname and role
            bool userValidated = DBConnection.getInstanceOfDBConnection().ValidateUser(email, password, out string firstname, out string role);

            DisplayGreeting(firstname, role);

            if (userValidated)
            {
                MessageBox.Show("LOGIN SUCCESSFUL!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //tbnProducts.Visible = true;
                if (role == "CONSULTANT")
                {
                    // After successful login, display a complete home screen
                    PanelVisibility(header, footer, homePage);
                    vendorButton.Visible = true;
                    softwareButton.Visible = true;
                    tbnProducts.Visible = true;
                    searchText.Visible = true;
                    searchButton.Visible = true;
                    menuFunctions.Visible = true;
                    logoutMenuItem.Visible = true;
                    aboutUsMenuItem.Visible = true;
                    loginMenuItem.Visible = false;
                    signupMenuItem.Visible = false;
                    adminAccessButton.Visible = false;
                    adminAccessPanel.Visible = false;
                }
                else if (role == "ADMINISTRATOR")
                {
                    PanelVisibility(adminDataGridViewPanel);
                    deleteUser.Visible = true;
                    updateUser.Visible = true;
                    createUser.Visible = true;
                    menuFunctions.Visible = true;
                    logoutMenuItem.Visible = true;
                    HomeMenuItem.Visible = false;
                    aboutUsMenuItem.Visible = false;
                    loginMenuItem.Visible = false;
                    signupMenuItem.Visible = false;
                    adminAccessButton.Visible = false;
                    adminAccessPanel.Visible = false;
                }
                else
                {
                    PanelVisibility(header, footer, homePage);
                    vendorButton.Visible = true;
                    softwareButton.Visible = true;
                    tbnProducts.Visible = true;
                    searchText.Visible = true;
                    searchButton.Visible = true;
                    menuFunctions.Visible = true;
                    logoutMenuItem.Visible = true;
                    aboutUsMenuItem.Visible = true;
                    loginMenuItem.Visible = false;
                    signupMenuItem.Visible = false;
                    BtnShowVendorPage.Visible = false;
                    btnAddSoft.Visible = false;
                    adminAccessButton.Visible = false;
                    adminAccessPanel.Visible = false;
                }
                
                
            }
            else
            {
                MessageBox.Show("INVALID CREDENTIALS. PLEASE CHECK LOGIN DETAILS!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            /*

            // Validate the user, retrieve the firstname and role
            if (DBConnection.getInstanceOfDBConnection().ValidateUser(email, password, out string firstname, out string role))
            {
                // Successful login
                MessageBox.Show($"Login successful! User role: {userRole}");

                // Now you can use the userRole information to determine actions or features available to the user
                if (userRole == "Admin")
                {
                    // Do something specific for Admin
                    PanelVisibility(header, footer, homePage);
                    logoutMenuItem.Visible = true;
                    loginMenuItem.Visible = false;
                    //signupMenuItem.Visible = true;
                    searchText.Visible = true;
                    searchButton.Visible = true;
                    menuFunctions.Visible = true;
                    adminDataGridViewPanel.Visible = true;

                }
                else if (userRole == "Consultant")
                {
                    // Do something specific for Consultant
                    PanelVisibility(header, footer, homePage);
                    vendorButton.Visible = true;
                    softwareButton.Visible = true;
                    searchText.Visible = true;
                    searchButton.Visible = true;
                    menuFunctions.Visible = true;
                    logoutMenuItem.Visible = true;
                    loginMenuItem.Visible = false;
                    signupMenuItem.Visible = false;
                    adminDataGridViewPanel.Visible = false;
                }

                // ... (other actions after successful login)
            }
            else
            {
                // Login failed
                MessageBox.Show("Invalid email or password. Please try again.");
            }

            */
        }

        //
        //
        //
        //
        // SIGN UP
        //
        //
        //
        //

        private void SignUpFirstnameBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(firstnameBox, EventArgs.Empty, 50);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();

        }

        private void SignUpLastnameBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(lastnameBox, EventArgs.Empty, 50);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();

        }

        private void SignUpEmailBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(emailBox, EventArgs.Empty, 100);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();

        }

        private void SignUpEmailBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This code check if '@' is the current character and the textbox does not already contain '@'
            if ((e.KeyChar == '@' && !emailBox.Text.Contains('@')) || e.KeyChar == '_' || e.KeyChar == '.')
            {
                // Allow the character to be entered
                e.Handled = false;
            }
            else if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '@' && e.KeyChar != '_' && e.KeyChar != '.')
            {
                // Ignore the input if it's not a letter, digit, or '@', '_', or '.' character
                e.Handled = true;
            }
        }

        private void SignUpPasswordBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(passwordBox, EventArgs.Empty, 20);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();
        }

        private void PasswordVisibility_CheckedChanged(object sender, EventArgs e)
        {
            // This code if the checkBox(PanelVisibiliy) is checked
            // It sets the PasswordChar of the checkBox to null(\0) if it is checked, thus displaying the password as plain text
            // If not checked, it masked the password by setting the PasswordChar to '*'
            passwordBox.PasswordChar = PasswordVisibility.Checked ? '\0' : '*';
        }

        private void SignUpGenderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();

        }

        private void SignUpMobileBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(mobileBox, EventArgs.Empty, 15);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();
        }

        private void SignUpMobileBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This code checks if the curent character entered is a digit or the '+' character
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '+')
            {
                // Allow the character to be entered into the box
                e.Handled = false;
            }
            else if (!char.IsControl(e.KeyChar))
            {
                // Ignore the character
                e.Handled = true;
            }

        }

        private void SignUpAddressBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(addressBox, EventArgs.Empty, 100);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();
        }

        private void SignUpPostcodeBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(postcodeBox, EventArgs.Empty, 7);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();
        }

        private void SignUpCountryBox_TextChanged(object sender, EventArgs e)
        {
            // Set a maximum lentgth for the number of character allowed into the text box
            TextBoxLength(countryBox, EventArgs.Empty, 100);

            // Check if all text boxes in the signup page have input and display the signup button
            CheckSignUpTextBoxes();
        }

        private void SignUpPageGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayHomepage();
        }

        private void LogInLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            menuFunctions.Visible = false;
            PanelVisibility(logInPage);
        }
        
        private void SignUpButton_Click(object sender, EventArgs e)
        {
             /* This method saves a new user.
             It is used when a new user signs up and when the Administrator creates a new user.
             The method uses the 'joinCommunityLabel' to determine where it is called from
             and further redirect to the neceessary page after a successful sign up process */

            // Assign parameters to the text boxes
            string firstname = firstnameBox.Text;
            string lastname = lastnameBox.Text;
            string email = emailBox.Text;

            string enteredPassword = passwordBox.Text;
            string hashPassword = PasswordHasher.HashPassword(enteredPassword); // This process hash the password for security
            string password = hashPassword;

            string gender = genderBox.Text;
            string mobile = mobileBox.Text;
            string address = addressBox.Text;
            string postcode = postcodeBox.Text;
            string country = countryBox.Text;
            string role = "NON-CONSULTANT";

            //Add parameters to the Arraylist
            parameterList.Add(new SqlParameter("firstName", firstname));
            parameterList.Add(new SqlParameter("lastName", lastname));
            parameterList.Add(new SqlParameter("email", email));
            parameterList.Add(new SqlParameter("password", password));
            parameterList.Add(new SqlParameter("gender", gender));
            parameterList.Add(new SqlParameter("mobile", mobile));
            parameterList.Add(new SqlParameter("address", address));
            parameterList.Add(new SqlParameter("postcode", postcode));
            parameterList.Add(new SqlParameter("country", country));
            parameterList.Add(new SqlParameter("role", role));

            //Save to the Database
            DBConnection.getInstanceOfDBConnection().saveToDatabase(SqlQueries.SAVE_USER_TO_DATABASE, parameterList);

            // Display the Administrator page
            if (joinCommunityLabel.Text == "ADD A NEW USER")
            {
                DisplayAdminPage();
            }
            else
            {
                // After successful signup, display a complete home screen
                DisplayHomepage();
                vendorButton.Visible = true;
                softwareButton.Visible = true;
                tbnProducts.Visible = true;
                searchText.Visible = true;
                searchButton.Visible = true;
                menuFunctions.Visible = true;
                logoutMenuItem.Visible = true;
                loginMenuItem.Visible = false;
                signupMenuItem.Visible = false;
                BtnShowVendorPage.Visible = false;
                btnAddSoft.Visible = false;

                DisplayGreeting(firstname, role);
            }
            

        }

        //
        //
        //
        //
        // VENDOR
        //
        //
        //
        //

        private void BtnShowVendorPage_Click(object sender, EventArgs e)
        {
            vendorDisplay.Visible = false;
            addVendorPanel.Visible = true;
            updateVendorPanel.Visible = false;        

        }

        private void btnAddVendor_Click(object sender, EventArgs e)
        {

            string company_name = companyNameTextBox.Text;
            string company_website = websiteTextBox.Text;
            string company_established = establishedDateTextBox.Text;
            string no_of_employees = numEployeesTextBox.Text;
            //string ref_no = updateRefNoTextBox.Text;

            if (!string.IsNullOrEmpty(companyNameTextBox.Text) && !string.IsNullOrEmpty(websiteTextBox.Text) &&
                !string.IsNullOrEmpty(establishedDateTextBox.Text) && !string.IsNullOrEmpty(numEployeesTextBox.Text))
            {
                DBConnection.getInstanceOfDBConnection().AddVendorToDatabase(SqlQueries.ADD_NEW_VENDOR, company_name, company_website, company_established, no_of_employees);
                companyNameTextBox.Text = "";
                websiteTextBox.Text = "";
                establishedDateTextBox.Text = "";
                numEployeesTextBox.Text = "";
                vendorDisplay.Visible = true;
            }
            else
            {
                MessageBox.Show("PLEASE ENTER ALL THE DETAILS!", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            VENDOR.changeVendor(dbconnection, sqlQueries, Convert.ToInt16(updateRefNoTextBox.Text), comTextBox.Text, webTextBox.Text, estTextBox.Text, empTextBox.Text);
            updateRefNoTextBox.Text = "";
            comTextBox.Text = "";
            webTextBox.Text = "";
            estTextBox.Text = "";
            empTextBox.Text = "";
            // MessageBox.Show("UPDATE IS SUCCESSFUL");
            vendorDisplay.Visible = true;
        }

        private void btnDeleteVendor_Click(object sender, EventArgs e)
        {
            VENDOR.eraseVendor(dbconnection, sqlQueries, Convert.ToInt16(updateRefNoTextBox.Text), comTextBox.Text, webTextBox.Text, estTextBox.Text, empTextBox.Text);
            updateRefNoTextBox.Text = "";
            comTextBox.Text = "";
            webTextBox.Text = "";
            estTextBox.Text = "";
            empTextBox.Text = "";
        }

        private void BtnShowVendors_Click(object sender, EventArgs e)
        {

            PanelVisibility(vendorPage, vendorDisplay);
            
        }

        private void displayVendorInGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateRefNoTextBox.ReadOnly = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && displayVendorInGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                displayVendorInGridView.CurrentRow.Selected = true;
                //SOFTWARE.changeSoftware(dbconnection, sqlQueries, Convert.ToInt32(softIDTextBox.Text), updateSoftNameTextBox.Text, updateSoftDescTextBox.Text, filePath.Text);
                updateRefNoTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["ref_no"].FormattedValue.ToString();
                // Assuming "software_name" is in the first column (index 0)
                comTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["company_name"].FormattedValue.ToString();

                // Assuming "description" is in the fourth column (index 3)
                webTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["company_website"].FormattedValue.ToString();

                // Assuming "path" is in the fifth column (index 4)
                estTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["company_established"].FormattedValue.ToString();
                empTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["no_of_employees"].FormattedValue.ToString();

                // Show the update page
                updateVendorPanel.Visible = true;
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && displayVendorInGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                updateRefNoTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["ref_no"].FormattedValue.ToString();
                comTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["company_name"].FormattedValue.ToString();
                webTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["company_website"].FormattedValue.ToString();
                estTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["company_established"].FormattedValue.ToString();
                empTextBox.Text = displayVendorInGridView.Rows[e.RowIndex].Cells["no_of_employees"].FormattedValue.ToString();
                updateVendorPanel.Visible = true;
            }
        }

        private void BtnShowHomePage_Click(object sender, EventArgs e)
        {
            DisplayHomepage();
        }     
       
        private void btnGoBackToAddingVendor_Click(object sender, EventArgs e)
        {

            PanelVisibility(vendorPage, vendorDisplay);
            //vendorDisplay.Visible = true;
            //addVendorPanel.Visible = false;
            //updateVendorPanel.Visible = false;
        }    

        private void vendorDisplay_Paint(object sender, PaintEventArgs e)
        {
            DataSet getVendor = dbconnection.LoadVendors(sqlQueries.displayVendor());
            displayVendorInGridView.DataSource = getVendor.Tables[0];
        }

        //
        //
        //
        //
        // SOFTWARE
        //
        //
        //
        //

        private void btnDeleteSoftwareFromDatabase_Click(object sender, EventArgs e)
        {
            SOFTWARE.eraseSoftware(dbconnection, sqlQueries, Convert.ToInt32(softIDTextBox.Text), updateSoftNameTextBox.Text, updateSoftDescTextBox.Text, filePath.Text);
            // Clear the text boxes
            softIDTextBox.Text = "";
            updateSoftNameTextBox.Text = "";
            updateSoftDescTextBox.Text = "";
            filePath.Text = "";

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            softIDTextBox.ReadOnly = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView2.CurrentRow.Selected = true;
                //SOFTWARE.changeSoftware(dbconnection, sqlQueries, Convert.ToInt32(softIDTextBox.Text), updateSoftNameTextBox.Text, updateSoftDescTextBox.Text, filePath.Text);
                softIDTextBox.Text = dataGridView2.Rows[e.RowIndex].Cells["software_id"].FormattedValue.ToString();
                // Assuming "software_name" is in the first column (index 0)
                updateSoftNameTextBox.Text = dataGridView2.Rows[e.RowIndex].Cells["software_name"].FormattedValue.ToString();

                // Assuming "description" is in the fourth column (index 3)
                updateSoftDescTextBox.Text = dataGridView2.Rows[e.RowIndex].Cells["description"].FormattedValue.ToString();

                // Assuming "path" is in the fifth column (index 4)
                filePath.Text = dataGridView2.Rows[e.RowIndex].Cells["document_to_attach"].FormattedValue.ToString();

                // Show the update page
                softwareUpdatePanel.Visible = true;
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                softIDTextBox.Text = dataGridView2.Rows[e.RowIndex].Cells["software_id"].FormattedValue.ToString();
                updateSoftNameTextBox.Text = dataGridView2.Rows[e.RowIndex].Cells["software_name"].FormattedValue.ToString();
                updateSoftDescTextBox.Text = dataGridView2.Rows[e.RowIndex].Cells["description"].FormattedValue.ToString();
                filePath.Text = dataGridView2.Rows[e.RowIndex].Cells["document_to_attach"].FormattedValue.ToString();
                softwareUpdatePanel.Visible = true;
            }
        }

        private void BtnGoToDeleteSoftware_Click(object sender, EventArgs e)
        {
            softwareUpdatePanel.Visible = false;
            displaySoftwarePanel.Visible = false;
            addSoftwarePanel.Visible = false;

        }

        private void btnBackToDisplaySoftwareFromDeletePage_Click_1(object sender, EventArgs e)
        {
            softwareUpdatePanel.Visible = false;
            displaySoftwarePanel.Visible = true;
            addSoftwarePanel.Visible = false;

        }

        private void btnBacToAddingSoftware_Click(object sender, EventArgs e)
        {

            displaySoftwarePanel.Visible = true;
            addSoftwarePanel.Visible = false;
            softwareUpdatePanel.Visible = false;
        }

        private void updateSoft_Click(object sender, EventArgs e)
        {

            softwareUpdatePanel.Visible = true;
            displaySoftwarePanel.Visible = false;
            addSoftwarePanel.Visible = false;
        }

        private void displaySoftwarePanel_Paint(object sender, PaintEventArgs e)
        {
            DataSet getSoftware = dbconnection.LoadSoftware(sqlQueries.displaySoftware());
            dataGridView2.DataSource = getSoftware.Tables[0];

        }

        private void btnBrowseFile_Click_1(object sender, EventArgs e)
        {
            pathFile.Text = SOFTWARE.pdfAttach();
        }

        private void updateBrowse_Click(object sender, EventArgs e)
        {
            filePath.Text = SOFTWARE.pdfAttach();
        }

        private void btnGoBackToDisplaySoftwarePanel_Click(object sender, EventArgs e)
        {

            displaySoftwarePanel.Visible = true;
            addSoftwarePanel.Visible = false;
            softwareUpdatePanel.Visible = false;
        }

        private void btnAddSoft_Click(object sender, EventArgs e)
        {

            softwareUpdatePanel.Visible = false;
            displaySoftwarePanel.Visible = false;
            addSoftwarePanel.Visible = true;
        }

        private void BtnGoBackToHomePage_Click(object sender, EventArgs e)
        {
            DisplayHomepage();
        }

        private void btnAddSoftwareToDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] pdf = File.ReadAllBytes(pathFile.Text);
                string software_name = softwareNameTextBox.Text;
                string description = softwareDescriptionTextBox.Text;
                string document_to_attach = pathFile.Text;
                //int ref_no;

                if (!string.IsNullOrEmpty(softwareNameTextBox.Text) &&
                !string.IsNullOrEmpty(softwareDescriptionTextBox.Text) &&
                !string.IsNullOrEmpty(pathFile.Text))
                {
                    DBConnection.getInstanceOfDBConnection().AddSoftwareToDatabase(SqlQueries.ADD_NEW_SOFTWARE, software_name, description, document_to_attach);
                    softwareNameTextBox.Text = "";
                    softwareDescriptionTextBox.Text = "";
                    pathFile.Text = "";
                    displaySoftwarePanel.Visible = true;
                }
                else
                {
                    MessageBox.Show("PLEASE ENTER ALL THE DETAILS!", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateSoftware_Click(object sender, EventArgs e)
        {
            softIDTextBox.ReadOnly = true;
            try
            {
                SOFTWARE.changeSoftware(dbconnection, sqlQueries, Convert.ToInt32(softIDTextBox.Text), updateSoftNameTextBox.Text, updateSoftDescTextBox.Text, filePath.Text);
                //softIDTextBox.Text = "";
                updateSoftNameTextBox.Text = "";
                updateSoftDescTextBox.Text = "";
                filePath.Text = "";
                //MessageBox.Show("UPDATE IS SUCCESSFUL");
                displaySoftwarePanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR OCCURRED: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        //
        //
        //
        // ADMINISTRATOR
        //
        //
        //
        //

        private void createUser_Click(object sender, EventArgs e)
        {
            joinCommunityLabel.Text = "ADD A NEW USER";
            goBackButtonS.Visible = false;
            backToAdminPage.Visible = true;
            logInRedirectLabel.Visible = false;
            logInLink.Visible = false;
            signUpButton.Text = "ADD USER";
            PanelVisibility(signUpPage);

        }

        private void backToAdminPage_Click(object sender, EventArgs e)
        {
            DisplayAdminPage();
        }

        private void deleteUserFromDatabase_Click(object sender, EventArgs e)
        {
            admin.DeleteUser(dbconnection, sqlQueries, Convert.ToInt16(userIDTextBox.Text));

            userIDTextBox.Text = "";
            PanelVisibility(adminDataGridViewPanel);
            
        }

        private void adminDataGridViewPanel_Paint(object sender, PaintEventArgs e)
        {
            DataSet getUser = dbconnection.LoadUsers(sqlQueries.displayUser());
            usersList.DataSource = getUser.Tables[0];
        }

        private void deleteUser_Click(object sender, EventArgs e)
        {
            PanelVisibility(adminDataGridViewPanel, deleteUserPanel);
            deleteUser.Visible = false;
            updateUser.Visible = false;
            createUser.Visible = false;

        }

        private void BtnBackToAdminDashboard_Click(object sender, EventArgs e)
        {
            DisplayAdminPage();

        }

        private void BtnBackToAdminDash_Click(object sender, EventArgs e)
        {
            DisplayAdminPage();

        }

        private void updateUser_Click(object sender, EventArgs e)
        {
            PanelVisibility(adminDataGridViewPanel, updateUserRolePanel);
            deleteUser.Visible = false;
            updateUser.Visible = false;
            createUser.Visible = false;

        }

        private void UpdateUserRoleButton_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(userIDUpdateTextBox.Text);
            string newRole = selectUserRoleBox.Text;

            DBConnection.getInstanceOfDBConnection().UpdateUserRole(SqlQueries.UPDATE_USER_ROLE, userId, newRole);

            DisplayAdminPage();
        }

        private void VendorToSoftwareLinkButton_Click(object sender, EventArgs e)
        {
            vendorToSoftwareLinkPanel.Visible = !vendorToSoftwareLinkPanel.Visible;
        }

        private void InitiateVenSofLinkButton_Click(object sender, EventArgs e)
        {
            int ref_no = int.Parse(companyRefTextBox.Text);
            int software_id = int.Parse(softwareIdTextBox.Text);

            DBConnection.getInstanceOfDBConnection().LinkVendorToSoftware(SqlQueries.LINK_QUERY, ref_no, software_id);
        }

        private void vendorPage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Products_Click(object sender, EventArgs e)
        {
            PanelVisibility(header, softwarePage, displaySoftwarePanel);
        }

        private void tbnProducts_Click(object sender, EventArgs e)
        {
            ProductPage.Visible = true;
            ProductDisplayPanel.Visible = true;

            DataSet getSoftware = dbconnection.LoadProducts(sqlQueries.displayProducts());
            dataGridView1.DataSource = getSoftware.Tables[0];
            PanelVisibility(header, ProductPage, ProductDisplayPanel);

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            //softwareUpdatePanel.Visible = false;
            //displaySoftwarePanel.Visible = false;
            //addSoftwarePanel.Visible = true;


            ProductDisplayPanel.Visible = false;
            displaySoftwarePanel.Visible = false;
            addProductPanel.Visible = true;
        }

        private void btnAddPro_Click(object sender, EventArgs e)
        {
            string Name = txtProductDscr.Text;
            string Rate = txtProductName.Text;
            string VendorId = "";
           
            //string ref_no = updateRefNoTextBox.Text;

            if (!string.IsNullOrEmpty(txtProductName.Text) && !string.IsNullOrEmpty(txtProductDscr.Text) 
                )
            {
                DBConnection.getInstanceOfDBConnection().AddProdToDatabase(SqlQueries.ADD_NEW_Products, Name, Rate,"1");
                //companyNameTextBox.Text = "";
                //websiteTextBox.Text = "";
                //establishedDateTextBox.Text = "";
                //numEployeesTextBox.Text = "";
                //vendorDisplay.Visible = true;
            }
            else
            {
                MessageBox.Show("PLEASE ENTER ALL THE DETAILS!", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //
        //
        //
        // 
        //
        //
        //
        //
    }
}