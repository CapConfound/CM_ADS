namespace CM_ADS;

// Define a generic Graph class that can be used with any type T to represent the nodes of the graph
public class Graph<T>
{
    // Use a dictionary to store the adjacency list of the graph
    private readonly Dictionary<T, List<T>> _adjacencyList;

    // Constructor that initializes the adjacency list
    public Graph()
    {
        _adjacencyList = new Dictionary<T, List<T>>();
    }

    // Method to add a node to the graph
    public void AddNode(T nodeValue)
    {
        // If the node does not already exist in the graph, add it to the adjacency list
        if (!_adjacencyList.ContainsKey(nodeValue))
        {
            _adjacencyList.Add(nodeValue, new List<T>());
        }
    }

    // Method to add an edge between two nodes in the graph
    public void AddEdge(T nodeValue1, T nodeValue2)
    {
        // If both nodes exist in the graph, add an edge between them by adding each node to the other's adjacency list
        if (_adjacencyList.ContainsKey(nodeValue1) && _adjacencyList.ContainsKey(nodeValue2))
        {
            _adjacencyList[nodeValue1].Add(nodeValue2);
            _adjacencyList[nodeValue2].Add(nodeValue1);
        }
        else
        {
            // If one or both nodes do not exist in the graph, throw an exception
            throw new ArgumentException("One or more nodes do not exist in the graph.");
        }
    }

    // Method to remove a node from the graph
    public void RemoveNode(T nodeValue)
    {
        // If the node does not exist in the graph, throw an exception
        if (!_adjacencyList.ContainsKey(nodeValue))
        {
            throw new ArgumentException("Node does not exist in the graph.");
        }

        // Remove all edges that contain the node to be removed by removing the node from each of its adjacent nodes' adjacency lists
        foreach (var adjacentNode in _adjacencyList[nodeValue])
        {
            _adjacencyList[adjacentNode].Remove(nodeValue);
        }

        // Remove the node from the adjacency list
        _adjacencyList.Remove(nodeValue);
    }

    // Method to remove an edge between two nodes in the graph
    public void RemoveEdge(T nodeValue1, T nodeValue2)
    {
        // If both nodes exist in the graph, remove the edge between them by removing each node from the other's adjacency list
        if (_adjacencyList.ContainsKey(nodeValue1) && _adjacencyList.ContainsKey(nodeValue2))
        {
            _adjacencyList[nodeValue1].Remove(nodeValue2);
            _adjacencyList[nodeValue2].Remove(nodeValue1);
        }
        else
        {
            // If one or both nodes do not exist in the graph, throw an exception
            throw new ArgumentException("One or more nodes do not exist in the graph.");
        }
    }

    // Method to get the adjacent nodes of a given node in the graph
    public List<T> GetAdjacentNodes(T nodeValue)
    {
        // If the node does not exist in the graph, throw an exception
        if (!_adjacencyList.ContainsKey(nodeValue))
        {
            throw new ArgumentException("Node does not exist in the graph.");
        }

        // Return the adjacency list of the node
        return _adjacencyList[nodeValue];
    }

    // Method to check if a node exists in the graph
    public bool HasNode(T nodeValue)
    {
        // Check if the node exists in the adjacency list
        return _adjacencyList.ContainsKey(nodeValue);
    }
    
    // Method to perform breadth-first search on the graph starting from a given node
    public List<T> BFS(T startNode)
    {
        // If the start node does not exist in the graph, throw an exception
        if (!_adjacencyList.ContainsKey(startNode))
        {
            throw new ArgumentException("Start node does not exist in the graph.");
        }

        // Create a queue to store the nodes to be visited
        Queue<T> queue = new Queue<T>();
        // Create a hash set to store the visited nodes
        HashSet<T> visited = new HashSet<T>();
        // Create a list to store the nodes in the order they were visited
        List<T> bfsOrder = new List<T>();

        // Add the start node to the queue and mark it as visited
        queue.Enqueue(startNode);
        visited.Add(startNode);

        // While there are still nodes in the queue
        while (queue.Count > 0)
        {
            // Dequeue the next node from the queue
            T currentNode = queue.Dequeue();

            // Add the current node to the list of visited nodes
            bfsOrder.Add(currentNode);

            // Visit all adjacent nodes of the current node that have not already been visited
            foreach (T adjacentNode in _adjacencyList[currentNode])
            {
                if (!visited.Contains(adjacentNode))
                {
                    queue.Enqueue(adjacentNode);
                    visited.Add(adjacentNode);
                }
            }
        }

        // Return the list of nodes in the order they were visited
        return bfsOrder;
    }

    // Method to perform depth-first search on the graph starting from a given node
    public List<T> DFS(T startNode)
    {
        // If the start node does not exist in the graph, throw an exception
        if (!_adjacencyList.ContainsKey(startNode))
        {
            throw new ArgumentException("Start node does not exist in the graph.");
        }

        // Create a stack to store the nodes to be visited
        Stack<T> stack = new Stack<T>();
        // Create a hash set to store the visited nodes
        HashSet<T> visited = new HashSet<T>();
        // Create a list to store the nodes in the order they were visited
        List<T> dfsOrder = new List<T>();

        // Push the start node onto the stack and mark it as visited
        stack.Push(startNode);
        visited.Add(startNode);

        // While there are still nodes in the stack
        while (stack.Count > 0)
        {
            // Pop the next node from the stack
            T currentNode = stack.Pop();

            // Add the current node to the list of visited nodes
            dfsOrder.Add(currentNode);

            // Visit all adjacent nodes of the current node that have not already been visited
            foreach (T adjacentNode in _adjacencyList[currentNode])
            {
                if (!visited.Contains(adjacentNode))
                {
                    stack.Push(adjacentNode);
                    visited.Add(adjacentNode);
                }
            }
        }

        // Return the list of nodes in the order they were visited
        return dfsOrder;
    }
}
