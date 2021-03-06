﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_new.Entities
{
   public class Solution
   {
       public List<CRoute> RoutesCollection = new List<CRoute>();
       private int _cost = 0;

       public Solution(){}
       public Solution(Solution source){
           this.RoutesCollection =  new List<CRoute>(); 
           foreach (var route in source.RoutesCollection)
           {
               RoutesCollection.Add(new CRoute(route));
           }
           this._cost = source.GetCost();
       }

    
        public Solution GetRandomSolution(){
            foreach (var request in CGlobalManager.GlobalRequestList)
            {
               CRoute randomRoute = GetRandomRouteForRequest(request,-1);
               RoutesCollection.Add(randomRoute);
            }
            return this;
        }

       public static CRoute GetRandomRouteForRequest(CRequest request, int index)  {
           List<CRoute> routes =
               CGlobalManager.GlobalRoutesList.Where(
                   x => x.NodeBegin == request.StartNode && x.NodeFinish == request.EndNode).ToList();  //linq that takes all routes for this request
       
           Random rnd = new Random();
           int random = rnd.Next(routes.Count() - 1);
           CRoute randomRoute = routes.ElementAt(random); //we take random route 
           if (randomRoute.Index == index) //if index == previous route then find new route
           {
               routes.Remove(randomRoute);
               random = rnd.Next(routes.Count() - 1);
               randomRoute = routes.ElementAt(random);   
           }
          
           var slotsQuantity = randomRoute.DemandSlotMapping[request.Size]; //we are looking for slots quantity for this request size
           while (!randomRoute.TryAlocateSlots(slotsQuantity, request.Id)) //When there is no free slots we have to find new random route TODO: if no route will be found propably stack overflow
           {
           routes.Remove(randomRoute);
           if (!routes.Any()) //if there is no more routes then return null (that means it cannot allocate this request
            {
            return null;
            }
           random = rnd.Next(routes.Count() - 1);
           randomRoute = routes.ElementAt(random); //we take random route 
           slotsQuantity = randomRoute.DemandSlotMapping[request.Size];
           }
            return randomRoute;
        }


        public int GetCost(){
        _cost = 0;
        if(!RoutesCollection.Any())
                throw  new Exception("Can't get _cost of empty solution!");
        foreach (var route in RoutesCollection)
        {
            _cost += route.Distance*route.TakenSlotsCount;
        }
          return _cost;
       }

       public void PrintSolution(int solutionId)
       {
          string  result = "";
          result+= "Solution {" +solutionId + "} ";
          result += "Cost: {" + _cost + "} \n";
            foreach (var route in RoutesCollection)
           {
               foreach (var requestAndSlots in route.TakenSlotsArrayForRequest)
               {
                 result += " Request: {" + requestAndSlots.Key + "} ";
                 result += " Route: {" + route.Index + "} ";
                   string slots = "";
                    foreach (var slot in requestAndSlots.Value)
                    {
                        slots += slot + " ";
                    }
                    result += " TakenSlots: {" + slots + "} \n";
                }
           }
           Console.WriteLine(result);
       }
    }
}
