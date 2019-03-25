using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeJuRoom : Room{

public override void fillRoom(LevelGenerator ourGenerator, ExitConstraint requiredExits) {

	for (int x = 0; x < LevelGenerator.ROOM_WIDTH; x++) {
		for (int y = 0; y < LevelGenerator.ROOM_HEIGHT; y++) {

			if (requiredExits.upExitRequired && isUpExit(x, y)) {
				continue;
			}
			if (requiredExits.rightExitRequired && isRightExit(x, y)) {
				continue;
			}
			if (requiredExits.downExitRequired && isDownExit(x, y)) {
				continue;
			}
			if (requiredExits.leftExitRequired && isLeftExit(x, y)) {
				continue;
			}
			if (x > 0 && x < LevelGenerator.ROOM_WIDTH-1 && y > 0 && y < LevelGenerator.ROOM_HEIGHT-1) {
				continue;
			}

			Tile.spawnTile(ourGenerator.normalWallPrefab, transform, x, y);
		}

	}
}

public bool isUpExit(int x, int y) {
	return x == LevelGenerator.ROOM_WIDTH / 2 && y == LevelGenerator.ROOM_HEIGHT - 1;
}

public bool isRightExit(int x, int y) {
	return x == LevelGenerator.ROOM_WIDTH - 1 && y == LevelGenerator.ROOM_HEIGHT / 2; 
}

public bool isDownExit(int x, int y) {
	return x == LevelGenerator.ROOM_WIDTH / 2 && y == 0;
}

public bool isLeftExit(int x, int y) {
	return x == 0 && y == LevelGenerator.ROOM_HEIGHT / 2;
}

}