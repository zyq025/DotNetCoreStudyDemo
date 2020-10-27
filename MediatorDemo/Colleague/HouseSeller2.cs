using MediatorDemo.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorDemo.Colleague
{
    /// <summary>
    /// 具体的卖房者2
    /// </summary>
    public class HouseSeller2 : People
    {
        public HouseSeller2(HouseMediator mediator):base(mediator)
        { }

        public void GetBuyHouseMsg(string msg)
        {
            Console.WriteLine($"卖房者2获得需要买房的消息:{msg}");
        }
    }
}
