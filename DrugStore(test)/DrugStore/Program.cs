using System;

namespace DrugStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Store ds = new Store("../datasets");
            ds.ReadFromDataset();
            System.Console.WriteLine(GC.GetTotalMemory(true));
            Console.ReadLine();
        }
    }
}
