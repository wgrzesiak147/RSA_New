using System;
using System.Collections.Generic;
using RSA_new.Entities;

namespace RSA_new {
    class Program {
        static void Main(string[] args) {

            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;

            string sTopologyFile = directoryPath + @"Dane\dd.net";
            string sRoutesFile = @"Dane\d.PAT";
            string sSlotsFile = @"Dane\d1.spec";
            string sRequestFile = @"Dane\51.dem";

            CGlobalManager _manager = new CGlobalManager();
            CGlobalManager.LoadLinks(sTopologyFile);
            CGlobalManager.LoadRoutes(sRoutesFile);
            CGlobalManager.LoadRequests(sRequestFile);
            CGlobalManager.LoadRouteSlotMappings(sSlotsFile);

            AnnealingAlgorithm alg = new AnnealingAlgorithm();
            Solution sol =  alg.CalculateSolution();
            Console.WriteLine("---------------------------------------------------------------");
            sol.PrintSolution(99999);
            Console.ReadKey();
        }
    }
}
