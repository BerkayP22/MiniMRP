using System.ComponentModel.DataAnnotations;

namespace MiniMRP.Web.Models;

public class OrderCreateViewModel
{
    [Required(ErrorMessage ="No alanı zorunludur.")]
    public string No { get; set; } = "";
    [Required(ErrorMessage ="Ürün seçimi zorunludur.")]
    public string PositionNo { get; set; } = "";
    [Range(1, long.MaxValue, ErrorMessage = "Lütfen bir ürün seçin.")]
    public long ProductId { get; set; }
    [Range(1, long.MaxValue, ErrorMessage = "Lütfen geçerli bir miktar girin.")]
    public long Quantity { get; set; }
}
