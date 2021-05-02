using MercadoBitcoin.Service.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MercadoBitcoin.Test.Builders
{
    public class TradesResponseBuilder
    {
        private List<Trades> _trades;

        public TradesResponseBuilder()
        {
            _trades = new List<Trades>
            {
                new Trades
                {
                    Date = 1502993741,
                    Price = 14086,
                    Amount = 0.0384,
                    Tid = 797861,
                    Type = "sell"
                },
                 new Trades
                {
                    Date = 1502993741,
                    Price = 14086,
                    Amount = 0.08519000,
                    Tid = 797860,
                    Type = "sell"
                },
                new Trades
                {
                    Date = 1502993907,
                    Price = 14200.00000000,
                    Amount = 0.01370282,
                    Tid = 797871,
                    Type = "buy"
                },
                 new Trades
                {
                    Date = 1502993898,
                    Price = 14200.00000000,
                    Amount = 0.00697183,
                    Tid = 797870,
                    Type = "buy"
                },
                 new Trades
                {
                    Date = 1502982896,
                    Price = 14320.00101000,
                    Amount = 0.02089000,
                    Tid = 796876,
                    Type = "sell"
                },
            };
        }

        public TradesResponseBuilder FilterByTid(double tid)
        {
            this._trades = _trades.Where(x => x.Tid > tid).ToList();

            return this;
        }

        public TradesResponseBuilder FilterByFrom()
        {
            this._trades = _trades.Where(x => x.Date >= 1502993907).ToList();

            return this;
        }

        public TradesResponseBuilder FilterByFromAndTo()
        {
            this._trades = _trades.Where(x => x.Date >= 1502993907 && x.Date <= 1502993741).ToList();

            return this;
        }

        public List<Trades> Build()
        {
            return _trades;
        }
    }
}
