using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock;

public class CreateStockRequestDto
{

    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 100000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
    public string Industry { get; set; } = string.Empty;
    [Required]
    public long MarketCap { get; set; }
}