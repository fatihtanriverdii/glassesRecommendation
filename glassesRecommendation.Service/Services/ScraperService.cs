using AngleSharp.Html.Parser;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using HtmlAgilityPack;
using PuppeteerSharp;
using System.Text.RegularExpressions;

namespace glassesRecommendation.Service.Services
{
    public class ScraperService : IScraperService
    {
        private readonly HttpClient _http;

        public ScraperService(HttpClient httpClient)
        {
            _http = httpClient;
        }
        public async Task<ProductScrapeResult> ScrapeProductAsync(string url, CancellationToken cancellationToken)
        {
            var html = await _http.GetStringAsync(url);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // Title: örneğin <h1 class="product-title"> içinden
            var titleText = doc.DocumentNode
                .SelectSingleNode("//title")?.InnerText.Trim();

            // — 2) Price: önce eski XPath, yoksa yeni XPath
            var priceXPaths = new[] {
                "//span[contains(@class,'price')]",            
                "//*[@id='fiyat2']/span[1]"                     
    };
            HtmlNode priceNode = null;
            foreach (var xp in priceXPaths)
            {
                priceNode = doc.DocumentNode.SelectSingleNode(xp);
                if (priceNode != null) break;
            }
            var priceText = priceNode?.InnerText.Trim();


            // — 3) Özellik/Description: önce eski liste, yoksa tablo
            var desc = new List<string>();
            // 3a) eski sitedeki <ul> li’leri
            var oldDescNodes = doc.DocumentNode.SelectNodes("//ul[contains(@class,'features')]/li");
            if (oldDescNodes != null && oldDescNodes.Any())
            {
                foreach (var node in oldDescNodes)
                {
                    // raw InnerText: "Ürün Kodu:\r\n        GU036382"
                    var raw = node.InnerText;

                    // 1) \r, \n, \t vs hepsini yakala
                    // 2) birden çok boşluğu tek “ ” yap
                    // 3) baştaki / sondaki boşlukları kırp
                    var clean = Regex
                        .Replace(raw, @"\s+", " ")
                        .Trim();

                    // sonucu: "Ürün Kodu: GU036382"
                    desc.Add(clean);
                }
            }
            else
            {
                // 3b) yeni sitedeki tablo yapısı
                var tableNode = doc.DocumentNode
                    .SelectSingleNode("//*[@id='divTabOzellikler']/div/table[1]/tbody");
                if (tableNode != null)
                {
                    var rows = tableNode.SelectNodes(".//tr");
                    if (rows != null)
                    {
                        foreach (var row in rows)
                        {
                            var cells = row.SelectNodes(".//td");
                            if (cells?.Count >= 2)
                            {
                                var key = cells[0].InnerText.Trim();
                                var value = cells[1].InnerText.Trim();
                                desc.Add($"{key}: {value}");
                            }
                        }
                    }
                }
            }

            //// Örnek: fiyatı <span class="price"> etiketi içinden çek
            //var priceNode = doc.DocumentNode
            //    .SelectSingleNode("//span[contains(@class,'price')]//*[@id='fiyat2']/span[1]");
            //var priceText = priceNode?.InnerText.Trim();

            //// Örnek: özellikleri <ul class="features"><li>…</li></ul> içinden al
            //var featureNodes = doc.DocumentNode
            //    .SelectNodes("//ul[contains(@class,'features')]/li");
            //var features = featureNodes?
            //    .Select(n => n.InnerText.Trim())
            //    .ToList() ?? new List<string>();

            return new ProductScrapeResult
            {
                Title = titleText,
                Price = priceText,
                Description = desc
            };
        }
    }
}
