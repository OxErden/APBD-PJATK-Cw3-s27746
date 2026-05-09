using System.ComponentModel.DataAnnotations;

namespace APBD_PJATK_Cw3_s27746.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }
    
    public int RoomId { get; set; }
    
    [Required]
    public string OrganizerName { get; set; } = string.Empty;

    [Required]
    public string Topic { get; set; } = string.Empty;
    
    public DateOnly Date { get; set; }
    
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public StatusEnum Status { get; set; }

    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime musi być późniejsze niż StartTime.",
                new[] { nameof(EndTime) });
        }
    }
}