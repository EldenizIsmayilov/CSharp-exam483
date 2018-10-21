using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LearnThreading
{
    public static class Program
    {

        public static void Main()
        {
            //UnderstandThread();

            //BackgroundThread();

            //ParameterizedThread();

            //GlobalVariable();

            //ThreadStaticAttribute();

            LocalVariable();
        }

        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(1000);
            }
        }

        public static void ThreadMethod(object param)
        {
            for (int i = 0; i < (int)param; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(1000);
            }
        }

        private static void UnderstandThread()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(0);
            }
            t.Join();

            Console.ReadKey();
        }

        private static void BackgroundThread()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.IsBackground = true;
            t.Start();
        }

        private static void ParameterizedThread()
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));
            t.Start(5);
            t.Join();

            Console.ReadKey();
        }

        private static void GlobalVariable()
        {
            bool stopped = false;
            Thread t = new Thread(new ThreadStart(() =>
            {
                while (!stopped)
                {
                    Console.WriteLine("Running...");
                    Thread.Sleep(1000);
                }
            }));
            t.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            stopped = true;
            t.Join();
        }


        [ThreadStatic]
        public static int _field;
        private static void ThreadStaticAttribute()
        {
            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    _field++;
                    Console.WriteLine("Thread A: {0}", _field);
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    _field++;
                    Console.WriteLine("Thread B: {0}", _field);
                }
            }).Start();
            Console.ReadKey();

        }


        public static ThreadLocal<int> _localField =
            new ThreadLocal<int>(() =>
            {
                return Thread.CurrentThread.ManagedThreadId;
            });
        public static void LocalVariable()
        {
            new Thread(() =>
                {
                    for (int x = 0; x < _localField.Value; x++)
                    {
                        Console.WriteLine("Thread A: {0}", x);
                    }
                }).Start();
            new Thread(() =>
                {
                    for (int x = 0; x < _localField.Value; x++)
                    {
                        Console.WriteLine("Thread B: {0}", x);
                    }
                }).Start();

            Console.ReadKey();
        }
    }
}
