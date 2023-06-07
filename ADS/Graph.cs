namespace CM_ADS;

public class Graph
{
    public List<Vertex> Vertices { get; set; }
    public List<Edge> Edges { get; set; }

    public Graph()
    {
        Vertices = new List<Vertex>();
        Edges = new List<Edge>();
    }
    
    public bool AddEdge(Vertex v1, Vertex v2, double d)
    {
        if (!Vertices.Contains(v1)) return false;
        if (!Vertices.Contains(v2)) return false;
        foreach (Edge cure in v1.GetEdges())
        {
            if (cure.Destination.GetId() == v2.GetId()) return false;
        }
            

        Edge ev1v2 = new Edge(v1, v2, d);
        v1.GetEdges().Add(ev1v2); Edges.Add(ev1v2);
        return true;
    }


    public List<Vertex> BFS(Vertex startVertex, Vertex endVertex)
    {
        
        Queue<Vertex> Queue = new Queue<Vertex>();
        List<Vertex> visited = new List<Vertex>();
        
        // Create a dictionary to store the path
        Dictionary<Vertex, Vertex> path = new Dictionary<Vertex, Vertex>();
        
        visited.Add(startVertex);

        
        // Инициализация
        foreach(Vertex cv in Vertices)
        {
            cv.SumDistance = double.MaxValue;
            cv.PrevVertex = null;
            cv.IsVisited = false;
        }
        
        startVertex.Color = Color.Gray;
        
        startVertex.SumDistance = 0;
        startVertex.IsVisited = true;
        Queue.Enqueue (startVertex);
        
        Vertex current, v;
        Edge tr;
        List<Edge> edges_u;
        
        // Основной цикл
        while (Queue.Count > 0)
        {
            current = Queue.Dequeue();
            if (current.Equals(endVertex)) break;

            foreach (Edge edge in current.GetEdges())
            {
                v = edge.Destination;
                if (!v.IsVisited)
                {
                    path[v] = current;
                    Queue.Enqueue(v);
                    visited.Add(v);
                    v.IsVisited = true;
                }
                
            }
            
            
        }

        // // Mark all vertices as not visited
        // foreach (Vertex vert in Vertices)
        // {
        //     vert.IsVisited = false;
        // }
        //
        // // Create a queue for BFS
        // Queue<Vertex> queue = new Queue<Vertex>();
        //
        // // Mark the current vertex as visited and enqueue it
        // startVertex.IsVisited = true;
        // queue.Enqueue(startVertex);
        //
        // // Create a dictionary to store the path
        // Dictionary<Vertex, Vertex> path = new Dictionary<Vertex, Vertex>();
        //
        // while (queue.Count > 0)
        // {
        //     // Dequeue a vertex from the queue
        //     Vertex currentVertex = queue.Dequeue();
        //
        //     // Get the adjacent vertices of the current vertex
        //     List<Edge> edges = currentVertex.GetEdges();
        //     foreach (Edge edge in edges)
        //     {
        //         Vertex adjacentVertex = edge.Destination;
        //         if (!adjacentVertex.IsVisited)
        //         {
        //             // Mark the adjacent vertex as visited, enqueue it, and store the path
        //             adjacentVertex.IsVisited = true;
        //             queue.Enqueue(adjacentVertex);
        //             path[adjacentVertex] = currentVertex;
        //         }
        //     }
        // }

        

        

        return visited;
    }

    // Depth-First Search
    public List<Vertex> DFS(Vertex startVertex)
    {
        // Mark all vertices as not visited
        foreach (Vertex vert in Vertices)
        {
            vert.IsVisited = false;
        }

        // Create a dictionary to store the path
        Dictionary<Vertex, Vertex> path = new Dictionary<Vertex, Vertex>();

        // Call the recursive helper function
        DFSUtil(startVertex, path);

        // Build the path as a list of vertices
        List<Vertex> result = new List<Vertex>();
        Vertex vertex = startVertex;
        while (vertex != null)
        {
            result.Add(vertex);
            if (path.ContainsKey(vertex))
            {
                vertex = path[vertex];
            }
            else
            {
                vertex = null;
            }
        }

        result.Reverse(); // Reverse the list to get the correct path order

        return result;
    }

    private void DFSUtil(Vertex vertex, Dictionary<Vertex, Vertex> path)
    {
        // Mark the current vertex as visited
        vertex.IsVisited = true;

        // Get the adjacent vertices of the current vertex
        List<Edge> edges = vertex.GetEdges();
        foreach (Edge edge in edges)
        {
            Vertex adjacentVertex = edge.Destination;
            if (!adjacentVertex.IsVisited)
            {
                // Store the path and recursively call the DFSUtil function on the adjacent vertex
                path[adjacentVertex] = vertex;
                DFSUtil(adjacentVertex, path);
            }
        }
    }

    private List<Vertex> GetPath(Dictionary<Vertex, Vertex> parents, Vertex start, Vertex end)
    {
        List<Vertex> path = new List<Vertex>();

        if (!parents.ContainsKey(end))
        {
            return null;
        }

        Vertex current = end;

        while (current != start)
        {
            path.Add(current);
            current = parents[current];
        }

        path.Add(start);

        path.Reverse();

        return path;
    }
}


// // Define a generic Graph class that can be used with any type T to represent the nodes of the graph
// public class Graph<T>
// {
//     // Use a dictionary to store the adjacency list of the graph
//     private readonly Dictionary<T, List<T>> _adjacencyList;
//
//     // Constructor that initializes the adjacency list
//     public Graph()
//     {
//         _adjacencyList = new Dictionary<T, List<T>>();
//     }
//
//     // Method to add a node to the graph
//     public void AddNodeGraph(T nodeValue)
//     {
//         // If the node does not already exist in the graph, add it to the adjacency list
//         if (!_adjacencyList.ContainsKey(nodeValue))
//         {
//             _adjacencyList.Add(nodeValue, new List<T>());
//         }
//     }
//
//     // Method to add an edge between two nodes in the graph
//     public void AddEdge(T nodeValue1, T nodeValue2)
//     {
//         // If both nodes exist in the graph, add an edge between them by adding each node to the other's adjacency list
//         if (_adjacencyList.ContainsKey(nodeValue1) && _adjacencyList.ContainsKey(nodeValue2))
//         {
//             _adjacencyList[nodeValue1].Add(nodeValue2);
//             _adjacencyList[nodeValue2].Add(nodeValue1);
//         }
//         else
//         {
//             // If one or both nodes do not exist in the graph, throw an exception
//             throw new ArgumentException("One or more nodes do not exist in the graph.");
//         }
//     }
//
//     // Method to remove a node from the graph
//     public void RemoveNodeGraph(T nodeValue)
//     {
//         // If the node does not exist in the graph, throw an exception
//         if (!_adjacencyList.ContainsKey(nodeValue))
//         {
//             throw new ArgumentException("NodeGraph does not exist in the graph.");
//         }
//
//         // Remove all edges that contain the node to be removed by removing the node from each of its adjacent nodes' adjacency lists
//         foreach (var adjacentNodeGraph in _adjacencyList[nodeValue])
//         {
//             _adjacencyList[adjacentNodeGraph].Remove(nodeValue);
//         }
//
//         // Remove the node from the adjacency list
//         _adjacencyList.Remove(nodeValue);
//     }
//
//     // Method to remove an edge between two nodes in the graph
//     public void RemoveEdge(T nodeValue1, T nodeValue2)
//     {
//         // If both nodes exist in the graph, remove the edge between them by removing each node from the other's adjacency list
//         if (_adjacencyList.ContainsKey(nodeValue1) && _adjacencyList.ContainsKey(nodeValue2))
//         {
//             _adjacencyList[nodeValue1].Remove(nodeValue2);
//             _adjacencyList[nodeValue2].Remove(nodeValue1);
//         }
//         else
//         {
//             // If one or both nodes do not exist in the graph, throw an exception
//             throw new ArgumentException("One or more nodes do not exist in the graph.");
//         }
//     }
//
//     // Method to get the adjacent nodes of a given node in the graph
//     public List<T> GetAdjacentNodeGraphs(T nodeValue)
//     {
//         // If the node does not exist in the graph, throw an exception
//         if (!_adjacencyList.ContainsKey(nodeValue))
//         {
//             throw new ArgumentException("NodeGraph does not exist in the graph.");
//         }
//
//         // Return the adjacency list of the node
//         return _adjacencyList[nodeValue];
//     }
//
//     // Method to check if a node exists in the graph
//     public bool HasNodeGraph(T nodeValue)
//     {
//         // Check if the node exists in the adjacency list
//         return _adjacencyList.ContainsKey(nodeValue);
//     }
//     
//     // Method to perform breadth-first search on the graph starting from a given node
//     public List<T> BFS(T startNodeGraph)
//     {
//         // If the start node does not exist in the graph, throw an exception
//         if (!_adjacencyList.ContainsKey(startNodeGraph))
//         {
//             throw new ArgumentException("Start node does not exist in the graph.");
//         }
//
//         // Create a queue to store the nodes to be visited
//         Queue<T> queue = new Queue<T>();
//         // Create a hash set to store the visited nodes
//         HashSet<T> visited = new HashSet<T>();
//         // Create a list to store the nodes in the order they were visited
//         List<T> bfsOrder = new List<T>();
//
//         // Add the start node to the queue and mark it as visited
//         queue.Enqueue(startNodeGraph);
//         visited.Add(startNodeGraph);
//
//         // While there are still nodes in the queue
//         while (queue.Count > 0)
//         {
//             // Dequeue the next node from the queue
//             T currentNodeGraph = queue.Dequeue();
//
//             // Add the current node to the list of visited nodes
//             bfsOrder.Add(currentNodeGraph);
//
//             // Visit all adjacent nodes of the current node that have not already been visited
//             foreach (T adjacentNodeGraph in _adjacencyList[currentNodeGraph])
//             {
//                 if (!visited.Contains(adjacentNodeGraph))
//                 {
//                     queue.Enqueue(adjacentNodeGraph);
//                     visited.Add(adjacentNodeGraph);
//                 }
//             }
//         }
//
//         // Return the list of nodes in the order they were visited
//         return bfsOrder;
//     }
//
//     // Method to perform depth-first search on the graph starting from a given node
//     public List<T> DFS(T startNodeGraph)
//     {
//         // If the start node does not exist in the graph, throw an exception
//         if (!_adjacencyList.ContainsKey(startNodeGraph))
//         {
//             throw new ArgumentException("Start node does not exist in the graph.");
//         }
//
//         // Create a stack to store the nodes to be visited
//         Stack<T> stack = new Stack<T>();
//         // Create a hash set to store the visited nodes
//         HashSet<T> visited = new HashSet<T>();
//         // Create a list to store the nodes in the order they were visited
//         List<T> dfsOrder = new List<T>();
//
//         // Push the start node onto the stack and mark it as visited
//         stack.Push(startNodeGraph);
//         visited.Add(startNodeGraph);
//
//         // While there are still nodes in the stack
//         while (stack.Count > 0)
//         {
//             // Pop the next node from the stack
//             T currentNodeGraph = stack.Pop();
//
//             // Add the current node to the list of visited nodes
//             dfsOrder.Add(currentNodeGraph);
//
//             // Visit all adjacent nodes of the current node that have not already been visited
//             foreach (T adjacentNodeGraph in _adjacencyList[currentNodeGraph])
//             {
//                 if (!visited.Contains(adjacentNodeGraph))
//                 {
//                     stack.Push(adjacentNodeGraph);
//                     visited.Add(adjacentNodeGraph);
//                 }
//             }
//         }
//
//         // Return the list of nodes in the order they were visited
//         return dfsOrder;
//     }
// }
