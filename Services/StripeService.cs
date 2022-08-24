

namespace BlazorStripeApp.Services;

public class StripeService
{

	private readonly IConfiguration _config;
	private readonly ProductService _productService;
	private readonly PaymentIntentService _paymentIntnetService;
	private readonly PaymentMethodService _paymentMethodService;
	private readonly CustomerService _customerService;

	public StripeService(IConfiguration config,
		ProductService productService,
		PaymentIntentService paymentIntnetService,
		PaymentMethodService paymentMethodService,
		CustomerService customerService)
	{
		_config = config;
		_productService = productService;
		_paymentIntnetService = paymentIntnetService;
		_paymentMethodService = paymentMethodService;
		_customerService = customerService;

		StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
	}

	public Product GetProduct()
	{
		var product = _productService.Get("prod_LuG9V0xyuSlMxg");

		return product;
	}

	public async Task CheckoutAsync(Product product, Core.Card card)
	{

		var paymentMethodOptions = new PaymentMethodCreateOptions
		{
			Type = "card",
			Card = new PaymentMethodCardOptions
			{
				Number = card.CardNumber,
				Cvc = card.Cvc,
				ExpMonth = long.Parse(card.Expires.Split("/")[0]),
                ExpYear = long.Parse(card.Expires.Split("/")[1])
            }
		};

		var paymentMethod = await _paymentMethodService.CreateAsync(paymentMethodOptions);

		var customerOptions = new CustomerCreateOptions
		{
			Address = new AddressOptions
			{
				City = "Pub",
				State = "London",
				Country = "UK",
				Line1 = "test line 1",
				Line2 = "test line 2",
				PostalCode = "H7M 2E1"
            },
			Email = "john@gmail.com",
			Name = "John Doe",		
		};

		var customer = await _customerService.CreateAsync(customerOptions);

		var paymentIntentOptions = new PaymentIntentCreateOptions
		{
			Amount = 1900,
			Currency = "USD",
			Customer = customer.Id,
			PaymentMethod = paymentMethod.Id
		};

		var paymentIntent = await _paymentIntnetService.CreateAsync(paymentIntentOptions);

		await _paymentIntnetService.ConfirmAsync(paymentIntent.Id);
	}
}
