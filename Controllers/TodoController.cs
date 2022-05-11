using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TodoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult<List<TodoItem>> GetAll()
        {
            List<TodoItem> items = _context.TodoItems.ToList();
            return Ok(items);
        }

        [HttpPost]

        public IActionResult Add(TodoItem model)
        {
            if (model == null) throw new ArgumentNullException(message: "Todo cannot be null", null);

            _context.TodoItems.Add(model);
            _context.SaveChanges();
            return CreatedAtAction("Add", model);
        }

        [HttpGet("{Id}")]

        public IActionResult SingleItem(int Id)
        {
            if (Id <= 0) return NotFound();

            var item = _context.TodoItems.FirstOrDefault(x => x.Id == Id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }


        [HttpPut("{Id}")]
        public IActionResult EditItem(int Id, [FromBody] TodoItem model)
        {
            if (Id <= 0) return NotFound();
            if (model == null) throw new ArgumentNullException(message: "Todo cannot be null", null);

            var item = _context.TodoItems.FirstOrDefault(x => x.Id == Id);

            if (item == null)
            {
                return NotFound();
            }

            item.Name = model.Name;
            item.Description = model.Description;
            item.DueDate = model.DueDate;

            _context.TodoItems.Update(item);
            _context.SaveChanges();
            return Ok(item);
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteItem(int Id)
        {
            if (Id <= 0) return NotFound();

            var item = _context.TodoItems.FirstOrDefault(x => x.Id == Id);

            if (item == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);
            _context.SaveChanges();
            return Ok(item);
        }
    }
}

