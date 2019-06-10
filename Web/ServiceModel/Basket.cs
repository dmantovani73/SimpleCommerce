using ServiceStack;
using System.Collections.Generic;

[Route("/basket", Verbs = "GET")]
public class Basket : IReturn<BasketResponse>
{ }

public class BasketResponse
{
    public List<Product> Data { get; set; }
}