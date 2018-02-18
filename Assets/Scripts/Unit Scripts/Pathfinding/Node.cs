using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public Vector2 Position;
    public Node Parent;
    public int Cost;

    public Node(Vector2 pos, Node parent, int cost)
    {
        Position = pos;
        this.Parent = parent;
        this.Cost = cost;
    }
}
