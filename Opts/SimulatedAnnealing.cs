﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opts
{
    public class SimulatedAnnealing
    {
        private Random random = new Random();
        private double t;
        private int u;
        private int q;
        private List<int> initialSolution;
        private int[,] dij;

        public SimulatedAnnealing(double initialTemperature, int maxIterations, int temperatureChange, 
            List<int> initialSolution, int[,] dij)
        {
            this.t = initialTemperature;
            this.u = maxIterations;
            this.q = temperatureChange;
            this.initialSolution = initialSolution;
            this.dij = dij;
        }

        public List<int> FindMinimum()
        {
            var currentSolution = this.initialSolution;
            var bestSolution = this.initialSolution;
            var r = 0;
            var w = 0;

            while(r != this.u)
            {
                var newSolution = GenerateRandomNeighbor(currentSolution);
                r++;
                w++;

                //Temperature change
                if (w == q)
                {
                    t = t / 2;
                    w = 0;
                }

                var newSolutionLength = TravelingSalesman.GetLength(this.dij, newSolution);
                var currentSolutionLength = TravelingSalesman.GetLength(this.dij, currentSolution);
                var delta = newSolutionLength - currentSolutionLength;                    

                if (delta <= 0)
                {
                    currentSolution = newSolution;
                    r = 0;
                    if (TravelingSalesman.GetLength(this.dij, newSolution) <=
                        TravelingSalesman.GetLength(this.dij, bestSolution))
                    {
                        bestSolution = newSolution;
                    }
                }
                else
                {
                    var probability = Math.Exp(-delta / t);
                    if (random.NextDouble() < probability)
                    {
                        currentSolution = newSolution;
                        r = 0;
                    }
                }

            }

            return bestSolution;
        }

        private List<int> GenerateRandomNeighbor(List<int> solution)
        {
            List<int> newSolution = null;
            while(newSolution == null)
            {
                var firstIndex = random.Next(1, solution.Count - 4);
                var secondIndex = random.Next(1, solution.Count - 4);
                if(Math.Abs(firstIndex - secondIndex) > 3)
                {
                    newSolution = new List<int>(solution);
                    var firstRange = newSolution.GetRange(firstIndex, 4);
                    var secondRange = newSolution.GetRange(secondIndex, 4);

                    newSolution.RemoveRange(firstIndex, 4);
                    newSolution.InsertRange(firstIndex, secondRange);
                    newSolution.RemoveRange(secondIndex, 4);
                    newSolution.InsertRange(secondIndex, firstRange);
                }
            }

            return newSolution;
        }
    }
}
