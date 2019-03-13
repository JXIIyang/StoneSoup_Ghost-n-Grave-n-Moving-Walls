using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiLabeledRoomPicker : Room
{

    public ExampleLabeledRoom[] labeledRooms;
    bool centerRoomTrue = false;
    bool deadEndMask = false;

    public override Room createRoom(ExitConstraint requiredExits)
    {

        List<Room> validRooms = new List<Room>();
        foreach (ExampleLabeledRoom labeledRoom in labeledRooms)
        {
            if (roomMeetsConstraints(labeledRoom, requiredExits))
            {
                validRooms.Add(labeledRoom);
            }
        }
        //centerRoomTrue = false;
        return validRooms[Random.Range(0, validRooms.Count)].createRoom(requiredExits);
    }

    public override void fillRoom(LevelGenerator ourGenerator, ExitConstraint requiredExits)
    {

        for (int x = 0; x < LevelGenerator.ROOM_WIDTH; x++)
        {
            for (int y = 0; y < LevelGenerator.ROOM_HEIGHT; y++)
            {

                if (requiredExits.upExitRequired && isUpExit(x, y))
                {
                    continue;
                }
                if (requiredExits.rightExitRequired && isRightExit(x, y))
                {
                    continue;
                }
                if (requiredExits.downExitRequired && isDownExit(x, y))
                {
                    continue;
                }
                if (requiredExits.leftExitRequired && isLeftExit(x, y))
                {
                    continue;
                }
                if (x > 0 && x < LevelGenerator.ROOM_WIDTH - 1 && y > 0 && y < LevelGenerator.ROOM_HEIGHT - 1)
                {
                    continue;
                }

                Tile.spawnTile(ourGenerator.normalWallPrefab, transform, x, y);
            }

        }
    }

    public bool roomMeetsConstraints(ExampleLabeledRoom roomToTest, ExitConstraint requiredExits)
    {
        if (requiredExits.upExitRequired && !roomToTest.hasUpExit)
        {
            return false;
        }
        if (requiredExits.rightExitRequired && !roomToTest.hasRightExit)
        {
            return false;
        }
        if (requiredExits.downExitRequired && !roomToTest.hasDownExit)
        {
            return false;
        }
        if (requiredExits.leftExitRequired && !roomToTest.hasLeftExit)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.rightExitRequired && !roomToTest.hasUpRightPath)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.downExitRequired && !roomToTest.hasUpDownPath)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.leftExitRequired && !roomToTest.hasUpLeftPath)
        {
            return false;
        }
        if (requiredExits.rightExitRequired && requiredExits.downExitRequired && !roomToTest.hasRightDownPath)
        {
            return false;
        }
        if (requiredExits.rightExitRequired && requiredExits.leftExitRequired && !roomToTest.hasRightLeftPath)
        {
            return false;
        }
        if (requiredExits.downExitRequired && requiredExits.leftExitRequired && !roomToTest.hasDownLeftPath)
        {
            return false;
        }
        if (roomToTest == labeledRooms[4])
        {
            if (!centerRoomTrue)
            {
                centerRoomTrue = true;
            }
            else
            {
                return false;
            }
        }
        if (roomToTest == labeledRooms[1])
        {
            if (!deadEndMask)
            {
                deadEndMask = true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }


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
}
