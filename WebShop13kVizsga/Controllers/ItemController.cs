using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop13kVizsga.Dto;
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

        [HttpGet("/items")]
        public ActionResult<IEnumerable<ItemDto>> GetItems()
        {
            try
            {
                return Ok(_model.GetItems());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("/items-by-category/{categoryId:int}")]
        public ActionResult<IEnumerable<ItemDto>> GetItemByCategory([FromRoute] int categoryId)
        {
            try
            {
                return Ok(_model.CategorySelectItems(categoryId));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Worker,Admin")]
        [HttpPost("/create-item")]
        public ActionResult CreateItem(
            [FromQuery] int categoryId,
            [FromQuery] string itemName,
            [FromQuery] int price,
            [FromQuery] string description,
            [FromQuery] int quantity)
        {
            try
            {
                _model.CreateNewItem(new ItemDto
                {
                    categoryId = categoryId,
                    itemName = itemName,
                    price = price,
                    description = description,
                    quantity = quantity
                });
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Worker,Admin")]
        [HttpPut("/modify-item/{id:int}")]
        public ActionResult ModifyItem(
            [FromRoute] int id,
            [FromQuery] int categoryId,
            [FromQuery] string itemName,
            [FromQuery] int price,
            [FromQuery] string description,
            [FromQuery] int quantity)
        {
            try
            {
                _model.ModifyItem(id, new ModifyItemDto
                {
                    itemId = id,
                    Modif_CategoryId = categoryId,
                    itemName = itemName,
                    price = price,
                    description = description,
                    quantity = quantity
                });
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Worker,Admin")]
        [HttpDelete("/delete-item/{id:int}")]
        public ActionResult DeleteItem([FromRoute] int id)
        {
            try
            {
                _model.DeleteItem(id);
                return Ok();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}