using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Interview.Business.Abstract;
using Interview.DAL.Concrete.EntityFramework;
using Interview.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Timers;

namespace Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMailer _mailer;
        public Uri url;
        public Uri url1;
        public Uri url2;
        public String html1;
        public String html2;
        public String html;
        public string priceChange;

        private static Timer timerControl =
            new Timer();

        private static int hour = DateTime.Now.Hour;
        

        public ProductController(IProductService productService, IMailer mailer)
        {
            _productService = productService;
            _mailer = mailer;
            timerControl.Elapsed +=new ElapsedEventHandler(TimerControl_Elapsed);
            timerControl.Enabled = true;
            timerControl.Start();

        }

        private void TimerControl_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (hour<DateTime.Now.Hour||(hour==23&& DateTime.Now.Hour==0))
            {
                hour = DateTime.Now.Hour;
                ProductScrape();
            }
        }

        [HttpGet("GetAllProductAds")]
        public List<Product> GetAllProducts()
        {
            return _productService.GetAll();
        }

        [HttpGet("GetByAdsDate/{date:DateTime}")]
        public List<Product> GetByDateAds(DateTime date)
        {
            return _productService.GetByDateAds(date);
        }



        [HttpGet("GetByAdsNumber/{id:int}")]
        public List<Product> GetByAdsNumber(int id)
        {
            return _productService.GetByAdsNumber(id);
        }

        [HttpGet("AdsPriceChangeReport")]
        public async Task<IActionResult> ReportAdsChange()
        {
            InterviewContext context = new InterviewContext();
            var item = context.Products.ToList();


            await _mailer.SendEmailAsync("denemeassa123@gmail.com", "Change", "Change Ads"+priceChange);
            return NoContent();
        }
        [HttpGet("ProductScrape")]
        public async void ProductScrape()
        {
            string price = "7.250 TL";
            string price1 = "100 TL";
            string price2 = "350 TL";

            string Url =
                "https://www.arabam.com/ilan/rent-a-car-dan-kiralik-skoda-superb/skoda-superb-2018-dizel-otomatik-avansis-oto-kiralama/15772963";
            string Url1 = "https://www.arabam.com/ilan/rent-a-car-dan-kiralik-citroen-c-elysee/dedeoglu-dan-kiralik-dizel-araclar/13058389";
            string Url2 = "https://www.arabam.com/ilan/rent-a-car-dan-kiralik-bmw-3-serisi/ador-motors-bmw-320-m-plus-otomatik-gunluk-350-tl/14951347";
            string XPath = "//*[@id='wrapper']/div[5]/div[3]/div/div[1]/p";
            string XPath1 = "//*[@id='js-hook-for-observer-detail']/div[2]/ul/li[1]/span[2]";
            string XPath2 = "//*[@id='js-hook-for-observer-detail']/div[2]/div[1]/div/span";
            url = new Uri(Url);
            url1 = new Uri(Url1);
            url2 = new Uri(Url2);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            html = client.DownloadString(url);
            html1 = client.DownloadString(url1);
            html2 = client.DownloadString(url2);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
            var firstAdsName = document.DocumentNode.SelectSingleNode(XPath).InnerText;
            var firstAdsNumber = document.DocumentNode.SelectSingleNode(XPath1).InnerText;
            var firstAdsPrice = document.DocumentNode.SelectSingleNode(XPath2).InnerText;
            document.LoadHtml(html1);
            var secondAdsName = document.DocumentNode.SelectSingleNode(XPath).InnerText;
            var secondAdsNumber = document.DocumentNode.SelectSingleNode(XPath1).InnerText;
            var secondAdsPrice = document.DocumentNode.SelectSingleNode(XPath2).InnerText;
            document.LoadHtml(html2);
            var thirdAdsName = document.DocumentNode.SelectSingleNode(XPath).InnerText;
            var thirdAdsNumber = document.DocumentNode.SelectSingleNode(XPath1).InnerText;
            var thirdAdsPrice = document.DocumentNode.SelectSingleNode(XPath2).InnerText;
            Product adsProduct = new Product
            {
                Name = firstAdsName,
                AdsNumber = Convert.ToInt32(firstAdsNumber),
                Price = firstAdsPrice,

                Date = DateTime.Now
            };
            Product adsProduct1 = new Product
            {
                Name = secondAdsName,
                AdsNumber = Convert.ToInt32(secondAdsNumber),
                Price = secondAdsPrice,
                Date = DateTime.Now
            };
            Product adsProduct2 = new Product
            {
                Name = thirdAdsName,
                AdsNumber = Convert.ToInt32(thirdAdsNumber),
                Price = thirdAdsPrice,
                Date = DateTime.Now


            };
            InterviewContext context = new InterviewContext();
            if (adsProduct.Price != price)
            {
                priceChange = price + "" + adsProduct.Price;
                await _mailer.SendEmailAsync("denemeassa123@gmail.com", "Change", "ChangeAdsProduct" + priceChange);
            }

            if (adsProduct1.Price != price1)
            {
                priceChange = price + "" + adsProduct1.Price;
                await _mailer.SendEmailAsync("denemeassa123@gmail.com", "Change", "ChangeAdsProduct" + priceChange);
            }

            if (adsProduct2.Price != price2)
            {
                priceChange = price + "" + adsProduct2.Price;
                await _mailer.SendEmailAsync("denemeassa123@gmail.com", "Change", "ChangeAdsProduct" + priceChange);
            }
            await context.Products.AddAsync(adsProduct);
            await context.Products.AddAsync(adsProduct1);
            await context.Products.AddAsync(adsProduct2);
            await context.SaveChangesAsync();


        }

    }
}
