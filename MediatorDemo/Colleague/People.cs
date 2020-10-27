using MediatorDemo.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorDemo.Colleague
{
    /// <summary>
    /// 抽象化人，买房和卖房者都是人
    /// </summary>
    public abstract class People
    {
        //和中介者关联
        protected HouseMediator _mediator;

        public People(HouseMediator mediator)
        {
            this._mediator = mediator;
        }

        // 都可以发送消息，卖房者发布房源，买房者发布购房意愿
        public  void SendMsg(string msg)
        {
            _mediator.SendHouseMsg(msg, this);
        }

    }
}
