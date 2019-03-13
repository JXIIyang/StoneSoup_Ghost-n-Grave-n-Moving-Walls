using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueRoomPicker : Room
{

    public TeagueRoom[] preLabeledRooms;

    public override Room createRoom(ExitConstraint requiredExits)
    {
        List<Room> validRooms = new List<Room>();
        foreach (TeagueRoom labeledRoom in preLabeledRooms)
        {
            if (roomMeetsConstraints(labeledRoom, requiredExits))
            {
                validRooms.Add(labeledRoom);
            }
        }
        return validRooms[Random.Range(0, validRooms.Count)].createRoom(requiredExits);
    }

    public bool roomMeetsConstraints(TeagueRoom testRoom, ExitConstraint requiredExits)
    {
        if (requiredExits.upExitRequired && !testRoom.hasUpExit)
        {
            return false;
        }
        if (requiredExits.rightExitRequired && !testRoom.hasRightExit)
        {
            return false;
        }
        if (requiredExits.downExitRequired && !testRoom.hasDownExit)
        {
            return false;
        }
        if (requiredExits.leftExitRequired && !testRoom.hasLeftExit)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.rightExitRequired && !testRoom.hasUpRightPath)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.downExitRequired && !testRoom.hasUpDownPath)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.leftExitRequired && !testRoom.hasUpLeftPath)
        {
            return false;
        }
        if (requiredExits.rightExitRequired && requiredExits.downExitRequired && !testRoom.hasRightDownPath)
        {
            return false;
        }
        if (requiredExits.rightExitRequired && requiredExits.leftExitRequired && !testRoom.hasRightLeftPath)
        {
            return false;
        }
        if (requiredExits.downExitRequired && requiredExits.leftExitRequired && !testRoom.hasDownLeftPath)
        {
            return false;
        }

        return true;
    }

}
