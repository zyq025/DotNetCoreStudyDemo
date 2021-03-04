using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AopConsoleDemo
{
    public class MyProxy : DispatchProxy
    {
        //具体类型
        public object TargetClass { get; set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine("增加用户前执行业务");

            //调用原有方法
            targetMethod.Invoke(TargetClass, args);

            Console.WriteLine("增加用户后执行业务");
            return true;
        }
    }
}
