using H4App3API.Database.DTO;
using H4App3API.Models;
using H4App3API.Services;
using Microsoft.AspNetCore.Mvc;

namespace H4App3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : Controller
    {
        private readonly IColumnService _service;

        public ColumnController(IColumnService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllColumns()
        {
            try
            {
                List<Column> columnList = await _service.GetAllColumns();
                if (columnList.Count > 0)
                {
                    return Ok(columnList);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColumn([FromRoute] int id, [FromBody] ColumnRequest column)
        {
            if (id > 0)
            {
                try
                {
                    Column updatedColumn = await _service.UpdateColumn(id, column);
                    if (updatedColumn is not null)
                    {
                        return Ok(updatedColumn);
                    }
                    return NotFound();
                }
                catch (Exception ex)
                {

                    return Problem(ex.Message);
                }

            }
            return Problem("invalid request");
        }


        [HttpPost]
        public async Task<IActionResult> CreateColumn([FromBody] ColumnRequest newColumn)
        {
            try
            {
                Column createdColumn = await _service.CreateColumn(newColumn);
                if (createdColumn is null)
                {
                    return NotFound();
                }
                return Ok(createdColumn);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn([FromRoute] int id)
        {
            if (id > 0)
            {
                try
                {
                    Column deletedColumn = await _service.DeleteColumn(id);
                    if (deletedColumn is not null)
                    {
                        return Ok(deletedColumn);
                    }
                    return NotFound();
                }
                catch (Exception ex)
                {

                    return Problem(ex.Message);
                }
            }
            return BadRequest("Id not valid");
        }
    }
}
