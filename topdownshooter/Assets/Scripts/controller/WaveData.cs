using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData{

	// Information for each level

	// Total number of zombies
	public int amtZombies;

	// Normal zombies
	public int amtNorm;
	// Fast zombies
	public int amtFasts;
	// Bulky zombies
	public int amtRenegades;

	// For now one health, but will find best way to have multiple healths
	// Technically can override this in individual zombie health script
	public int zombieHealth;

	public double spawnInterval;
}
