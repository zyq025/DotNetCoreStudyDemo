using MediatorDemo.Colleague;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorDemo.Mediator
{
    public abstract class HouseMediator
    {
        public abstract void SendHouseMsg(string msg,People people);
    }
}
