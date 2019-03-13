using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleanorRoomPicker : Room
{
    public List<EleanorLabeledRoom> LabeledRooms;

    public override Room createRoom(ExitConstraint requiredExits)
    {
        List<Room> validRooms = new List<Room>();
        foreach (EleanorLabeledRoom labeledRoom in LabeledRooms) {
            if (roomMeetsConstraints(labeledRoom, requiredExits)) {
                validRooms.Add(labeledRoom);
            }
        }
        return validRooms[Random.Range(0, validRooms.Count)].createRoom(requiredExits);
        
    }
    
    public bool roomMeetsConstraints(EleanorLabeledRoom roomToTest, ExitConstraint requiredExits) {
        if (requiredExits.upExitRequired && !roomToTest.HasUpExit) 
        {
            return false;
        }
        if (requiredExits.rightExitRequired && !roomToTest.HasRightExit) 
        {
            return false;
        }
        if (requiredExits.downExitRequired && !roomToTest.HasDownExit) 
        {
            return false;
        }
        if (requiredExits.leftExitRequired && !roomToTest.HasLeftExit) 
        {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.rightExitRequired && !roomToTest.HasUpRightPath) {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.downExitRequired && !roomToTest.HasUpDownPath) {
            return false;
        }
        if (requiredExits.upExitRequired && requiredExits.leftExitRequired && !roomToTest.HasUpLeftPath) {
            return false;
        }
        if (requiredExits.rightExitRequired && requiredExits.downExitRequired && !roomToTest.HasRightDownPath) {
            return false;
        }
        if (requiredExits.rightExitRequired && requiredExits.leftExitRequired && !roomToTest.HasRightLeftPath) {
            return false;
        }
        if (requiredExits.downExitRequired && requiredExits.leftExitRequired && !roomToTest.HasDownLeftPath) {
            return false;
        }

        return true;
    }
}
