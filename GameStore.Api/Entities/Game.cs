using System.ComponentModel.DataAnnotations;
namespace GameStore.Api.Entities;

public class Game
{
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    public required string Genre { get; set; }

    [Range(0, 1000)]
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }

    [Url]
    [StringLength(300)]
    public required string ImageUri { get; set; }
}

