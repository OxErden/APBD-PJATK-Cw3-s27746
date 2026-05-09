using Microsoft.AspNetCore.Mvc;
using APBD_PJATK_Cw3_s27746.Data;
using APBD_PJATK_Cw3_s27746.Models;

namespace APBD_PJATK_Cw3_s27746.Controlers;


[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetRooms([FromQuery] bool? hasProjector = null,  [FromQuery] bool? IsActive = null, [FromQuery] int? minCapacity = null)
    {
        var query = DataBase.Rooms.AsEnumerable();

        if (hasProjector.HasValue)
        {
            query = query.Where(r => r.hasProjector == hasProjector.Value);
        }

        if (IsActive.HasValue)
        {
            query = query.Where(r => r.isActive== IsActive.Value);
        }

        if (minCapacity.HasValue)
            query = query.Where(r => r.Capacity >= minCapacity.Value);
        {
            
        }
        return Ok(query.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var room = DataBase.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound("Room not found");
        }
        return Ok(room);
    }


    [HttpGet("building/{BuildingCode}")]

    public IActionResult GetByBuildingCode(string BuildingCode)
    {
        var room = DataBase.Rooms.Where(r=>r.BuildingCode.Equals(BuildingCode, StringComparison.CurrentCultureIgnoreCase));
        
        if (!room.Any())
        {
            return NotFound("Such building is not existing");
        }
        return Ok(room);
    }
    
    
}