using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestWindConsole.Entities
{
    // TODO: Begin reviewing entity attributes
    [Table("Categories")] // This is called an "attribute" of the class. It adds extra information about the name of the database table that the Category class "maps to".
    public class Category
    {
        [Key] // Identifies this property as mapping to the Primary Key
        public int CategoryID { get; set; }


        [Required] // Use this for string/varchar columns that are NOT NULL
        [StringLength(15, ErrorMessage = "Category Name cannot be more than 15 characters long")]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        [StringLength(40, ErrorMessage = "Picture Mime Type has a maximum of 40 characters")]
        public string PictureMimeType { get; set; }

        // TODO: Introducing Navigation Properties
        // This is a "navigation property" that references a collection of
        // Products that belong this this Category.
        // The virtual keyword allows us to do "lazy-loading".
        // "Lazy-loading" is where we want to get the Category *without* all the products.
        public virtual ICollection<Product> Products { get; set; } =
            // Notice I "initialilze" the property with a new HashSet<Product>
            // Think of this as an "empty list" that I'm initializing it with.
            new HashSet<Product>();
    }
}
