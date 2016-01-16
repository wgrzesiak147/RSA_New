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
               //List<CRoute> routes = CGlobalManager.GlobalRoutesList
            }
            return new Solution();
        }

       
       public int GetCost(){
        foreach (var route in RoutesCollection)
        {
            cost += route.Distance*route.TakenSlotsCount;
        }
          return cost;
       }
    }
}
