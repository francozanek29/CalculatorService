using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.Repository.Entities
{
    /// <summary>
    /// Record that would be use to model and handle the information inside the Repository interface and its implementation.
    /// </summary>
    public record OperationEntity
    {
        public string Operation { get; set; }

        public string Calculation { get; set; }

        public string TrackingId { get; set; }

        public string Date { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
