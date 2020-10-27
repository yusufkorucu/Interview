using Interview.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetByAdsNumber(int adsNumber);
        List<Product> GetByDateAds(DateTime date);
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
