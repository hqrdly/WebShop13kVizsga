using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop13kVizsga.Dto;
using WebShop13kVizsga.Model;

namespace WebShop13kVizsga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly CartModel _model;
        public CartsController(CartModel model)
        {
            _model = model;
        }

        [HttpGet("/get-cart-byid/{cartId:int}")]
        public ActionResult<IEnumerable<CartDto>> GetCart([FromRoute] int cartId)
        {
            try
            {
                return Ok(_model.GetCart(cartId));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/total-price")]
        public ActionResult<int> GetCartTotalPrice([FromQuery] int cartId)
        {
            try
            {
                return Ok(_model.GetCartTotalPrice(cartId));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/modify-cart/{cartId:int}")]
        public ActionResult ModifyCart(
            [FromRoute] int cartId,
            [FromBody] ModifyCartDto dto)
        {
            try
            {
                _model.ModifyCart(cartId, dto);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/delete-item-from-cart/{id:int}")]
        public ActionResult DeleteItemFromCart([FromRoute] int id)
        {
            try
            {
                _model.DeleteItemFromCart(id);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/modify-item-quantity/{id:int}")]
        public ActionResult ModifyItemQuantity([FromQuery] int id, [FromQuery] int quantity)
        {
            try
            {
                _model.ModifyItemQuantity(id, quantity);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
