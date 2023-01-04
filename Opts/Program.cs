using Opts;
using System.Text;

Console.WriteLine("Loading file...");

var input = File.ReadAllLines(@"E:\FRI\7.semester\OPTS\Opts\Matica_TN_(0276).txt");
//var input = File.ReadAllLines(@"E:\FRI\7.semester\OPTS\Opts\Matica_BB_(0515).txt");
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
foreach(var node in tourNode)
{
    tour += node + "->";    
}

Console.WriteLine("Way: " + tour);
Console.WriteLine("Node count: " + (tourNode.Count-1));
Console.WriteLine("Length: " + totalLength);