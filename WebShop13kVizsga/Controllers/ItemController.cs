using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop13kVizsga.Model;

namespace WebShop13kVizsga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemModel _model;
        public ItemController(ItemModel model)
        {
            _model = model;
        }




    }
}
