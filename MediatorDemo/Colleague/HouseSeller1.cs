using MediatorDemo.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorDemo.Colleague
{
    /// <summary>
    /// 具体的卖房者1
    /// </summary>
    public class HouseSeller1 : People
    {
        public HouseSeller1(HouseMediator mediator)
            :base(mediator)
        { }

        public void GetBuyHouseMsg(string msg)
        {
            Console.WriteLine($"卖房者1获得需要买房的消息:{msg}");
        }
    }
}
