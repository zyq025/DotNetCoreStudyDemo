using MediatorDemo.Colleague;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorDemo.Mediator
{
    /// <summary>
    /// 抽象化房屋中介，因为中介有很多家，如安居客、链家等
    /// </summary>
    public abstract class HouseMediator
    {
        public abstract void SendHouseMsg(string msg,People people);
    }
}
