using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwPlayer.Models;
using RestSharp;
using JwPlayer.Utils;
namespace JwPlayer
{
    class Program
    {
        private static string host = "https://www.jw.org";
        static void Main(string[] args)
        {
            var link = @"/apps/TRGCHlZRQVNYVrXF?output=json&pub=iasn&fileformat=MP3%2CAAC&alllangs=0&langwritten=S&txtCMSLang=S";

            var client = new RestClient();
            client.BaseUrl = new Uri(host);
            var response = client.Execute<RootObject>(new RestRequest(link));
            var canticos = response.Data.files.S.MP3.Where(x=>x.mimetype == "audio/mpeg").Select(x => new Cantico
            {
                Numero = x.title.Split('-')[0].ToInt(),
                Titulo = x.title.Split('-')[1].Substring(2),
                Versao = CanticoVersao.Original,
                LinkToStream = x.file.stream,
                LinkToDownload = x.file.url
            }).ToList();
            //var html = new HtmlDocument();
            //html.Load("page.html");
            //var musicas = html.DocumentNode.SelectNodes("//li[@class='itemRow']").Where(x => x.InnerHtml.Contains("playBtn") && x.InnerHtml.Contains("trackTitle")).Select(s => new Cantico
            //{
            //    Numero = int.Parse( s.SelectSingleNode("//div[@class='trackTitle']//a").InnerText.Split('-')[0]),
            //    Titulo = s.SelectSingleNode("//div[@class='trackTitle']//a").InnerText.Split('-')[1],
            //    Versao = CanticoVersao.Original
            //}).ToList() ;
        }
    }

    
}
