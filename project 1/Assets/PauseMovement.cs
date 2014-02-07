using UnityEngine;
using System.Collections;

public static class PauseMovement
{
static bool isStopped;
static bool isFrozen;

static PauseMovement() {
	isStopped = false;
	isFrozen = false;
}

public static void freezeTime() {
				isFrozen = true;
		}

public static void unfreezeTime() {
				isFrozen = false;
		}

public static void stopEverything() {
				isStopped = true;
		}

public static void startEverything() {
				isStopped = false;
		}

public static bool isTimeFrozen() {
				return isFrozen;
		}

public static bool isTimeStopped() {
				return isStopped;
		}

}