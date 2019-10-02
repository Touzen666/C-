using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Graf_Finder
{
    // wzorowana na https://en.wikipedia.org/wiki/A*_search_algorithm
    class AStar : BaseSearchStrategy
    {
        //Przeładowana metoda znajdująca trasę
        public override List<Node> findAWay(Dictionary<int, Node> graph, int startNode, int endNode)
        {
            // tag 1 oznacza że wierzchołek jest "discovered"
            // Zbior "open set"(zbiór wierzcholkow ktore mamy jeszcze odwiedzic)
            HashSet<Node> openSet = new HashSet<Node>();
            //Zapakuj wezel startowy do zbioru
            openSet.Add(graph[startNode]);
            //Jezeli kolejka nie jest pusta
            //definiujemy dwie zmienne pomocnicze 
            Dictionary<int, double> g_score = new Dictionary<int, double>();
            Dictionary<int, double> f_score = new Dictionary<int, double>();
            //zainicjuj je jakimis wartosciami
            f_score.Add(startNode, graph[startNode] - graph[endNode]);
            g_score.Add(startNode, 0.0);
            //dopoki w zbiorze znajdują sie elementy ktore nalezy odwiedzic
            while (openSet.Count > 0)
            {
                // https://en.wikipedia.org/wiki/A*_search_algorithm
                //pobiez element majacy najmniejsze f_score
                Node v = openSet.OrderBy(p => f_score.ContainsKey(p.number) ?//Najpierw sortuje wszystkie elementy openSet 
                f_score[p.number] :             // rosnąco po ich f_wartości, jeśli ta jest zdefiniowana w zmiennej pomocniczej
                Double.PositiveInfinity).       // podstaw +nieskonczonosc w innym przypadku
                First();                        // wybierz element majacy najmniejszy f_score
                openSet.Remove(v);              //usun ten element ze zbioru openSet
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
                        //wylicz nastepne g_score
                        double tentative_g_score = g_score[v.number] + (v - neighbour);
                        //dodaj sasiada do listy wierzcholkow do odwiedzenia 
                        openSet.Add(neighbour);
                        //jezeli do tego wierzcholka da sie dojsc inna trasa 
                        if (g_score.ContainsKey(neighbour.number))
                        {   //to jesli jest lepsza od tej to kontynuuj
                            if (tentative_g_score >= g_score[neighbour.number])
                                continue;
                        }
                        //oznacz ze jest osiagalny z tego wierzcholka 
                        neighbour.parent = v;
                        //zaktualizuj g i f_score
                        g_score[neighbour.number] = tentative_g_score;
                        f_score[neighbour.number] = tentative_g_score + (neighbour - graph[endNode]);
                    }
                }
            }
            // w innym wypadku nie ma przejscia
            return null;
        }
    }
}
