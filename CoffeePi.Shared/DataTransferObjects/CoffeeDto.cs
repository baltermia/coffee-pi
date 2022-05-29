﻿namespace CoffeePi.Shared.DataTransferObjects;
using CoffeePi.Shared.Enums;
using System.ComponentModel.DataAnnotations;

public class CoffeeDto
{
    public int Id { get; init; }
    public DateTime TimeExecuted { get; set; }
    [Required]
    public string CoffeeType { get; set; }
}
