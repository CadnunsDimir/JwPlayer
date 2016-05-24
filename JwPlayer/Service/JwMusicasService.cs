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
        //TRGCHlZRQVNYVrXF?output=json&pub=iasnm&fileformat=MP3%2CAAC&alllangs=0&langwritten=S&txtCMSLang=S
        public List<Cantico> GetCanticos(string linkINformado  =null, CanticoVersao versao = CanticoVersao.Original)
        {
            var link = linkINformado ?? @"/apps/TRGCHlZRQVNYVrXF?output=json&pub=iasn&fileformat=MP3%2CAAC&alllangs=0&langwritten=S&txtCMSLang=S";
            var response = _client.Execute<RootObject>(new RestRequest(link));
            var canticos = response.Data.files.S.MP3.Where(x => x.mimetype == "audio/mpeg").Select(x => new Cantico
            {
                Numero = x.title.Split('-').Length > 1 ? x.title.Split('-')[0].ToInt() : x.title.Split('_')[0].ToInt(),
                Titulo = x.title.Split('-').Length > 1 ? x.title.Split('-')[1].Substring(2) : x.title.Split('_')[1].Substring(0),
                Versao =versao,
                LinkToStream = x.file.stream,
                LinkToDownload = x.file.url
            }).ToList();

            return canticos;
        }

        public List<Cantico> GetTodosCanticos()
        {
            var canticos = GetCanticos();
            var novosCanticos = @"/apps/TRGCHlZRQVNYVrXF?output=json&pub=iasnm&fileformat=MP3%2CAAC&alllangs=0&langwritten=S&txtCMSLang=S";
            canticos.AddRange(GetCanticos(novosCanticos, CanticoVersao.ArranjoOrquertralNovo));
            var canticosForaDoCantico = @"/apps/TRGCHlZRQVNYVrXF?output=json&pub=snnw&fileformat=MP3%2CAAC&alllangs=0&langwritten=S&txtCMSLang=S";
            canticos.AddRange(GetCanticos(canticosForaDoCantico, CanticoVersao.CancoesNovas));
            return canticos.GroupBy(x => x.Numero).Select(x =>
                x.FirstOrDefault(y => y.Versao == CanticoVersao.ArranjoOrquertralNovo) ??
                x.FirstOrDefault(y => y.Versao == CanticoVersao.CancoesNovas) ??
                x.FirstOrDefault()).ToList();
        }
    }
}
