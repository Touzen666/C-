using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Graf_Finder
{
    class BFS : BaseSearchStrategy
    {
        // metoda "odwiedzająca" wierzchołek

        public override List<Node> findAWay(Dictionary<int, Node> graph, int startNode, int endNode)
        {
            // tag 1 oznacza że wierzchołek jest "discovered"
            // https://en.wikipedia.org/wiki/Breadth-first_search
            //Kolejka typu FiFO
            Queue<Node> queue = new Queue<Node>();
            //Zapakuj wezel startowy do kolejki
            queue.Enqueue(graph[startNode]);
            //Jezeli kolejka nie jest pusta 
            while (queue.Count > 0)
            {
                //pobiez element z wierzcholka kolejki 
                Node v = queue.Dequeue();
                //jesli to on jest elementem ktorego szukalismy
                if (v.number == endNode)
                {
                    //storz zmienna tymczasowa ktora jest kolejka 
                    Queue<Node> path = new Queue<Node>();
                    //storz zmienna tymczasowa ktora bedzie reprezentowac aktualnie odwiedzony wierzcholek
                    Node currentNode = v;
                    //Dopóki nie doszlismy do poczatku
                    while (currentNode.parent != null)
                    {   //to wsadz do kolejki biezacy wezel 
                        path.Enqueue(currentNode);
                        //cofnij się o 1 do tylu 
                        currentNode = currentNode.parent;
                    }
                    // zaladuj wezel poczatkowy
                    path.Enqueue(currentNode);
                    //przeksztalc kolejke do listy
                    List<Node> path2 = path.Select(item => item).ToList();
                    //odroc liste 
                    path2.Reverse();
                    //zwroc sciezke 
                    return path2;
                }
                //dla kazdego sasiedniego wiezcholka 
                foreach (Node neighbour in v.nodes)
                {   //jesli jeszcze nie zostal odwiedzony
                    if (neighbour.tag == 0)
                    {
                        // to oznacz go jako odwiedzony
                        neighbour.tag = 1;
                        //oznacz ze jest osiagalny z tego wierzcholka 
                        neighbour.parent = v;
                        //dodaj do kolejki wierzcholkow do odwiedzenia 
                        queue.Enqueue(neighbour);
                    }
                }
            }
            // w innym wypadku nie ma przejscia
            return null;
        }
    }
}
