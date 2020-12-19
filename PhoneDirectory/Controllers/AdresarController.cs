using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Models;

namespace PhoneDirectory.Controllers
{
    [FormatFilter]
    [Route("api/[controller]")]
    [ApiController]

    //http://localhost:61044/api/Adresar/GetTimeTrain
    public class AdresarController : ControllerBase
    {
        [Route("remove/{dirID}")]  //radi
        // DELETE: api/<controller>
        [HttpDelete]
        public ActionResult<DataTable> DeleteDirectoryRecord(String dirID)
        {

            string connectionString = "Integrated Security = False; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {


                string SQL = "DELETE FROM PhoneDirectory WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@id", dirID);

                    cmd.ExecuteNonQuery();
                }

                SQL = "SELECT * FROM PhoneDirectory";
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable("Directories");
                        table.Load(reader);
                        return Ok(table);
                    }
                }
            }
        }

        [Route("AddImenik")]
        [HttpPost("[controller].{format}")]
        public ActionResult<DataTable> RegisImenik(Models.PhoneDirectory imenikregis)
        {


            string connectionString = "Integrated Security = False; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string SQL = "INSERT INTO PhoneDirectory (name, street, city, country,  phoneNumber) VALUES (@name, @street, @city, @country, @phoneNumber)";
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@name", imenikregis.Name);
                    cmd.Parameters.AddWithValue("@street", imenikregis.Street);
                    cmd.Parameters.AddWithValue("@city", imenikregis.City);
                    cmd.Parameters.AddWithValue("@country", imenikregis.Country);
                    cmd.Parameters.AddWithValue("@phoneNumber", imenikregis.PhoneNumber);
                    cmd.ExecuteNonQuery();
                }

                SQL = "SELECT * FROM PhoneDirectory";
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable("Directories");
                        table.Load(reader);
                        return Ok(table);
                    }
                }

            }
        }

        //[Route("AddImenik.{format}")]
        //[HttpPost("[controller].{format}")]
        //public JsonResult AddImenik(Imenik imenikreg)
        //{
        //    ImenikRegistracijaReply imenreply = new ImenikRegistracijaReply();
        //    ImenikRegistracija.getInstance().Add(imenikreg);
        //    imenreply.Ime = imenikreg.Ime;
        //    imenreply.Street = imenikreg.Street;
        //    imenreply.City = imenikreg.City;
        //    imenreply.Country = imenikreg.Country;
        //    imenreply.PhoneNumber = imenikreg.PhoneNumber;
        //    imenreply.RegistracijaStatus = "Uspesna";
        //    return Json(imenreply);
        //}

        //}
        //[Route("InsertImenik")]
        //[HttpPost]
        //public IActionResult InsertImenik(Imenik imenikreg)
        //{

        //    ImenikRegistracijaReply imenreply = new ImenikRegistracijaReply();
        //    ImenikRegistracija.getInstance().Add(imenikreg);
        //    imenreply.Ime = imenikreg.Ime;
        //    imenreply.Street = imenikreg.Street;
        //    imenreply.City = imenikreg.City;
        //    imenreply.Country = imenikreg.Country;
        //    imenreply.PhoneNumber = imenikreg.PhoneNumber;
        //    imenreply.RegistracijaStatus = "Uspesna";
        //    return Ok(imenreply);
        //}
        [Route("UpdateImenik")]
        [HttpPut("[controller].{format}")]
        public ActionResult<DataTable> UpdateImenik(Models.PhoneDirectory imen)
        {
            string connectionString = "Integrated Security = False; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string SQL = "UPDATE PhoneDirectory SET name = @name, street = @street, city = @city, country = @country, phoneNumber = @phoneNumber  WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", imen.ID);
                    cmd.Parameters.AddWithValue("@name", imen.Name);
                    cmd.Parameters.AddWithValue("@street", imen.Street);
                    cmd.Parameters.AddWithValue("@city", imen.City);
                    cmd.Parameters.AddWithValue("@country", imen.Country);
                    cmd.Parameters.AddWithValue("@phoneNumber", imen.PhoneNumber);

                    cmd.ExecuteNonQuery();


                }

                SQL = "SELECT * FROM PhoneDirectory ";
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable("Directories");
                        table.Load(reader);
                        return Ok(table);
                    }
                }
            }
        }

        //[Route("GetImenik")] //radi
        ////[HttpGet]
        //[HttpGet("[controller].{format}")]
        //public ActionResult<DataTable> GetAllImenik()
        //{
        //    string connectionString = "Integrated Security = SSPI; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;";
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string SQL = "SELECT * FROM PhoneDirectory";
        //        using (SqlCommand cmd = new SqlCommand(SQL, conn))
        //        {
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                DataTable table = new DataTable("Directories");
        //                table.Load(reader);
        //                return Ok(table);
        //            }
        //        }
        //    }
        //}

        [Route("GetTimeTrain")] //radi
        //[HttpGet]
        [HttpGet("[controller].{format}")]
        public ActionResult<DataTable> GetTimeTrain()
        {
            string connectionString = "Integrated Security = False; Data source = VMFA40DC2\\MSSQLDEV; Initial Catalog = PROOF;User ID=sa;Password=ascet";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM PhoneDirectory";
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable("Test");
                        table.Load(reader);
                        return Ok(table);
                    }
                }
            }
        }



        //[HttpGet("GetImenikReport")]
        //public ActionResult<FileStreamResult> GetImenikReport()
        //{
        //    XtraReport doc = new XtraReport();
        //    doc.CreateDocument();
        //    doc.LoadLayout(@"C:\Putanja\adresar.repx");
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        doc.ExportToPdf(ms);
        //        //return Ok(new FileStreamResult(ms, "application/pdf"));
        //        return File(ms.GetBuffer(), "application/pdf");

        //    }

        //}

        [HttpGet("GetTimeTrain1")]
        public ActionResult<FileStreamResult> GetTimeTrainReport()
        {
            XtraReport1 doc = new XtraReport1();
            doc.CreateDocument();
            doc.LoadLayout(@"C:\Putanja\XtraReport1.repx");
            using (MemoryStream ms = new MemoryStream())
            {
                doc.ExportToPdf(ms);
                //return Ok(new FileStreamResult(ms, "application/pdf"));
                return File(ms.GetBuffer(), "application/pdf");

            }

        }

    }
}
