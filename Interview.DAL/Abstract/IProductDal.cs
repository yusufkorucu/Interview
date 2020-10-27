using System;
using System.Collections.Generic;
using System.Text;
using Interview.Core.DataAccess;
using Interview.Entities.Concrete;

namespace Interview.DAL.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
    }
}
