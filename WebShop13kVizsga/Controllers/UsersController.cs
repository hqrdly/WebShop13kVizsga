using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop13kVizsga.Model;

namespace WebShop13kVizsga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserModel _model;
        public UsersController(UserModel model)
        {
            _model = model;
        }


    }
}
