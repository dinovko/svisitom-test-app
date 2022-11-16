using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private Models.AppContext _context;
        public WeatherForecastController(Models.AppContext context)
        {
            _context = context;
        }

        [Route("tabledata")]
        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok(_context.TableDatas.ToList());
        }

        [Route("tabledata")]
        [HttpPost]
        public IActionResult InsertInfo(TableData data)
        {
            _context.TableDatas.Add(data);
            _context.SaveChanges();
            return Ok(new { message = "ok", data = data });
        }

        [Route("tabledata")]
        [HttpPut]
        public IActionResult UpdateInfo(UpdateStatusModel data)
        {

            var items = _context.TableDatas.Where(x => data.Ids.Contains(x.Id)).ToList();
            items.ForEach(x => x.Status = data.Status);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Route("tabledataname")]
        [HttpPut]
        public IActionResult UpdateName(int id,[FromBody]UpdateName name)
        {
            var item = _context.TableDatas.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            item.Name = name.Name;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
