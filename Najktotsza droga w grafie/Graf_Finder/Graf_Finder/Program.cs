using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graf_Finder
{
    // Klasa reprezentująca węzeł w pamięci
    class Node {
        // Nr tego węzła
        public int number;
        // Względna pozycja x tego węzła
        public int x;
        // Względna pozycja y tego węzła
        public int y;
        // Krawędzie prowadzące do innych węzłów (tak więc ta klasa opisuje graf skierowany)
        public List<Node> nodes = new List<Node>();
        // Zmienna tymczasowa do użytku algorytmów znajdujących trasę
        public int tag = 0;
        // Zmienna tymczasowa do użytku algorytmów znajdujących trasę
        public Node parent = null;
        
        //Konstruktor obiektu klasy node 
        public Node(int number, int x, int y)
        {
            this.number = number;
            this.x = x;
            this.y = y;
        }
        //zaimplementowany wzor na odleglosc miedzy punktami
        public static double operator-(Node a, Node b) {

            //zwraca odległość jednego punktu od drugiego w przestrzeni 2D
            return Math.Sqrt((a.x - b.x)*(a.x-b.x) + (a.y-b.y)*(a.y-b.y));
        }
    }

    class Program
    {
        //funkcja wczytujaca graf z pliku
        static Dictionary<int, Node> loadGraphFromFile(string filename)
        {
            //Otworz plik z grafem
            StreamReader reader = File.OpenText(filename);
            //Zmienna pomocnicza do której bedziemy wczytywac kolejne linie
            string line;
            //Zmienna pomocnicza do której bedziemy wczytywac ten graf
            Dictionary<int, Node> ourMap = new Dictionary<int, Node>();
            int whatIsBeingRead = 0;    // 0 powoduje wczytanie wezla 1 powoduje wczytanie krawedzi
            while ((line = reader.ReadLine()) != null)
            {
                if (whatIsBeingRead == 0)
                {
                    // Sprawdź czy nie natrafiliśmy na haszyk, bo wtedy trzeba przejść do trybu wczytywania krawędzi
                    if (line == "#")
                    {
                        whatIsBeingRead = 1;
                        continue;
                    }
                    // Wczytaj pojedynczy wierzchołek grafu
                    string[] parts = line.Split(' ');   // wydziel z line numer węzła i współrzędne
                    int nodeNo = Int32.Parse(parts[0]);
                    string[] coords = parts[1].Split(',');  // wydziel z współrzędnych x i y
                    int x = Int32.Parse(coords[0]);
                    int y = Int32.Parse(coords[1]);
                    Node node = new Node(nodeNo, x, y);       // stworz nowy obiekt typu Node
                    ourMap.Add(nodeNo, node);               // dodaj go do naszego słownika
                } else
                {
                    // Wczytaj krawędź tego grafu
                    string[] parts = line.Split(' ');
                    int startNode = Int32.Parse(parts[0]);
                    int stopNode = Int32.Parse(parts[1]);
                    ourMap[startNode].nodes.Add(ourMap[stopNode]);  // dodaj do elementu rozpoczynajacego informacje o drodze do elementu koncowego.
                }
            }
            return ourMap;
        }

        static void Main(string[] args)
        {
            Dictionary<int, Node> graph = Program.loadGraphFromFile(args[0]);
            // Wybierz strategię przeszukiwania
            BaseSearchStrategy bss;
            bss = new DFS();
        
            if (args[1] == "BFS")
            {
                Console.WriteLine("Setting BFS");
                bss = new BFS();
            }
            if (args[1] == "AStar") {
                Console.WriteLine("Setting AStar");
                bss = new AStar();
            }              
            if (args[1] == "B&B")
            {
                Console.WriteLine("Setting B&B");
                bss = new BranchAndBound();
            }

            List<Node> way = bss.findAWay(graph, Int32.Parse(args[2]), Int32.Parse(args[3]));
            Bitmap drawing = Graf_Finder.Drawing.draw(graph, way);
            drawing.Save("output.bmp");
        }

  
    }
}
