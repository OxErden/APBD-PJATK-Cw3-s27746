using APBD_PJATK_Cw3_s27746.Models;

namespace APBD_PJATK_Cw3_s27746.Data;

public class DataBase
{
    public static List<Room> Rooms = new()
    {
        new Room {Id = 1, Name = "Sala 3", BuildingCode = "A", Floor = 1, Capacity = 50, hasProjector = true, isActive = true},
        new Room {Id = 2, Name = "Aula 1", BuildingCode = "A", Floor = 0, Capacity = 100, hasProjector = true, isActive = true},
        new Room {Id = 3, Name = "Sala 2", BuildingCode = "B", Floor = 2, Capacity = 26, hasProjector = false, isActive = false},
        new Room {Id = 4, Name = "Sala 15", BuildingCode = "C", Floor = 2, Capacity = 20, hasProjector = false, isActive = true},
        new Room {Id = 5, Name = "Sala 28", BuildingCode = "A", Floor = 2, Capacity = 35, hasProjector = true, isActive = true},
        new Room {Id = 6, Name = "Aula 2", BuildingCode = "B", Floor = 0, Capacity = 80, hasProjector = true, isActive = true},
    };


    public static List<Reservation> Reservations = new()
    {
        new Reservation
        { 
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "Warsztaty z ASP.NET Core",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 30),
            Status = StatusEnum.confirmed
        },

        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Jan Nowak",
            Topic = "Szkolenie z REST API",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),
            Status = StatusEnum.planned
        },

        new Reservation
        {
            Id = 3,
            RoomId = 4,
            OrganizerName = "Katarzyna Wiśniewska",
            Topic = "Konsultacje projektowe",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(15, 0),
            Status = StatusEnum.cancelled
        },

        new Reservation
        {
            Id = 4,
            RoomId = 5,
            OrganizerName = "Piotr Zieliński",
            Topic = "Podstawy HTTP",
            Date = new DateOnly(2026, 5, 13),
            StartTime = new TimeOnly(9, 30),
            EndTime = new TimeOnly(11, 0),
            Status = StatusEnum.confirmed
        },

        new Reservation
        {
            Id = 5,
            RoomId = 6,
            OrganizerName = "Maria Lewandowska",
            Topic = "Architektura REST",
            Date = new DateOnly(2026, 5, 14),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(17, 0),
            Status = StatusEnum.planned
        }
    };


}