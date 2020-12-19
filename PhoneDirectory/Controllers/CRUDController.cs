using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhoneDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        SqlConnection conn = new SqlConnection(new SqlConnectionStringBuilder()
        {
            DataSource = "VMFA40DC2/m.markovic",
            InitialCatalog = "PROOF",
        }.ConnectionString);
    }
}