﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using C5;
using SCG = System.Collections.Generic;

public class Graph
{
	#region Private Member Variables
	// private member variables
	private NodeList nodes;
	#endregion
	
	#region Constructor
	/// <summary>
	/// Default constructor.  Creates a new Graph class instance.
	/// </summary>
	public Graph()
	{
		this.nodes = new NodeList();
	}
	
	/// <summary>
	/// Creates a new graph class instance based on a list of nodes.
	/// </summary>
	/// <param name="nodes">The list of nodes to populate the newly created Graph class with.</param>
	public Graph(NodeList nodes)
	{
		this.nodes = nodes;
	}
	#endregion
	
	#region Public Methods
	/// <summary>
	/// Clears out all of the nodes in the graph.
	/// </summary>
	public virtual void Clear()
	{
		nodes.Clear();
	}
	
	#region Adding Node Methods
	/// <summary>
	/// Adds a new node to the graph.
	/// </summary>
	/// <param name="key">The key value of the node to add.</param>
	/// <param name="data">The data of the node to add.</param>
	/// <returns>A reference to the Node that was created and added to the graph.</returns>
	/// <remarks>If there already exists a node in the graph with the same <b>key</b> value then an
	/// <b>ArgumentException</b> exception will be thrown.</remarks>
	public virtual Node AddNode(string key, object data)
	{
		// Make sure the key is unique
		if (!nodes.ContainsKey(key))
		{
			Node n = new Node(key, data);
			nodes.Add(n);
			return n;
		}
		else
			throw new ArgumentException("There already exists a node in the graph with key " + key);
	}
	
	/// <summary>
	/// Adds a new node to the graph.
	/// </summary>
	/// <param name="n">A node instance to add to the graph</param>
	/// <remarks>If there already exists a node in the graph with the same <b>key</b> value as <b>n</b> then an
	/// <b>ArgumentException</b> exception will be thrown.</remarks>
	public virtual void AddNode(Node n)
	{
		// Make sure this node is unique
		if (!nodes.ContainsKey(n.Key))
			nodes.Add(n);
		else
			throw new ArgumentException("There already exists a node in the graph with key " + n.Key);
	}
	#endregion
	
	#region Adding Edge Methods
	/// <summary>
	/// Adds a directed edge from one node to another.
	/// </summary>
	/// <param name="uKey">The <b>Key</b> of the node from which the directed edge eminates.</param>
	/// <param name="vKey">The <b>Key</b> of the node from which the directed edge leads to.</param>
	/// <remarks>If nodes with <b>uKey</b> and <b>vKey</b> do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddDirectedEdge(string uKey, string vKey)
	{
		AddDirectedEdge(uKey, vKey, 0);
	}
	
	/// <summary>
	/// Adds a directed, weighted edge from one node to another.
	/// </summary>
	/// <param name="uKey">The <b>Key</b> of the node from which the directed edge eminates.</param>
	/// <param name="vKey">The <b>Key</b> of the node from which the directed edge leads to.</param>
	/// <param name="cost">The weight of the edge.</param>
	/// <remarks>If nodes with <b>uKey</b> and <b>vKey</b> do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddDirectedEdge(string uKey, string vKey, int cost)
	{
		// get references to uKey and vKey
		if (nodes.ContainsKey(uKey) && nodes.ContainsKey(vKey))
			AddDirectedEdge(nodes[uKey], nodes[vKey], cost);
		else
			throw new ArgumentException("One or both of the nodes supplied were not members of the graph.");
	}
	
	/// <summary>
	/// Adds a directed edge from one node to another.
	/// </summary>
	/// <param name="u">The node from which the directed edge eminates.</param>
	/// <param name="v">The node from which the directed edge leads to.</param>
	/// <remarks>If the passed-in nodes do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddDirectedEdge(Node u, Node v)
	{
		AddDirectedEdge(u, v, 0);
	}
	
	/// <summary>
	/// Adds a directed, weighted edge from one node to another.
	/// </summary>
	/// <param name="u">The node from which the directed edge eminates.</param>
	/// <param name="v">The node from which the directed edge leads to.</param>
	/// <param name="cost">The weight of the edge.</param>
	/// <remarks>If the passed-in nodes do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddDirectedEdge(Node u, Node v, int cost)
	{
		// Make sure u and v are Nodes in this graph
		if (nodes.ContainsKey(u.Key) && nodes.ContainsKey(v.Key))
			// add an edge from u -> v
			u.AddDirected(v, cost);
		else			
			// one or both of the nodes were not found in the graph
			throw new ArgumentException("One or both of the nodes supplied were not members of the graph.");
	}
	
	/// <summary>
	/// Adds an undirected edge from one node to another.
	/// </summary>
	/// <remarks>If nodes with <b>uKey</b> and <b>vKey</b> do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddUndirectedEdge(string uKey, string vKey)
	{
		AddUndirectedEdge(uKey, vKey, 0);
	}
	
	/// <summary>
	/// Adds an undirected, weighted edge from one node to another.
	/// </summary>
	/// <param name="cost">The weight of the edge.</param>
	/// <remarks>If nodes with <b>uKey</b> and <b>vKey</b> do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddUndirectedEdge(string uKey, string vKey, int cost)
	{
		// get references to uKey and vKey
		if (nodes.ContainsKey(uKey) && nodes.ContainsKey(vKey))
			AddUndirectedEdge(nodes[uKey], nodes[vKey], cost);
		else
			throw new ArgumentException("One or both of the nodes supplied were not members of the graph.");
	}
	
	/// <summary>
	/// Adds an undirected edge from one node to another.
	/// </summary>
	/// <remarks>If the passed-in nodes do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddUndirectedEdge(Node u, Node v)
	{
		AddUndirectedEdge(u, v, 0);
	}
	
	/// <summary>
	/// Adds an undirected, weighted edge from one node to another.
	/// </summary>
	/// <param name="cost">The weight of the edge.</param>
	/// <remarks>If the passed-in nodes do not exist in the graph, an <b>ArgumentException</b>
	/// exception is thrown.</remarks>
	public virtual void AddUndirectedEdge(Node u, Node v, int cost)
	{
		// Make sure u and v are Nodes in this graph
		if (nodes.ContainsKey(u.Key) && nodes.ContainsKey(v.Key))
		{
			// Add an edge from u -> v and from v -> u
			u.AddDirected(v, cost);
			v.AddDirected(u, cost);
		}
		else			
			// one or both of the nodes were not found in the graph
			throw new ArgumentException("One or both of the nodes supplied were not members of the graph.");
	}
	#endregion
	
	#region Contains Methods
	/// <summary>
	/// Determines if a node exists within the graph.
	/// </summary>
	/// <param name="n">The node to check for in the graph.</param>
	/// <returns><b>True</b> if the node <b>n</b> exists in the graph, <b>False</b> otherwise.</returns>
	public virtual bool Contains(Node n)
	{
		return Contains(n.Key);
	}
	
	/// <summary>
	/// Determines if a node exists within the graph.
	/// </summary>
	/// <param name="key">The key of the node to check for in the graph.</param>
	/// <returns><b>True</b> if a node with key <b>key</b> exists in the graph, <b>False</b> otherwise.</returns>
	public virtual bool Contains(string key)
	{
		return nodes.ContainsKey(key);
	}
	#endregion
	#endregion
	
	#region Public Properties
	/// <summary>
	/// Returns the number of nodes in the graph.
	/// </summary>
	public virtual int Count
	{
		get
		{
			return nodes.Count;
		}
	}
	
	/// <summary>
	/// Returns a reference to the set of nodes in the graph.
	/// </summary>
	public virtual NodeList Nodes
	{
		get
		{
			return this.nodes;
		}
	}
	#endregion

	#region Graph Algorithems
	/// <summary>
	/// Get all tile ID's X steps from current node
	/// </summary>
	/// <returns>Hashset of all valid nodes.</returns>
	/// <param name="node">Node.</param>
	/// <param name="maxStep">Max step.</param>
	public List<string> GetTileIDsNumOfStepFromNode(Node node, int maxStep)
	{
		Queue<Node> frontier = new Queue<Node> ();
		List<string> visited = new List<string>();
		Dictionary<string, int> distance = new Dictionary<string, int>();
		frontier.Enqueue(node);
		visited.Add (node.Key);
		distance.Add(node.Key, 0);

		while(frontier.Count > 0)
		{
			Node current = frontier.Dequeue();

			if(distance[current.Key] < maxStep)
			{
				foreach(EdgeToNeighbor edge in current.Neighbors)
				{
					if(!visited.Contains(edge.Neighbor.Key) && !distance.ContainsKey(edge.Neighbor.Key))
					{
						frontier.Enqueue(edge.Neighbor);
						visited.Add(edge.Neighbor.Key);
						distance.Add(edge.Neighbor.Key, (distance[current.Key] + 1));
					}
				}
			}
		}

		// Print out tiles that is cheked out and is in range
		string[] visitedIds = new string[visited.Count];
		visited.CopyTo(visitedIds);
		string output = "Visited: ";

		for(int i = 0; i < visitedIds.Length; i++)
		{
			output += (visitedIds[i] + "(" + distance[visitedIds[i]] + ")" +  (i < (visitedIds.Length - 1) ? ", " : ""));
		}

		//Debug.Log(output);

		return visited.ToList();
	}


    public List<string> ConstructPath(Node start, Node goal)
    {
        Queue<Node> frontier = new Queue<Node>();
        Dictionary<string, Node> cameFrom = new Dictionary<string, Node>();
        bool goalReached = false;
        Node current;
        frontier.Enqueue(start);
        
        while(frontier.Count > 0 && !goalReached)
        {
            current = frontier.Dequeue();

            if (current.Key == goal.Key)
            {
                goalReached = true;
            }
            else
            {
                foreach(EdgeToNeighbor edge in current.Neighbors)
                {
                    if (!cameFrom.ContainsKey(edge.Neighbor.Key)) 
                    {
                        frontier.Enqueue(edge.Neighbor);
                        cameFrom.Add(edge.Neighbor.Key, current);
                    }
                }
            }
        }

        current = goal;

        List<String> path = new List<string>();
        path.Add(current.Key);

        while (current.Key != start.Key)
        {
            current = cameFrom[current.Key];
            path.Add(current.Key);
        }

        path.Reverse();

        return path;
    }

	#endregion
}