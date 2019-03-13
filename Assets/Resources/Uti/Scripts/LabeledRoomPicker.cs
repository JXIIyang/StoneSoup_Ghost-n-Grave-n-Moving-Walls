using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabeledRoomPicker : Room
{
    public ExampleLabeledRoom[] rooms;

    public override Room createRoom(ExitConstraint requiredExits)
    {
        List<Room> validRooms = new List<Room>();
        foreach(ExampleLabeledRoom labeledRoom in rooms)
        {
            if (roomMeetsConstraints(labeledRoom, requiredExits))
            {
                validRooms.Add(labeledRoom);
            }
        }
        return validRooms[Random.Range(0, validRooms.Count)].createRoom(requiredExits);
    }

    public bool roomMeetsConstraints(ExampleLabeledRoom roomToTest, ExitConstraint requiredExits)
    {
        if(requiredExits.upExitRequired && !roomToTest.hasUpExit)
        {
            return false;
        }
        if(requiredExits.rightExitRequired && !roomToTest.hasRightExit)
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

        if(requiredExits.upExitRequired && requiredExits.rightExitRequired && !roomToTest.hasUpRightPath)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.leftExitRequired && !roomToTest.hasUpLeftPath)
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.downExitRequired && !roomToTest.hasUpDownPath)
        {
            return false;
        }
        if (requiredExits.leftExitRequired && requiredExits.rightExitRequired && !roomToTest.hasRightLeftPath)
        {
            return false;
        }

        if (requiredExits.downExitRequired && requiredExits.rightExitRequired && !roomToTest.hasRightDownPath)
        {
            return false;
        }
        if (requiredExits.downExitRequired && requiredExits.leftExitRequired && !roomToTest.hasDownLeftPath)
        {
            return false;
        }

        if (requiredExits.upExitRequired && requiredExits.rightExitRequired && !roomToTest.hasUpRightPath)
        {
            return false;
        }

        return true;
    }

}
