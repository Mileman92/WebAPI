using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;




namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            string SQL = "DELETE FROM PhoneDirectory WHERE id ='" + textBox1.Text + "'";
            con.ConnectionString = ("Integrated Security = SSPI; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;");
            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("OBRISAN");
                disp_data(); //poziv metode
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonADD_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61044/api/adresar/AddImenik");

                Imenik imenik = new Imenik()
                {
                    Name = textBox2.Text,
                    Street = textBox3.Text,
                    City = textBox4.Text,
                    Country = textBox5.Text,
                    PhoneNumber = Int32.Parse(textBox6.Text),

                };
                var postTask = client.PostAsJsonAsync<Imenik>("adresar.json", imenik); // ime kontrolera
                postTask.Wait(); // ovdje sacekamo, da se prenesu svi podatci iz servera na klienta

                var result = postTask.Result;
                if (result.IsSuccessStatusCode) // http OK - 200
                {
                    MessageBox.Show("Dodat kontakt");
                    disp_data();
                }
            }
        }
        public void disp_data()  //Get funkcija
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61044/api/adresar/GetTimeTrain");
                var responseTask = client.GetAsync("Adresar.json"); 
                responseTask.Wait(); 

                var result =  responseTask.Result;

                if (result.IsSuccessStatusCode) // http OK - 200
                {
                    var readTask =  result.Content.ReadAsAsync<DataTable>();
                    

                    readTask.Wait(); //stream pretvara u tablicu
                  
                    
                    DataTable table = readTask.Result;
                    dataGridView1.DataSource = table;

                }

            }


            ////SqlConnection con = new SqlConnection();
            ////string SQL = "SELECT * FROM PhoneDirectory";
            ////con.ConnectionString = ("Integrated Security = SSPI; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;");
            ////using (SqlCommand cmd = new SqlCommand(SQL, con))
            ////{
            ////    con.Open();
            ////    cmd.ExecuteNonQuery();
            ////    DataTable dt = new DataTable();
            ////    SqlDataAdapter da = new SqlDataAdapter(cmd);
            ////    da.Fill(dt);
            ////    dataGridView1.DataSource = dt;
            ////    con.Close();
            ////}
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection();
            //string SQL = "UPDATE PhoneDirectory SET name = @name, street = @street, city = @city, country = @country, phoneNumber = @phoneNumber  WHERE id = @id";
            //con.ConnectionString = ("Integrated Security = SSPI; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;");
            //using (SqlCommand cmd = new SqlCommand(SQL, con))
            //{
            //    con.Open();
            //    cmd.Parameters.AddWithValue("@id", textBox1.Text);
            //    cmd.Parameters.AddWithValue("@name", textBox2.Text);
            //    cmd.Parameters.AddWithValue("@street", textBox3.Text);
            //    cmd.Parameters.AddWithValue("@city", textBox4.Text);
            //    cmd.Parameters.AddWithValue("@country", textBox5.Text);
            //    cmd.Parameters.AddWithValue("@phoneNumber", textBox6.Text);

            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("USPEO UPDATE");
            //    disp_data(); //poziv metode
            //}
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61044/api/adresar/UpdateImenik");

                Imenik imenik = new Imenik()
                {
                    ID = Int32.Parse(textBox1.Text),
                    Name = textBox2.Text,
                    Street = textBox3.Text,
                    City = textBox4.Text,
                    Country = textBox5.Text,
                    PhoneNumber = Int32.Parse(textBox6.Text),

                };
                var postTask = client.PutAsJsonAsync<Imenik>("Adresar.json", imenik); // ime kontrolera
                postTask.Wait(); 
                
                var result = postTask.Result;
                if (result.IsSuccessStatusCode) // http OK - 200
                {
                    MessageBox.Show("Izmenjen");
                    disp_data();
                }
            }
        }
            

        private void buttonGET_Click(object sender, EventArgs e)
        {
            disp_data();
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61044/api/adresar/GetTimeTrain1");
                var responseTask = client.GetAsync("GetTimeTrain1"); // ime kontrolera
                responseTask.Wait();

                var result = responseTask.Result;
                var readTask = result.Content.ReadAsStreamAsync();
                readTask.Wait();
                MemoryStream ms = new MemoryStream();
                var copyTask = readTask.Result.CopyToAsync(ms);
                copyTask.Wait();
                pdfViewer1.LoadDocument(ms);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
