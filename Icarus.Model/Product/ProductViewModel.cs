using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Model.Product
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Ürün adı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Ürün adı 50 karakterden fazla olmamalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gösterim adı boş bırakılamaz!")]
        [StringLength(50, ErrorMessage = "Ürün gösterim adı 50 karakterden fazla olmamalıdır.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz.")]
        [StringLength(250, ErrorMessage = "Açıklama 250 karakterden fazla olmamalıdır.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Fiyat girmek zorunludur.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stok adeti boş bırakılamaz!")]
        public int Stock { get; set; }
    }
}
