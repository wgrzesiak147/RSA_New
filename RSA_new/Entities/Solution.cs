﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_new.Entities
{
   public class Solution
   {
       public List<CRoute> RoutesCollection;
       private int cost = 0;
        
        public Solution GetRandomSolution(){

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
