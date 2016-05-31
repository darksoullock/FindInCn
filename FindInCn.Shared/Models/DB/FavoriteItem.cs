using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.DB
{
    public class FavoriteItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual int ShopId { get; set; }

        public virtual Shop Shop { get; set; }

        public string ItemUrl { get; set; }
    }
}
