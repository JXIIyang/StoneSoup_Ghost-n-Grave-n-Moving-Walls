using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeRoomPicker : Room {

	public LabeledRoom[] labeledRooms;

	public override Room createRoom(ExitConstraint requiredExits) {

		List<Room> validRooms = new List<Room>();
		foreach (LabeledRoom labeledRoom in labeledRooms) {
			if (roomMeetsConstraints(labeledRoom, requiredExits)) {
				validRooms.Add(labeledRoom);
			}
		}
		return validRooms[Random.Range(0, validRooms.Count)].createRoom(requiredExits);
	}

	public bool roomMeetsConstraints(LabeledRoom roomToTest, ExitConstraint requiredExits) {
		if (requiredExits.upExitRequired && !roomToTest.hasUpExit) {
			return false;
		}
		if (requiredExits.rightExitRequired && !roomToTest.hasRightExit) {
			return false;
		}
		if (requiredExits.downExitRequired && !roomToTest.hasDownExit) {
			return false;
		}
		if (requiredExits.leftExitRequired && !roomToTest.hasLeftExit) {
			return false;
		}
		if (requiredExits.upExitRequired && requiredExits.rightExitRequired && !roomToTest.hasUpRightPath) {
			return false;
		}
		if (requiredExits.upExitRequired && requiredExits.downExitRequired && !roomToTest.hasUpDownPath) {
			return false;
		}
		if (requiredExits.upExitRequired && requiredExits.leftExitRequired && !roomToTest.hasUpLeftPath) {
			return false;
		}
		if (requiredExits.rightExitRequired && requiredExits.downExitRequired && !roomToTest.hasRightDownPath) {
			return false;
		}
		if (requiredExits.rightExitRequired && requiredExits.leftExitRequired && !roomToTest.hasRightLeftPath) {
			return false;
		}
		if (requiredExits.downExitRequired && requiredExits.leftExitRequired && !roomToTest.hasDownLeftPath) {
			return false;
		}

		return true;
	}
}
