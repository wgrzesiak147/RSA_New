using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_new.Entities
{
   public class Solution
   {
       public List<CRoute> RoutesCollection = new List<CRoute>();
       private int cost = 0;
        public Solution GetRandomSolution(){
            foreach (var request in CGlobalManager.GlobalRequestList)
            {
               CRoute randomRoute = GetRandomRouteForRequest(request);
               RoutesCollection.Add(randomRoute);
            }
            return this;
        }

       public static CRoute GetRandomRouteForRequest(CRequest request)  {
           List<CRoute> routes =
               CGlobalManager.GlobalRoutesList.Where(
                   x => x.NodeBegin == request.StartNode && x.NodeFinish == request.EndNode).ToList();  //linq that takes all routes for this request

           Random rnd = new Random();
           int random = rnd.Next(routes.Count() - 1);
           CRoute randomRoute = routes.ElementAt(random); //we take random route 
           var slotsQuantity = randomRoute.DemandSlotMapping[request.Size]; //we are looking for slots quantity for this request size
           while (!randomRoute.TryAlocateSlots(slotsQuantity,request.Id)) //When there is no free slots we have to find new random route TODO: if no route will be found propably stack overflow
           {
              random = rnd.Next(routes.Count() - 1);
              randomRoute = routes.ElementAt(random); //we take random route 
              slotsQuantity = randomRoute.DemandSlotMapping[request.Size];
           }

            return randomRoute;
        }


        public int GetCost(){
        cost = 0;
        if(!RoutesCollection.Any())
                throw  new Exception("Can't get cost of empty solution!");
        foreach (var route in RoutesCollection)
        {
            cost += route.Distance*route.TakenSlotsCount;
        }
          return cost;
       }

       public void PrintSolution(int solutionId)
       {
          string  result = "";
          result+= "Solution {" +solutionId + "} ";
          result += "Cost: {" + cost + "} \n";
            foreach (var route in RoutesCollection)
           {
               foreach (var requestAndSlots in route.TakenSlotsArrayForRequest)
               {
                 result += " Request: {" + requestAndSlots.Key + "} ";
                 result += " Route: {" + route.Index + "} ";
                   string slots = "";
                    foreach (var slot in requestAndSlots.Value)
                    {
                        slots += slot;
                    }
                    result += " TakenSlots: {" + slots + "} \n";
                }
           }
           Console.WriteLine(result);
       }
    }
}
