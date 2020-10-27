using MediatorDemo.Colleague;
using MediatorDemo.Mediator;
using System;

namespace MediatorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 先有个房屋中介公司
            AnJuKe anjuke = new AnJuKe();

            // 卖房者先找到房屋中介，从那发布房源信息，或获取买房者购房意愿
            HouseSeller1 seller1 = new HouseSeller1(anjuke);
            HouseSeller2 seller2 = new HouseSeller2(anjuke);

            // 买房者找到房屋中介，从那获取房源信息，或是发布购房信息
            HouseBuyer1 buyer1 = new HouseBuyer1(anjuke);
            HouseBuyer2 buyer2 = new HouseBuyer2(anjuke);

            // 房屋中介进行登记
            anjuke.Seller1 = seller1;
            anjuke.Seller2 = seller2;
            anjuke.Buyer1 = buyer1;
            anjuke.Buyer2 = buyer2;

            // 卖房者发布信息，其实是通过中介发布，发布之后所有买房者接受信息
            seller1.SendMsg("sell1精品好房源，速联，秒出");

            // 买房者发布购房意愿信息，其实通过中介发布，发布之后相关人可以看到对应的信息
            buyer1.SendMsg("buyer1想入手两室一厅学区房，采光良好");
        }
    }
}
