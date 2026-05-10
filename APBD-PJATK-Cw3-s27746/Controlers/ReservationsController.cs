using APBD_PJATK_Cw3_s27746.Data;
using APBD_PJATK_Cw3_s27746.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_PJATK_Cw3_s27746.Controlers;


[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    
    //----------------------------------GET------------------------------


    [HttpGet]

    public IActionResult getAllReservations
        ([FromQuery] DateOnly? date = null, 
        [FromQuery] StatusEnum? status = null, 
        [FromQuery] string? topic = null,
        [FromQuery] int? roomid = null)
    {
        var query = DataBase.Reservations.AsEnumerable();

        if (date.HasValue)
        {
            query = query.Where(re => re.Date == date.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(re => re.Status == status.Value);
            
        }

        if (!String.IsNullOrEmpty(topic))
        {
            query = query.Where(re => re.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase));
            
        }

        if (roomid.HasValue)
        {
            query = query.Where(re => re.RoomId == roomid.Value);
        }
        
        return Ok(query.ToList());
    }

    [HttpGet("{id}")]

    public IActionResult getReservationById(int id)
    {
        var query = DataBase.Reservations.FirstOrDefault(re => re.Id == id);

        if (query == null)
        {
            return NotFound("Reservation not found");
        }
        
        return Ok(query);
    }
    
    //-----------------------------POST--------------------------

    [HttpPost]

    public IActionResult addReservation([FromBody] Reservation reservation)
    {
        var existingroom = DataBase.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (existingroom == null)
        {
            return NotFound("Cannot add reservation since such room does not exists");
        }

        if (!existingroom.isActive)
        {
            return BadRequest("cannot add reservation since room is not active");
        }

        var conflict = DataBase.Reservations.FirstOrDefault(newreservation =>
            newreservation.StartTime < reservation.EndTime &&
            reservation.StartTime < newreservation.EndTime &&
            newreservation.RoomId == reservation.RoomId &&
            newreservation.Date == reservation.Date);

        if (conflict != null)
        {
            return Conflict("Nowa rezerwacja koliduje z istniejącą już rezerwacją");
        }

        reservation.Id = DataBase.NextReservationId();
        DataBase.Reservations.Add(reservation);
        
        return CreatedAtAction(nameof(getReservationById), new { id = reservation.Id }, reservation);


    }
    
    //----------------------------------------PUT------------------------------------

    [HttpPut("{id}")]

    public IActionResult modifyReservation(int id, [FromBody] Reservation reservation)
    {
        
        var existingReservation = DataBase.Reservations.FirstOrDefault(re => re.Id == id);
        
        if (existingReservation == null)
        {
            return NotFound("Reservation not found");
        }
        
        var existingRoom = DataBase.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (existingRoom == null)
        {
            return NotFound("Cannot modify reservation since such room does not exists");
        }

        if (!existingRoom.isActive)
        {
            return BadRequest("Cannot modify reservation since this room is not active");
        }
        
        var conflict =  DataBase.Reservations.FirstOrDefault(newreservation => 
            newreservation.StartTime < reservation.EndTime &&
            reservation.StartTime < newreservation.EndTime &&
            newreservation.RoomId == reservation.RoomId &&
            newreservation.Date == reservation.Date &&
            newreservation.Id != id);

        if (conflict != null)
        {
            return Conflict("Cannot modify reservation because of a conflict");
        }
       
        
        existingReservation.RoomId = reservation.RoomId;
        existingReservation.OrganizerName = reservation.OrganizerName;
        existingReservation.Topic = reservation.Topic;
        existingReservation.Date =  reservation.Date;
        existingReservation.StartTime = reservation.StartTime;
        existingReservation.EndTime = reservation.EndTime;
        existingReservation.Status = reservation.Status;

        return Ok(existingReservation);
    }
    
    //---------------------------DELETE-----------------------------------


    [HttpDelete("{id}")]

    public IActionResult removeReservation(int id)
    {
        var existingReservation = DataBase.Reservations.FirstOrDefault(re => re.Id == id);

        if (existingReservation == null)
        {
            return NotFound("Reservation not found");
            
        }
        
        DataBase.Reservations.Remove(existingReservation);
        return NoContent();
    }
    
    
    
}