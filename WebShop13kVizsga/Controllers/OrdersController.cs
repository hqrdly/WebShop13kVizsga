using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop13kVizsga.Dto;
using WebShop13kVizsga.Model;

namespace WebShop13kVizsga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderModel _model;
        public OrdersController(OrderModel model) 
        {
            _model = model;
        }


        #region Orders
        [HttpGet("/orders")]
        public ActionResult<IEnumerable<OrderDto>> OrderList()
        {
            try
            {
                return Ok(_model.OrderList());
            }
            catch
            {
                return NotFound();
            }
        }
        #endregion

        #region OrderById
        [HttpGet("/orders/{id}")]
        public ActionResult<OrderDto> GetOrder([FromRoute] int id)
        {
            try
            {
                var order = _model.GetOrder(id);
                if (order == null) return NotFound();
                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }
        #endregion

        #region NewOrder
        [Authorize(Roles = "User")]
        [HttpPost("/orders")]
        public ActionResult NewOrder(
            [FromQuery] int cartId,
            [FromQuery] int userId,
            [FromQuery] string targetAddress,
            [FromQuery] int targetPhone,
            [FromQuery] int totalPrice)
        {
            try
            {
                _model.NewOrder(new OrderDto
                {
                    CartId = cartId,
                    UserId = userId,
                    TargetAddress = targetAddress,
                    TargetPhone = targetPhone,
                    TotalPrice = totalPrice
                });
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return BadRequest("Rendelés létrehozása nem sikerült.");
            }
        }
        #endregion

        #region ConfirmData
        [Authorize(Roles = "User")]
        [HttpPut("/orders/{id}/confirm-data")]
        public ActionResult ConfirmData(
            [FromRoute] int id,
            [FromQuery] string targetAddress,
            [FromQuery] int targetPhone)
        {
            try
            {
                _model.ConfirmData(id, new ConfirmOrderDataDto
                {
                    TargetAddress = targetAddress,
                    TargetPhone = targetPhone
                });
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return NotFound();
            }
        }
        #endregion

        #region PaymentSuccess
        [Authorize(Roles = "User")]
        [HttpPut("/orders/{id}/payment-success")]
        public ActionResult PaymentSuccess([FromRoute] int id)
        {
            try
            {
                _model.PaymentSuccess(id);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ConfirmOrder
        [Authorize(Roles = "Worker,Admin")]
        [HttpPut("/orders/{id}/confirm-order")]
        public ActionResult ConfirmOrder([FromRoute] int id)
        {
            try
            {
                _model.ConfirmOrder(id);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DeleteOrder
        [Authorize(Roles = "Worker,Admin")]
        [HttpDelete("/orders/{id}")]
        public ActionResult DeleteOrder([FromRoute] int id)
        {
            try
            {
                _model.DeleteOrder(id);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

    }
}
