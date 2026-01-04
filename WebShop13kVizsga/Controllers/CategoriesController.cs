using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop13kVizsga.Dto;
using WebShop13kVizsga.Model;
using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryModel _model;

        public CategoriesController(CategoryModel model)
        {
            _model = model;
        }

        [HttpGet("/categories")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            try
            {
                return Ok(_model.GetCategory());
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Worker,Admin")]
        [HttpPost("/create-category")]
        public ActionResult CreateCategory(
            [FromQuery] string categoryName)
        {
            try
            {
                _model.CreateNewCategory(new NewCategoryDto
                {
                    Category_Name = categoryName,
                    Items = new List<Item>()
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
        [HttpPut("/modify-category/{id:int}")]
        public ActionResult ModifyCategory(
            [FromRoute] int id,
            [FromQuery] string newName)
        {
            try
            {
                _model.ModifyCategory(id, new ModifyCategoryDto
                {
                    Category_Id = id,
                    Modif_CategoryName = newName
                });
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Worker,Admin")]
        [HttpDelete("/delete-category/{id:int}")]
        public ActionResult DeleteCategory([FromRoute] int id)
        {
            try
            {
                _model.DeleteCategory(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}