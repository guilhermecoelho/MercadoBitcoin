using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MercadoBitcoin.Test.Builders
{
    public class HttpResponseMessageBuilder
    {
        private HttpResponseMessage _httpResponseMessage;

        public HttpResponseMessageBuilder()
        {
            this._httpResponseMessage = new HttpResponseMessage();
        }

        public HttpResponseMessageBuilder StatusOk_ticker()
        {
            this._httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""ticker"": {""high"":""318365.97366000"",""low"":""300000.00000000"",""vol"":""161.17266663"",""last"":""318365.97364000"",""buy"":""318365.97000000"",""sell"":""318365.97364000"",""open"":""306499.99999000"",""date"":1616588277}}"),
            };

            return this;
        }

        public HttpResponseMessageBuilder StatusOk_Orderbook()
        {
            this._httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""asks"": [[10410.00006000, 2.09190016],[10420.00000000, 0.00997000],[10488.99999000, 0.46634897]],""bids"": [[10405.38258000, 0.00181000],[10393.84180000, 0.08387000]]}"),
            };

            return this;
        }

        public HttpResponseMessageBuilder StatusOk_Trades()
        {
            this._httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[
                    {
                        'date': 1502993741,
                        'price': 14086.00000000,
                        'amount': 0.03840000,
                        'tid': 797861,
                        'type': 'sell'
                    },
                    {
                        'date': 1502993741,
                        'price': 14086.00101000,
                        'amount': 0.08519000,
                        'tid': 797860,
                        'type': 'sell'
                    },
                    {
                        'date': 1502993907,
                        'price': 14200.00000000,
                        'amount': 0.01370282,
                        'tid': 797871,
                        'type': 'buy'
                    },
                    {
                        'date': 1502993898,
                        'price': 14200.00000000,
                        'amount': 0.00697183,
                        'tid': 797870,
                        'type': 'buy'
                    },
                    {
                        'date': 1502982896,
                        'price': 14320.00101000,
                        'amount': 0.02089000,
                        'tid': 796876,
                        'type': 'sell'
                    }
                ]"),
            };

            return this;
        }

        public HttpResponseMessageBuilder StatusOk_DaySummary()
        {
            this._httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{
                    'date': '2013-06-20',
                    'opening': 262.99999,
                    'closing': 269.0,
                    'lowest': 260.00002,
                    'highest': 269.0,
                    'volume': 7253.1336356785,
                    'quantity': 27.11390588,
                    'amount': 28,
                    'avg_price': 267.5060416518087
                }"),
            };

            return this;
        }

        public HttpResponseMessageBuilder StatusOkEmpty()
        {
            _httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty),
            };

            return this;
        }

        public HttpResponseMessage Build()
        {
            return this._httpResponseMessage;
        }
    }
}
