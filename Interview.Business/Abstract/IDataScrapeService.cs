using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Business.Abstract
{
   
    public interface IDataScrapeService
    {


        string GetData(string Url, String XPath);
        
    }
}
