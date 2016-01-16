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
            Solution bestSol = sol;   //start solution is automaticaly best solution
            Solution _sol = sol;      //next solution

            int costSol, cost_Sol, costBestSol; //cost of current solution, cost of next solution, cost of best solution
            costSol = sol.GetCost();
            costBestSol = costSol;

            while (_startTemperature > _endTemperature) {   //Annealing algorithm starts iterate here
                _sol = MakeMutation(sol);       // we take next random solution
                cost_Sol = _sol.GetCost();      // taking the cost of next solution
                if (cost_Sol < costBestSol) {     //checking if the new solution is better than old one
                     bestSol = _sol;
                    costBestSol = cost_Sol;
                }
                #region algorithm calculation ...
                double delta = cost_Sol - costSol;
                if (delta < 0){
                    sol = _sol;
                    costSol = cost_Sol;
                }
                else{
                    Random rnd = new Random();
                    double x = (rnd.Next(10000)) / 10000.0;  // random number <0, 1)
                    if (x < Math.Exp(((-delta) / _startTemperature))){
                        sol = _sol;
                        costSol = cost_Sol;
                    }
                }
#endregion
                _startTemperature *= _annealingParameter; // we decrease the start temperature by the annealingParameter
            } 
            return bestSol;
        }

       private Solution MakeMutation(Solution sol){
          Random rnd = new Random();
          int random = rnd.Next(sol.RoutesCollection.Count - 1);
          int startNode = sol.RoutesCollection.ElementAt(random).NodeBegin;
          int endNode = sol.RoutesCollection.ElementAt(random).NodeFinish;
         // Solution.GetRandomValuesForRequest(new CRequest()) //TODO : blocked untill slots solution will be solved
          return sol;
       }
   }
}
