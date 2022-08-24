namespace BlazorStripeApp.Pages;

public partial class Checkout
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "status")]
    public string Status { get; set; }

    [Inject] public StripeService Stripe { get; set; }
    [Inject] public NavigationManager NavManager { get; set; }

    Product product = new();
    Core.Card card = new();

    string message = string.Empty;
    bool loading = false;

    protected override void OnInitialized()
    {
        product = Stripe.GetProduct();
    }

    async Task Submit()
    {
        loading = true;
        
        try
        {
            await Stripe.CheckoutAsync(product, card);

            NavManager.NavigateTo("/checkout?status=success");
        }
        catch (StripeException e)
        {
            message = e.Message;
        }
        finally
        {
            loading = false;
        }
    }
}
