using AutoMapper;
using Icarus.DB.Entities.DataContext;
using Icarus.Model;
using Icarus.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        public ProductService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<ProductViewModel> GetProducts()
        {
            var result = new General<ProductViewModel>();

            using(var context = new IcarusContext())
            {
                var data = context.Product.
                            Where(x => x.IsActive && !x.IsDeleted).
                            OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<ProductViewModel>>(data);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Herhangi bir ürün bulunamadı.";
                }
            }

            return result;
        }
        public General<ProductViewModel> Insert(ProductViewModel newProduct)
        {
            var result = new General<ProductViewModel>();
            var model = mapper.Map<Icarus.DB.Entities.Product>(newProduct);

            using (var context = new IcarusContext())
            {
                var isAuth = context.Product.Any(x => x.Iuser == model.Iuser);

                if (isAuth)
                {
                    model.Idate = DateTime.Now;
                    context.Product.Add(model);
                    context.SaveChanges();

                    result.Entity = mapper.Map<ProductViewModel>(model);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Ürün ekleme yetkiniz bulunmamaktadır.";
                }
            }

            return result;
        }
        public General<UpdateDeleteViewModel> Update(int id, UpdateDeleteViewModel product)
        {
            var result = new General<UpdateDeleteViewModel>();

            using (var context = new IcarusContext())
            {
                var isAuth = context.Product.Any(x => x.Iuser == product.Iuser);
                var updateProduct = context.Product.SingleOrDefault(i => i.Id == id);

                if (isAuth)
                {

                    if (updateProduct is not null)
                    {
                        updateProduct.Name = product.Name;
                        updateProduct.DisplayName = product.DisplayName;
                        updateProduct.Description = product.Description;
                        updateProduct.Price = product.Price;
                        updateProduct.Stock = product.Stock;

                        context.SaveChanges();

                        result.Entity = mapper.Map<UpdateDeleteViewModel>(updateProduct);
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ExceptionMessage = "Ürün bulunamadı.";
                    }
                }
                else
                {
                    result.ExceptionMessage = "Güncelleme yetkiniz bulunmamaktadır.";
                }
            }

            return result;
        }
        public General<UpdateDeleteViewModel> Delete(int id)
        {
            var result = new General<UpdateDeleteViewModel>();

            using (var context = new IcarusContext())
            {
                var product = context.Product.SingleOrDefault(i => i.Id == id);

                if (product is not null)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();

                    result.Entity = mapper.Map<UpdateDeleteViewModel>(product);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Kullanıcı bulunamadı.";
                }
            }

            return result;
        }
    }
}
