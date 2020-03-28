using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Astar
{
    public List<Node> openList = new List<Node>(); //waar je nog heen kan
    public HashSet<Node> closedHash = new HashSet<Node>(); // waar je bent geweest en waar je bent
    public Cell[,] currentGrid;

    /// <summary>
    /// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path
    /// Note that you will probably need to add some helper functions
    /// from the startPos to the endPos
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        currentGrid = grid;
        Node startNode = new Node(startPos, null, 0,0);
        Node targetNode = new Node(endPos, null, int.MaxValue, 0);
        List<Vector2Int> path = new List<Vector2Int>();

        openList.Add(startNode);
        openList.Add(targetNode);

        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                openList.Add(new Node(grid[i, j].gridPosition, null, int.MaxValue, 0));


        while (openList.Count > 0)
        {
            //Get the Current Node
            Node currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FScore < currentNode.FScore || (openList[i].FScore == currentNode.FScore && openList[i].HScore < currentNode.HScore))
                    currentNode = openList[i];
            }

            //remove currentNode from open list and add it to the closed
            openList.Remove(currentNode);
            closedHash.Add(currentNode);

            //Retracing parents
            if (currentNode.position != targetNode.position)
            {
                //checking neighbors
                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (closedHash.Contains(neighbor))
                        continue;

                    int newMovement = System.Convert.ToInt32(currentNode.GScore + GetDistance(currentNode, neighbor));

                    if (newMovement < neighbor.GScore)
                    {
                        neighbor.GScore = newMovement;
                        neighbor.HScore = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                    }
                }
            }
            //Retract parent  for the path
            else
            {
                path = RetractParent(startNode, targetNode);
                break;
            }
        }

        return path;
    }
    private List<Vector2Int> RetractParent(Node startNode, Node endNode)
    {
        List<Vector2Int> FinalPath = new List<Vector2Int>();
        Node currentNode = endNode;
        Debug.Log(currentNode.parent);

        while (currentNode != startNode)
        {
            FinalPath.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        FinalPath.Reverse();
        return FinalPath;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distance = (int)Vector2Int.Distance(nodeA.position, nodeB.position);
        return distance;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                if (Math.Abs(x) == Math.Abs(y))
                    continue;

                int checkX = node.position.x + x;
                int checkY = node.position.y + y;

                if(currentGrid[node.position.x, node.position.y].HasWall(Wall.UP) && y == 1 || currentGrid[node.position.x, node.position.y].HasWall(Wall.RIGHT) && x == 1 ||
                currentGrid[node.position.x, node.position.y].HasWall(Wall.LEFT) && x == -1 || currentGrid[node.position.x, node.position.y].HasWall(Wall.DOWN) && y == -1) 
                continue;

                if (checkX >= -1 && checkY >= -1)
                {
                    for (int i = 0; i < openList.Count; i++)
                    {
                        if (openList[i].position.y == checkY && openList[i].position.x == checkX)
                        {

                            neighbors.Add(openList[i]);
                        }
                    }
                }


            }
        }
        return neighbors;
    }
}


/// <summary>
/// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
/// </summary>
public class Node
{
    public Vector2Int position; //Position on the grid
    public Node parent; //Parent Node of this node

    public float FScore
    { //GScore + HScore
        get
        {
            return GScore + HScore;
        }
    }
    public float GScore; //Current Travelled Distance distance from starting node
    public float HScore; //Distance estimated based on Heuristic distance from end node

    public Node() { }

    public Node(Vector2Int position, Node parent, int GScore, int HScore)
    {
        this.position = position;
        this.parent = parent;
        this.GScore = GScore;
        this.HScore = HScore;
    }
}


