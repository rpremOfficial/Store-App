using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record GameDto(
    int ID,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
);

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(50)] string Genre,
    [Range(1, 1000)] decimal Price,
    DateTime ReleaseDate,
    [Url][StringLength(300)] string ImageUri
);

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(50)] string Genre,
    [Range(1, 1000)] decimal Price,
    DateTime ReleaseDate,
    [Url][StringLength(300)] string ImageUri
);