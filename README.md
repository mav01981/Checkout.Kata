# Checkout.Kata



Solution completed with .net core library and unit tests using xunit and Moq.



**Further considerations :-**

Added a **ProductValidator** service as business rules might change in the future.

Added a **DiscountCalculator** , so that more detailed tests can be written when running though discount scenarios.

Added a **OfferProvider** as the potential data sources used for offers might change or additional ones added in the future.

All services are mapped to a interface so that they can be mocked if required.

Have not taken into account **Scalability** or **concurrency** as not part of the kata.

