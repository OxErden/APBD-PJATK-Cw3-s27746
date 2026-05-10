using Microsoft.AspNetCore.Mvc;
using APBD_PJATK_Cw3_s27746.Data;
using APBD_PJATK_Cw3_s27746.Models;

namespace APBD_PJATK_Cw3_s27746.Controlers;


[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
//-----------------------------GET------------------------------------
    [HttpGet]
    public IActionResult GetRooms([FromQuery] bool? hasProjector = null, [FromQuery] bool? IsActive = null,
        [FromQuery] int? minCapacity = null)
    {
        var query = DataBase.Rooms.AsEnumerable();

        if (hasProjector.HasValue)
        {
            query = query.Where(r => r.hasProjector == hasProjector.Value);
        }

        if (IsActive.HasValue)
        {
            query = query.Where(r => r.isActive == IsActive.Value);
        }

        if (minCapacity.HasValue)
        {
            query = query.Where(r => r.Capacity >= minCapacity.Value);
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

    [HttpGet("building/floor/{Floor}")]

    public IActionResult GetByFloor(int Floor)
    {

        var room = DataBase.Rooms.Where(r=>r.Floor == Floor);
        
        if (!room.Any())
        {
            return NotFound("No rooms at such floor");
        }
        return Ok(room);
    }
    
    //------------------------------POST-----------------------

    [HttpPost]

    public IActionResult addRoom([FromBody] Room room)
    {
        room.Id = DataBase.NextRoomId();
        
        DataBase.Rooms.Add(room);
        
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);

    }

    //-----------------------------------PUT----------------------
    [HttpPut("{id}")]

    public IActionResult modifyRoom(int id, [FromBody] Room room)
    {

       var existingRoom = DataBase.Rooms.FirstOrDefault(r => r.Id == room.Id);

       if (existingRoom == null)
       {
           return NotFound("Room not found");
       }
       
       existingRoom.Name = room.Name;
       existingRoom.BuildingCode = room.BuildingCode;
       existingRoom.Floor =  room.Floor;
       existingRoom.Capacity = room.Capacity;
       existingRoom.hasProjector = room.hasProjector;
       existingRoom.isActive = room.isActive;
       
       return Ok(existingRoom);

    }
    
    
    //----------------------------DELETE-------------------------

    [HttpDelete("{id}")]

    public IActionResult removeRoom(int id)
    {
        var existingRoom = DataBase.Rooms.FirstOrDefault(r => r.Id == id);

        if (existingRoom == null)
        {
            return NotFound("Room not found");
        }
        var today = DateOnly.FromDateTime(DateTime.Today);
        var hasUpcoming = DataBase.Reservations
            .Any(re => re.RoomId == id && re.Date >= today);

        if (hasUpcoming)
            return Conflict("Cannot delete room with upcoming reservations");
        
        
        DataBase.Rooms.Remove(existingRoom);
        return NoContent();
    }
    






}