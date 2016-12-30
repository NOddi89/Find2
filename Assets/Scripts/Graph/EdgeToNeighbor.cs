using UnityEngine;
using System.Collections;

public class EdgeToNeighbor 
{
	private int m_Cost;
	private Node m_Neighbor;

	public EdgeToNeighbor(Node neighbor) : this(neighbor, 0) {}

	public EdgeToNeighbor(Node neighbor, int cost)
	{
		m_Cost = cost;
		m_Neighbor = neighbor;
	}

	public virtual int Cost
	{
		get 
		{ 
			return m_Cost; 
		}
	}

	public virtual Node Neighbor
	{
		get
		{
			return m_Neighbor;
		}
	}

}
