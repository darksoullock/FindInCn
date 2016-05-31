using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.DB
{
    public class CategoryToShopSpecificIdMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual Category Category { get; set; }

        public string RemoteCategoryId { get; set; }
    }
}
