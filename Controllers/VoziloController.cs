using Microsoft.AspNetCore.Mvc;
using Models;
namespace WEBPROJEKAT.Controllers // ?????????????
{
    [ApiController]
    [Route("[controller]")]
    public class VoziloController : ControllerBase
    {
        public AutoSkolaContext Context { get; set; } //da se koristi context u kontroleru

        public VoziloController(AutoSkolaContext context)
        {
            Context=context;
        }
    }
}