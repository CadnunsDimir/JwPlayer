using JwPlayer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwPlayer.Utils;
namespace JwPlayer.Service
{
    public class JwMusicasService
    {
        private const string host = "https://www.jw.org";
        private RestClient _client;
        public string Host { get { return host; } }
        public JwMusicasService()
        {
            _client = new RestClient();
            _client.BaseUrl = new Uri(host);
        }

        public List<Cantico> GetCanticos()
        {
            var link = @"/apps/TRGCHlZRQVNYVrXF?output=json&pub=iasn&fileformat=MP3%2CAAC&alllangs=0&langwritten=S&txtCMSLang=S";
            var response = _client.Execute<RootObject>(new RestRequest(link));
            var canticos = response.Data.files.S.MP3.Where(x => x.mimetype == "audio/mpeg").Select(x => new Cantico
            {
                Numero = x.title.Split('-')[0].ToInt(),
                Titulo = x.title.Split('-')[1].Substring(2),
                Versao = CanticoVersao.Original,
                LinkToStream = x.file.stream,
                LinkToDownload = x.file.url
            }).ToList();
            return canticos;
        }
    }
}
