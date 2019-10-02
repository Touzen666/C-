using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Graf_Finder
{
    // https://artint.info/html/ArtInt_63.html
    //klasa implementujaca algorytm B&B
    class BranchAndBound : BaseSearchStrategy
    {   //zmienna prywatna zawierajaca graf
        private Dictionary<int, Node> graph;
        //nr wezla koncowego i nr wezla poczatkowego
        int endNode;
        int startNode;

        // funkcja obliczająca cenę danej ścieżki
        private double computeBound(List<Node> path)
        {   //ustal poczatkowa cene sciezki na 0.0
            double price = 0.0;
            //dla kazdego wierzcholka ktory ma nastepce
            for (int i=0; i<path.Count-1; i++)
            {   //dodaj cene sciezki z wierzcholka do nastepcy
                price += path[i] - path[i + 1];
            }
            //zwroc cene sciezki
            return price;
        }

        // metoda wykonująca algorytm
        private List<Node> doTheAlgorithm(List<Node> heuristic)
        {   //stworz kolejke sciezek ktore mamy odwiedzic
            Queue<List<Node>> queue = new Queue<List<Node>>();
            //zanotuj sciezke najlepsza znaleziona do tej pory
            List<Node> bestSoFar = heuristic;
            //policz jej cene
            double bound = this.computeBound(heuristic);
            //storz liste rozpoczynajaca sie wierzcholkiem startowym
            List<Node> firstList = new List<Node>();
            firstList.Add(this.graph[this.startNode]);
            //i dodaj ja do kelejki
            queue.Enqueue(firstList);
            //dopoki sa jakies sciezki do odwiedzenia  
            while (queue.Count > 0)
            {   //pobierz tka sciezke z kolejki
                List<Node> nodeList = queue.Dequeue();
                //jezeli ona jest sciezka do miejsca docelowego
                if (nodeList.Last() == graph[endNode])
                {   //to policz jej cene 
                    if (this.computeBound(nodeList) > bound) {
                        //jezeli jej cena jest wyzsza od biezacej to kontynuuj
                        continue;
                    } else
                    {   //w przeciwnym wypadku zapisz jej cene i przebieg
                        bound = this.computeBound(nodeList);
                        bestSoFar = nodeList;
                    }
                } else
                {   //JEZELI TA SCIEZKA NIE KONCZY NASZEJ TRASY
                    //to przejdz przez kazdego sasiada
                    foreach (Node neighbour in nodeList.Last().nodes)
                    {   //najpierw skopiuj liste 
                        List<Node> childList = nodeList.Select(p => p).ToList();
                        //potem dodaj tego sasiada
                        childList.Add(neighbour);
                        //jezeli cena za ta sciezke jest wyzsza od biezacej to kontynuuj
                        if (this.computeBound(childList) > bound)
                        {
                            continue;
                        } else
                        {   //w przeciwnym wypadku dopisz ta sciezke do listy sciezek do odwiedzenia 
                            queue.Enqueue(childList);
                        }
                    }
                }
            }

            return bestSoFar; //jesli sie nie dalo ,zwroc null
        }
        //metoda znajdujaca trase
        public override List<Node> findAWay(Dictionary<int, Node> graph, int startNode, int endNode)
        {   //podstaw pola tego obiektu
            this.graph = graph;
            this.endNode = endNode;
            this.startNode = startNode;
            //znajdz heurystyka najlepsza trase a potem wykonaj algorytm
            List<Node> path = new DFS().findAWay(graph, startNode, endNode);
            return this.doTheAlgorithm(path);
        }
    }
}
