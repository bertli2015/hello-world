using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDemo
{

    public interface IMyInterface
    {
        void MethodB();
    }
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extension
    {
        public static void MethodA(this IMyInterface myInterface, int i)
        {
            Console.WriteLine("Extension.MethodA(this IMyInterface myInterface, int i)");
        }
        public static void MethodA(this IMyInterface myInterface, string s)
        {
            Console.WriteLine("Extension.MethodA(this IMyInterface myInterface, string s)");
        }
        public static void MethodB(this IMyInterface myInterface)
        {
            Console.WriteLine("Extension.MethodB(this IMyInterface myInterface)");
        }
    }

    class A : IMyInterface
    {
        public void MethodB() { Console.WriteLine("A.MethodB()"); }
    }
    class B : IMyInterface
    {
        public void MethodB() { Console.WriteLine("B.MethodB()"); }
        public void MethodA(int i) { Console.WriteLine("B.MethodA(int i)"); }
    }
    class C : IMyInterface
    {
        public void MethodB() { Console.WriteLine("C.MethodB()"); }
        public void MethodA(object obj)
        {
            Console.WriteLine("C.MethodA(object obj)");
        }
    }

    public class ExtensionMethodDemo
    {
        public static void Start()
        {
            A a = new A();
            B b = new B();
            C c = new C();
            a.MethodA(1);           // Extension.MethodA(object, int)
            a.MethodA("hello");     // Extension.MethodA(object, string)
            a.MethodB();            // A.MethodB()
            b.MethodA(1);           // B.MethodA(int)
            b.MethodB();            // B.MethodB()
            b.MethodA("hello");     // Extension.MethodA(object, string)
            c.MethodA(1);           // C.MethodA(object)
            c.MethodA("hello");     // C.MethodA(object)
            c.MethodB();            // C.MethodB()

        }
    }
}
