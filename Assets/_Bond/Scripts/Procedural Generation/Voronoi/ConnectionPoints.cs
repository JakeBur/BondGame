using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConnectionPoints", menuName = "ScriptableObjects/ConnectionPoints")]
public class ConnectionPoints : ScriptableObject
{
	public string patternName;
    public List<Vector2Pair> points = new List<Vector2Pair>();
	public List<int> seeds = new List<int>();
}

[System.Serializable]
public class Vector2Pair
{
	public Vector2 start;
	public Vector2 end;
	public Biome biome;
}