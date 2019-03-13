using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueRoom : Room
{
    public bool hasUpExit;
    public bool hasRightExit;
    public bool hasDownExit;
    public bool hasLeftExit;
    public bool hasUpRightPath;
    public bool hasUpDownPath;
    public bool hasUpLeftPath;
    public bool hasRightDownPath;
    public bool hasRightLeftPath;
    public bool hasDownLeftPath;

    private int[,] roomGrid;

    public bool isUpExit(int x, int y)
    {
        return x == LevelGenerator.ROOM_WIDTH / 2 && y == LevelGenerator.ROOM_HEIGHT - 1;
    }
    public bool isRightExit(int x, int y)
    {
        return x == LevelGenerator.ROOM_WIDTH - 1 && y == LevelGenerator.ROOM_HEIGHT / 2;
    }
    public bool isDownExit(int x, int y)
    {
        return x == LevelGenerator.ROOM_WIDTH / 2 && y == 0;
    }
    public bool isLeftExit(int x, int y)
    {
        return x == 0 && y == LevelGenerator.ROOM_HEIGHT / 2;
    }

    public void buildGrid()
    {
        string initialGridString = designedRoomFile.text;
        string[] rows = initialGridString.Trim().Split('\n');
        int width = rows[0].Trim().Split(',').Length;
        int height = rows.Length;
        if (height != LevelGenerator.ROOM_HEIGHT)
        {
            throw new UnityException(string.Format("Error in room by {0}. Wrong height, Expected: {1}, Got: {2}", roomAuthor, LevelGenerator.ROOM_HEIGHT, height));
        }
        if (width != LevelGenerator.ROOM_WIDTH)
        {
            throw new UnityException(string.Format("Error in room by {0}. Wrong width, Expected: {1}, Got: {2}", roomAuthor, LevelGenerator.ROOM_WIDTH, width));
        }
        int[,] indexGrid = new int[width, height];
        for (int r = 0; r < height; r++)
        {
            string row = rows[height - r - 1];
            string[] cols = row.Trim().Split(',');
            for (int c = 0; c < width; c++)
            {
                indexGrid[c, r] = int.Parse(cols[c]);
            }
        }
        roomGrid = indexGrid;
    }

    public bool inGrid(Vector2Int point)
    {
        return point.x >= 0 && point.x < roomGrid.GetLength(0) && point.y >= 0 && point.y < roomGrid.GetLength(1);
    }

    public bool isEmpty(Vector2Int point)
    {
        return roomGrid[point.x, point.y] == 0 || roomGrid[point.x, point.y] == 5;
    }

    public void preLabelRoom()
    {

        buildGrid();

        Vector2Int upExit = new Vector2Int(LevelGenerator.ROOM_WIDTH / 2, LevelGenerator.ROOM_HEIGHT - 1);
        Vector2Int rightExit = new Vector2Int(LevelGenerator.ROOM_WIDTH - 1, LevelGenerator.ROOM_HEIGHT / 2);
        Vector2Int downExit = new Vector2Int(LevelGenerator.ROOM_WIDTH / 2, 0);
        Vector2Int leftExit = new Vector2Int(0, LevelGenerator.ROOM_HEIGHT / 2);

        hasUpExit = inGrid(upExit) && isEmpty(upExit);
        hasRightExit = inGrid(rightExit) && isEmpty(rightExit);
        hasDownExit = inGrid(downExit) && isEmpty(downExit);
        hasLeftExit = inGrid(leftExit) && isEmpty(leftExit);

        hasUpRightPath = doesPathExist(upExit, rightExit);
        hasUpDownPath = doesPathExist(upExit, downExit);
        hasUpLeftPath = doesPathExist(upExit, leftExit);
        hasRightDownPath = doesPathExist(rightExit, downExit);
        hasRightLeftPath = doesPathExist(rightExit, leftExit);
        hasDownLeftPath = doesPathExist(downExit, leftExit);
    }

    public bool doesPathExist(Vector2Int startPoint, Vector2Int targetPoint)
    {
        List<Vector2Int> frontier = new List<Vector2Int>();
        List<Vector2Int> closed = new List<Vector2Int>();
        if (inGrid(startPoint) && isEmpty(startPoint))
        {
            frontier.Add(startPoint);
        }
        while (frontier.Count > 0)
        {
            Vector2Int currentPoint = frontier[0];
            frontier.RemoveAt(0);
            closed.Add(currentPoint);
            if (currentPoint == targetPoint)
            {
                return true;
            }
            Vector2Int upNeighbor = new Vector2Int(currentPoint.x, currentPoint.y + 1);
            if (inGrid(upNeighbor)
                && isEmpty(upNeighbor)
                && !closed.Contains(upNeighbor)
                && !frontier.Contains(upNeighbor))
            {
                frontier.Add(upNeighbor);
            }
            Vector2Int rightNeighbor = new Vector2Int(currentPoint.x + 1, currentPoint.y);
            if (inGrid(rightNeighbor)
                && isEmpty(rightNeighbor)
                && !closed.Contains(rightNeighbor)
                && !frontier.Contains(rightNeighbor))
            {
                frontier.Add(rightNeighbor);
            }
            Vector2Int downNeighbor = new Vector2Int(currentPoint.x, currentPoint.y - 1);
            if (inGrid(downNeighbor)
                && isEmpty(downNeighbor)
                && !closed.Contains(downNeighbor)
                && !frontier.Contains(downNeighbor))
            {
                frontier.Add(downNeighbor);
            }
            Vector2Int leftNeighbor = new Vector2Int(currentPoint.x - 1, currentPoint.y);
            if (inGrid(leftNeighbor)
                && isEmpty(leftNeighbor)
                && !closed.Contains(leftNeighbor)
                && !frontier.Contains(leftNeighbor))
            {
                frontier.Add(leftNeighbor);
            }
        }
        return false;
    }
}
