using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.EF
{
    public class CategoryEntity
    {

        public CategoryEntity()
        {
            this.Items = new HashSet<ItemEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ItemEntity> Items { get; set; }
    }
}
