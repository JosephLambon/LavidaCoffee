using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LavidaCoffee.Models
{
    public class EmailRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailRequestId { get; set; }
        public Email Email { get; set; } = default!;

    }
}
