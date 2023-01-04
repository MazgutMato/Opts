using Opts;
using System.Text;

Console.WriteLine("Loading file...");

//var input = File.ReadAllLines(@"C:\Users\matej\Desktop\Ing\1.semester\Opts\Matica_TN_(0276).txt");
//var input = File.ReadAllLines(@"E:\FRI\7.semester\OPTS\Opts\Matica_TN_(0276).txt");
var input = File.ReadAllLines(@"C:\Users\matej\Desktop\Ing\1.semester\Opts\Matica_BB_(0515).txt");
//var input = File.ReadAllLines(@"E:\FRI\7.semester\OPTS\Opts\Matica_Moja.txt");

//Load dij
var i = 0;
int[,]? dij = null;
foreach (var row in input)
{
    if(dij== null)
    {
        var size = int.Parse(row);
        dij = new int[size, size];
    }
    else
    {
        var j = 0;
        foreach (var col in row.Trim().Split(' '))
        {
            dij[i, j] = int.Parse(col.Trim());
            j++;
        }
        i++;
    }    
}

var tourNode = TravelingSalesman.GenerateTour(dij);
string tour = "";
for(var node = 0; node < tourNode.Count - 1; node++)
{
    tour += tourNode[node] + "->";    
}
tour += tourNode[tourNode.Count - 1];

Console.WriteLine("-----------------------------------------------------------------------------");
Console.WriteLine("Traveling Salesman:");
Console.WriteLine("\tTour: " + tour);
Console.WriteLine("\tNode count: " + (tourNode.Count-1));
Console.WriteLine("\tTour length: " + TravelingSalesman.GetLength(dij,tourNode));
Console.WriteLine("-----------------------------------------------------------------------------");

Console.WriteLine("-----------------------------------------------------------------------------");
Console.WriteLine("Simulated Annealing:");
var simAnnealing = new SimulatedAnnealing(10000, 40, 50, tourNode, dij);
var simTour = simAnnealing.FindMinimum();
tour = "";
for (var node = 0; node < simTour.Count - 1; node++)
{
    tour += simTour[node] + "->";
}
tour += tourNode[tourNode.Count - 1];
Console.WriteLine("\tTour: " + tour);
Console.WriteLine("\tNode count: " + (simTour.Count - 1));
Console.WriteLine("\tTour length: " + TravelingSalesman.GetLength(dij, simTour));
Console.WriteLine("-----------------------------------------------------------------------------");