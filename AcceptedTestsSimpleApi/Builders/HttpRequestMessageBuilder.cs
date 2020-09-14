using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AcceptedTestsSimpleApi.Builders
{
    public class HttpRequestMessageBuilder
    {
        private string _url;
        private string _body;
        private HttpMethod _method;
        private Encoding _encoding = Encoding.UTF8;
        private string _mediaType = "application/json";

        public HttpRequestMessageBuilder ComUrl(string url)
        {
            _url = url;

            return this;
        }

        public HttpRequestMessageBuilder ComMethod(HttpMethod method)
        {
            _method = method;

            return this;
        }

        public HttpRequestMessageBuilder ComEncoding(Encoding encoding)
        {
            _encoding = encoding;

            return this;
        }

        public HttpRequestMessageBuilder ComMediaType(string mediaType)
        {
            _mediaType = mediaType;

            return this;
        }

        public HttpRequestMessageBuilder ComBody<T>(T body)
        {
            _body = JsonConvert.SerializeObject(body);

            return this;
        }

        public HttpRequestMessage Instanciar() => new HttpRequestMessage(_method, _url)
        {
            Content = new StringContent(_body, _encoding, _mediaType)
        };
    }
}
