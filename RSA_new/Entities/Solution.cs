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
               CRoute randomRoute = GetRandomValuesForRequest(request);
               RoutesCollection.Add(randomRoute);
            }
            return this;
        }

       public static CRoute GetRandomValuesForRequest(CRequest request)
       {
           List<CRoute> routes =
               CGlobalManager.GlobalRoutesList.Where(
                   x => x.NodeBegin == request.StartNode && x.NodeFinish == request.EndNode).ToList();
               //linq that takes all routes for this request
           Random rnd = new Random();
           int random = rnd.Next(routes.Count() - 1);
           CRoute randomRoute = routes.ElementAt(random); //we take random route 
           return randomRoute;
            // here we have draw random channels/slots?? we have to delete this route flor CGlobal //TODO : blocked untill slots solution will be solved
        }


        public int GetCost(){
        if(!RoutesCollection.Any())
                throw  new Exception("Can't get cost of empty solution!");
        foreach (var route in RoutesCollection)
        {
            cost += route.Distance*route.TakenSlotsCount;
        }
          return cost;
       }
    }
}
