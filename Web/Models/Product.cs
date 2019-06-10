﻿using ServiceStack.DataAnnotations;

public class Product : IHasId
{
    [PrimaryKey]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string PictureUrl { get; set; }
}