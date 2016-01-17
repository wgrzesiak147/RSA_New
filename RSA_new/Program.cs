using System;
using System.Collections.Generic;
using RSA_new.Entities;

namespace RSA_new {
    class Program {
        static void Main(string[] args) {
            CGlobalManager _manager = new CGlobalManager();
            CGlobalManager.LoadLinks(@"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\dd.net");
            CGlobalManager.LoadRoutes(@"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\d.pat");
            CGlobalManager.LoadRequests(@"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\51.dem");
            CGlobalManager.LoadRouteSlotMappings(@"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\d1.spec");
            AnnealingAlgorithm alg = new AnnealingAlgorithm();
            Solution sol =  alg.CalculateSolution();
            //CRoute newRoute = new CRoute(newList);
            //foreach (var node in newRoute.ReturnPassedNodes()) {
            //    Console.WriteLine(node);
            //}
        }
    }
}
