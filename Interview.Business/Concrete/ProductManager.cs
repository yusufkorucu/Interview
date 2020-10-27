using System;
using System.Collections.Generic;
using System.Text;
using Interview.Business.Abstract;
using Interview.DAL.Abstract;
using Interview.Entities.Concrete;

namespace Interview.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;

        }
        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(int productId)
        {
            _productDal.Delete(new Product { Id = productId });
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public List<Product> GetByAdsNumber(int adsNumber)
        {
            return _productDal.GetList(x => x.AdsNumber == adsNumber);
        }

        public List<Product> GetByDateAds(DateTime date)
        {
            return _productDal.GetList(x => x.Date == date);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
