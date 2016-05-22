using System.IO;
using System.Net;
using NAudio.Wave;
using System.Threading;

namespace JwPlayer.Service
{
    public class Mp3Player
    {
        //private MediaPlayer _mediaPlayer;
        private Thread _processo;
        private WaveOut _waveOut;
        public Mp3Player()
        {
            //_mediaPlayer = new MediaPlayer();
        }

        public void SetStream(string link, bool playOnLoad = true)
        {            
            StopIfPlaying();
            _processo = new Thread(StreamMusic);
            _processo.Start(link);
        }

        private void StreamMusic(object link)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(link.ToString())
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {

                    _waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
                    _waveOut.Init(blockAlignedStream);
                    _waveOut.Play();
                    while (_waveOut.PlaybackState != PlaybackState.Stopped)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }

        public void StopIfPlaying()
        {
            if (_processo != null)
            {
                //Monitor.Exit(_processo);
                _processo.Abort();
            }
        }

        public void Pause()
        {
            if (_waveOut != null && _waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Pause();
            }
        }

        public void Resume()
        {
            if (_waveOut != null && _waveOut.PlaybackState == PlaybackState.Paused)
            {
                _waveOut.Resume();
            }
        }
    }
}
