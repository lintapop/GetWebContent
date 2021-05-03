using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;

namespace GetWebContent
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            string url = "https://bus.cyhg.gov.tw/Forum.aspx?n=4FDDCF85BDD6B371&sms=B9D0D08558E15D4D";
            //string url = "https://dannyliu.me";
            //發送請求
            var responseMessage = await httpClient.GetAsync(url);
            //判斷式檢查回應的伺服器狀態 Status Code是否是200 OK
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseResult = responseMessage.Content.ReadAsStringAsync().Result; //取得內容
                Console.WriteLine(responseResult);

                //AngleSharp前置設定
                var config = Configuration.Default;
                var context = BrowsingContext.New(config);
                //傳入response資料
                //將用httpclient拿到的資料放入res.Content()中
                var document = await context.OpenAsync(res => res.Content(responseResult));
                //使用QuerySelector取得內容
                var head = document.QuerySelector("head"); //用("head")找出<head></head>元素
                Console.WriteLine(head.ToHtml());
                var contents = document.QuerySelectorAll(".entry-content"); //(".entry-content")找出class="entry-content"的所有元素

                foreach (var c in contents)
                {
                    //取得每個元素的TextContent
                    Console.WriteLine(c.TextContent);
                }
            }

            Console.ReadKey();
        }
    }
}