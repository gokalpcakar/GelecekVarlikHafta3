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
        public General<ProductViewModel> GetProducts();
        public General<ProductViewModel> Insert(ProductViewModel newProduct);
        public General<UpdateDeleteViewModel> Update(int id, UpdateDeleteViewModel product);
        public General<UpdateDeleteViewModel> Delete(int id);
    }
}
