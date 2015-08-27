using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.EF
{
    public class ItemEntity
    {

        public ItemEntity()
        {
            this.Categories = new HashSet<CategoryEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        public virtual ICollection<CategoryEntity> Categories { get; set; }
    }
}
