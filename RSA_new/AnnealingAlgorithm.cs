﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSA_new.Entities;

namespace RSA_new
{
   public class AnnealingAlgorithm
   {
       private double _startTemperature;
       private readonly double _endTemperature;
       private readonly double _annealingParameter;
       private int _counter = 0;

      //Constructor initializes the algorithms parameters
       public AnnealingAlgorithm(double startTemperature = 1000, double endTemperature = 10, double annealingParameter = 0.99){
           _startTemperature = startTemperature;
           _endTemperature = endTemperature;
           _annealingParameter = annealingParameter;
       }

       //Method that will calculate our solution
       public Solution CalculateSolution(){  
            Solution sol = new Solution();
            sol.GetRandomSolution();  //creating start solution
            sol.PrintSolution(_counter);
            Solution bestSol = new Solution(sol);   //start solution is automaticaly best solution
            Solution _sol = new Solution(sol);      //next solution

            int costSol, cost_Sol, costBestSol; //cost of current solution, cost of next solution, cost of best solution
            costSol = sol.GetCost();
            costBestSol = costSol;

            while (_startTemperature > _endTemperature) {   //Annealing algorithm starts iterate here
                _sol = MakeMutation(sol);       // we take next random solution
                cost_Sol = _sol.GetCost();      // taking the cost of next solution
                if (cost_Sol < costBestSol){     //checking if the new solution is better than old one
                     bestSol = new Solution(_sol);
                     costBestSol = cost_Sol;
                }
                #region algorithm calculation ...
                double delta = cost_Sol - costSol;
                if (delta < 0){
                    sol = new Solution(_sol);
                    costSol = cost_Sol;
                }
                else{
                    Random rnd = new Random();
                    double x = (rnd.Next(10000)) / 10000.0;  // random number <0, 1)
                    if (x < Math.Exp(((-delta) / _startTemperature))){
                        sol = new Solution(_sol);
                        costSol = cost_Sol;
                    }
                }
#endregion
                _startTemperature *= _annealingParameter; // we decrease the start temperature by the annealingParameter
            } 
            return bestSol;
        }

       //Method that tries to take first request from random route and tries to relocate it to other route ( if allocation is impossible then returning same solution)
       private Solution MakeMutation(Solution sol){
          _counter++;
          Random rnd = new Random();
          int random = rnd.Next(sol.RoutesCollection.Count - 1); //find random route
          CRoute randomRoute = sol.RoutesCollection.ElementAt(random);
          var requestAndSlots = randomRoute.TakenSlotsArrayForRequest.FirstOrDefault(); //TODO: For now its not random just firstOrDefault for better optimization it should be random
           while (requestAndSlots.Value == null) //if there is no request alocated find other route with allocated request
           {
               random = rnd.Next(sol.RoutesCollection.Count - 1); 
               randomRoute = sol.RoutesCollection.ElementAt(random);
               requestAndSlots = randomRoute.TakenSlotsArrayForRequest.FirstOrDefault();
           }

          CRequest req = CGlobalManager.GlobalRequestList.FirstOrDefault(x => x.Id == requestAndSlots.Key);// we are looking for this request and one more time randomly allocating somewhere
          CRoute newRoute = Solution.GetRandomRouteForRequest(req,randomRoute.Index); //Trying to find new route for this request
          if (newRoute == null){return sol;} //if the new route is null it means that the slot allocation for other route was impossible
          randomRoute.FreeSlots(requestAndSlots); //Free the slots on the previous route
          if (!randomRoute.TakenSlotsArrayForRequest.Any()) //If the previous route hasn't any other request then delete it from solution
          {
                sol.RoutesCollection.Remove(randomRoute);
          }
          sol.RoutesCollection.Add(newRoute);
          sol.PrintSolution(_counter);
          return sol;
       }
   }
}
