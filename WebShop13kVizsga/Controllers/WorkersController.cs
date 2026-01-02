using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop13kVizsga.Model;

namespace WebShop13kVizsga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly WorkerModel _model;
        public WorkersController(WorkerModel model)
        {
            _model = model;
        }

    }
}
