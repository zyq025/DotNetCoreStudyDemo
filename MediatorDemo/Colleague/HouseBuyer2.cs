using MediatorDemo.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorDemo.Colleague
{
    public class HouseBuyer2 : People
    {
        public HouseBuyer2(HouseMediator mediator) : base(mediator)
        { }

        public void GetHouseMsg(string msg)
        {
            Console.WriteLine($"买房者2获得房源消息:{msg}");
        }
    }
}
