﻿using System.Text.Json.Serialization;

namespace CalculatorService.Core.Models.Entities.RequestsModels
{
  public record AddOperationModel
  {
    [JsonPropertyName("Addends")]
    public IEnumerable<int> Addends { get; set; } = new List<int>();
  }
}
