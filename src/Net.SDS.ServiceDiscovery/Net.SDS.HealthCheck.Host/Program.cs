using System;
using System.Threading;
using System.Threading.Tasks;

namespace Net.SDS.HealthCheck.Host
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            while (true)
            {
                Console.WriteLine("check");
                Thread.Sleep(1000);
            }
        }
    }
}
