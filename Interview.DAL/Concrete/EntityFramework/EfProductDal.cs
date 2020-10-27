using System;
using System.Collections.Generic;
using System.Text;
using Interview.Core.DataAccess.EntityFramework;
using Interview.DAL.Abstract;
using Interview.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Interview.DAL.Concrete.EntityFramework
{
   public class EfProductDal:EfEntityRepositoryBase<Product,InterviewContext>,IProductDal
    {
      
    }
}
