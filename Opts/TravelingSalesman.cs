using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opts
{
    public static class TravelingSalesman
    {
        public static int GetLength(int[,] dij, List<int> tour)
        {
            var length = 0;
            for(var i = 0; i < tour.Count - 1; i++)
            {
                length += dij[tour[i], tour[i + 1]];
            }
            return length;
        }
        public static List<int> GenerateTour(int[,] dij)
        {
            var totalLength = 0;
            //Default way
            var included = new List<int>();
            var notIncluded = new List<int>();
            for (var nodes = 0; nodes < dij.GetLength(0); nodes++)
            {
                notIncluded.Add(nodes);
            }

            //I1
            included.Add(notIncluded[0]);
            notIncluded.Remove(notIncluded[0]);

            //I2
            var second = -1;
            for (var col = 0; col < dij.GetLength(0); col++)
            {
                var length = dij[included[0], col];
                if ((second == -1 || length > dij[included[0], second]) && (!included.Contains(col)))
                {
                    second = col;
                }
            }
            included.Add(second);
            notIncluded.Remove(second);
            totalLength += dij[included[0], included[1]];

            //I3
            var third = -1;
            for (var col = 0; col < dij.GetLength(0); col++)
            {
                var length = dij[included[1], col];
                if ((third == -1 || length > dij[included[1], third]) && (!included.Contains(col)))
                {
                    third = col;
                }
            }
            included.Add(third);
            notIncluded.Remove(third);
            totalLength += dij[included[1], included[2]];

            //I1
            included.Add(0);
            totalLength += dij[included[2], included[3]];

            while (notIncluded.Count > 0)
            {
                var nextPosition = -1;
                var nextNode = -1;
                var minLength = int.MaxValue;
                foreach (var notInc in notIncluded)
                {
                    for (var incNode = 0; incNode < included.Count - 1; incNode++)
                    {
                        var length = dij[included[incNode], notInc];
                        length += dij[notInc, included[incNode + 1]];
                        length -= dij[included[incNode], included[incNode + 1]];
                        if (length < minLength)
                        {
                            nextPosition = incNode + 1;
                            nextNode = notInc;
                            minLength = length;
                        }
                    }
                }
                included.Insert(nextPosition, nextNode);
                notIncluded.Remove(nextNode);
                totalLength += minLength;
            }

            return included;
        }
    }
}
