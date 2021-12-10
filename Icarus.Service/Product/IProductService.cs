using Icarus.Model;
using Icarus.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Service.Product
{
    public interface IProductService
    {
        public General<ListDeleteViewModel> GetProducts();
        public General<ProductViewModel> Insert(ProductViewModel newProduct);
        public General<UpdateProductViewModel> Update(int id, UpdateProductViewModel product);
        public General<ListDeleteViewModel> Delete(int id);
    }
}
