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

            foreach (var request in CGlobalManager.GlobalRequestList){
                List<CRoute> routes =                              //linq that takes all routes for this request
                    CGlobalManager.GlobalRoutesList.Where(
                        x => x.NodeBegin == request.StartNode && x.NodeFinish == request.EndNode).ToList();
               Random rnd = new Random();
               int random = rnd.Next(routes.Count() - 1); 
               CRoute randomRoute = routes.ElementAt(random); //we take random route
               // here we have draw random channels/slots??
               RoutesCollection.Add(randomRoute);
            }
            return this;
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
