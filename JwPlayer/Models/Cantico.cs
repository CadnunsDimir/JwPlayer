using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwPlayer.Models
{

    public class Cantico
    {
        public int Numero { get; set; }
        public string Titulo { get; set; }
        public CanticoVersao Versao { get; set; }


        public string LinkToStream { get; set; }

        public string LinkToDownload { get; set; }
    }
    public class MP3
    {       

        public string title { get; set; }
        public File file { get; set; }
        public int filesize { get; set; }
        public TrackImage trackImage { get; set; }
        public object markers { get; set; }
        public string label { get; set; }
        public int track { get; set; }
        public int docid { get; set; }
        public int booknum { get; set; }
        public string mimetype { get; set; }
        public string edition { get; set; }
        public string editionDescr { get; set; }
        public string format { get; set; }
        public string formatDescr { get; set; }
        public string specialty { get; set; }
        public string specialtyDescr { get; set; }
        public bool subtitled { get; set; }
        public int frameWidth { get; set; }
        public int frameHeight { get; set; }
        public int frameRate { get; set; }
        public double duration { get; set; }
        public double bitRate { get; set; }
    }
    public class TrackImage
    {
        public string url { get; set; }
        public string modifiedDatetime { get; set; }
        public object checksum { get; set; }
    }
    public class File
    {
        public string url { get; set; }
        public string stream { get; set; }
        public string modifiedDatetime { get; set; }
        public string checksum { get; set; }
    }
    public class RootObject
    {
        public string pubName { get; set; }
        public string parentPubName { get; set; }
        public object booknum { get; set; }
        public string pub { get; set; }
        public string issue { get; set; }
        public string formattedDate { get; set; }
        public List<string> fileformat { get; set; }
        public object track { get; set; }
        public string specialty { get; set; }
        public PubImage pubImage { get; set; }
        public Languages languages { get; set; }
        public Files files { get; set; }
    }
    public class PubImage
    {
        public string url { get; set; }
        public string modifiedDatetime { get; set; }
        public string checksum { get; set; }
    }
    public class S
    {
        public string name { get; set; }
        public string direction { get; set; }
        public string locale { get; set; }
    }

    public class Languages
    {
        public S S { get; set; }
    }
    public class Files
    {
        public S2 S { get; set; }
    }

    public class S2
    {
        public List<MP3> MP3 { get; set; }
    }
}
