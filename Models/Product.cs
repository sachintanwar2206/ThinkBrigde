using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge.Models
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            Active = true;
            CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Active { get; set; }
    }
}
