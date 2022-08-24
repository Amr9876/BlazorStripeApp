namespace BlazorStripeApp.Core;

public class Card
{
    [Required]
    [StringLength(19)]
    [Display(Name = "Card Number")]
    public string CardNumber { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public string Expires { get; set; }

    [Required]
    [StringLength(3)]
    [Display(Name = "CVC")]
    public string Cvc { get; set; }
}
