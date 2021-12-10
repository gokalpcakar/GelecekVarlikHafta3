using AutoMapper;
using Icarus.DB.Entities.DataContext;
using Icarus.Model;
using Icarus.Model.Extensions;
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
        public General<ListDeleteViewModel> GetProducts()
        {
            var result = new General<ListDeleteViewModel>();

            using (var context = new IcarusContext())
            {
                var data = context.Product.
                            Where(x => x.IsActive && !x.IsDeleted).
                            OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<ListDeleteViewModel>>(data);
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
                var isAuth = context.User.Any(x => x.Id == model.Iuser &&
                                                           x.IsActive &&
                                                           !x.IsDeleted);

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
        public General<UpdateProductViewModel> Update(int id, UpdateProductViewModel product)
        {
            var result = new General<UpdateProductViewModel>();

            using (var context = new IcarusContext())
            {
                var isAuth = context.Product.Any(x => x.Iuser == product.Iuser);
                var updateProduct = context.Product.SingleOrDefault(i => i.Id == id);

                if (isAuth)
                {
                    if (updateProduct is not null)
                    {
                        var trSaati = DateTime.Now;

                        updateProduct.Name = product.Name;
                        updateProduct.DisplayName = product.DisplayName;
                        updateProduct.Description = product.Description;
                        updateProduct.Price = product.Price;
                        updateProduct.Stock = product.Stock;
                        updateProduct.Udate = DateTime.Now;

                        // Güncellemeler tokyo ve londra saatine göre ne zaman yapılmış onu ekliyoruz
                        updateProduct.UlondonDate = ProductExtensions.toLondonTimeZone(trSaati);
                        updateProduct.UtokyoDate = ProductExtensions.toTokyoTimeZone(trSaati);

                        context.SaveChanges();

                        result.Entity = mapper.Map<UpdateProductViewModel>(updateProduct);
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
        public General<ListDeleteViewModel> Delete(int id)
        {
            var result = new General<ListDeleteViewModel>();

            using (var context = new IcarusContext())
            {
                var product = context.Product.SingleOrDefault(i => i.Id == id);

                if (product is not null)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();

                    result.Entity = mapper.Map<ListDeleteViewModel>(product);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Ürün bulunamadı.";
                }
            }

            return result;
        }
    }
}
