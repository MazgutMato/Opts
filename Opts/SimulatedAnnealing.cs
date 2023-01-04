using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opts
{
    public class SimulatedAnnealing
    {
        private Random _random = new Random();
        private double t;
        private int u;
        private int q;
        private List<int> initialSolution;

        public SimulatedAnnealing(double initialTemperature, int maxIterations, int temperatureChange, List<int> initialSolution)
        {
            this.t = initialTemperature;
            this.u = maxIterations;
            this.q = temperatureChange;
            this.initialSolution = initialSolution;
        }

        public List<int> FindMinimum()
        {
            var currentSolution = this.initialSolution;
            var bestSolution = this.initialSolution;

            for (int i = 0; i < u; i++)
            {
                var newSolution = GenerateRandomNeighbor(currentSolution);

                // Calculate the difference in cost between the new solution and the current solution
                var delta = newSolution.Cost - currentSolution.Cost;

                // If the new solution is better than the current solution, accept it
                if (delta < 0)
                {
                    currentSolution = newSolution;
                    if (newSolution.Cost < bestSolution.Cost)
                    {
                        bestSolution = newSolution;
                    }
                }
                // If the new solution is worse than the current solution, accept it with probability exp(-delta/T)
                else
                {
                    var probability = Math.Exp(-delta / _initialTemperature);
                    if (_random.NextDouble() < probability)
                    {
                        currentSolution = newSolution;
                    }
                }

                // Cool down the temperature
                _initialTemperature *= 0.9;

                // If the temperature has reached the minimum temperature, stop
                if (_initialTemperature < _minTemperature)
                {
                    break;
                }
            }

            return bestSolution;
        }

        private List<int> GenerateRandomNeighbor(List<int> solution)
        {
            List<int> newSolution = null;
            while(newSolution == null)
            {
                var firstIndex = _random.Next(1, solution.Count - 4);
                var secondIndex = _random.Next(1, solution.Count - 4);
                if(Math.Abs(firstIndex - secondIndex) > 3)
                {
                    var firstRange = solution.GetRange(firstIndex, 4);
                    solution.RemoveRange(firstIndex, 4);
                    var secondRange = solution.GetRange(secondIndex, 4);
                    solution.RemoveRange(secondIndex, 4);

                    solution.InsertRange(firstIndex, secondRange);
                    solution.InsertRange(secondIndex, firstRange);
                    newSolution = solution;
                }
            }

            return newSolution;
        }
    }
}
