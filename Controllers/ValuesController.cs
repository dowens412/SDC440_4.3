using Microsoft.AspNetCore.Mvc;

namespace David_Owens_GP_4._3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}