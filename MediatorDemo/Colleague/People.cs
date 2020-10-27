using MediatorDemo.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorDemo.Colleague
{
    public abstract class People
    {
        protected HouseMediator _mediator;

        public People(HouseMediator mediator)
        {
            this._mediator = mediator;
        }

        public  void SendMsg(string msg)
        {
            _mediator.SendHouseMsg(msg, this);
        }

    }
}
