using UnityEngine;
using System.Collections;

public class AdjacencyList : CollectionBase 
{
	protected internal virtual void Add(EdgeToNeighbor e)
	{
		base.InnerList.Add (e);
	}

	public virtual EdgeToNeighbor this[int index]
	{
		get{ return (EdgeToNeighbor) base.InnerList[index]; }
		set{ base.InnerList[index] = value; }
	}
}
