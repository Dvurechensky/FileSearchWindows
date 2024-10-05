using System.Text;

namespace FileSearch.Logic.Model.EncodingDetection
{
    internal interface IEncodingFactory
    {
        Encoding[] DetectEncoding(byte[] firstBytes);
    }
}
