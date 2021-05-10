﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VoronoiPCG : MonoBehaviour
{
	[Header("Set-up")]
	public int gameSeed;
	public Vector2Int imageDim;
	public int coarseRegionAmount;
	public int fineRegionAmount;
	public int adjacencyCount;
	public bool drawByDistance = false;
	public int increment;
	
	public List<ConnectionPoints> connectionPoints;

	public Terrain terrain;
	public GameObject EncounterPosFinderObj;
	public NavMeshSurface navMesh;
	public GameObject LoadingUI;
	public Slider progressBar;
	
	private EncounterPositionFinder encounterPositionFinder;

	List<Cell> coarseVisitedCells = new List<Cell>(); 
	List<Cell> fineVisitedCells = new List<Cell>();
	List<Cell> borderCells = new List<Cell>();
	
	[Header("Terrain Debugging Colors")]
	public Color32 forestColor;
	public Color32 forestBorderColor;
	public Color32 marshColor;
	public Color32 marshBorderColor;
	public Color32 meadowsColor;
	public Color32 meadowsBorderColor;
	public Color32 corruptionColor;
	public Color32 corruptionBorderColor;

	[Header("Stuff To Spawn")]
	public GameObject playerSpawnPoint;
	public GameObject levelExit;
	public GameObject Shopkeeper;
	
	public int numberOfCombat;
	public int numberOfCombatIndicators;
	public List<Encounter> combatEncounters = new List<Encounter>();
	public List<GameObject> noRotationItems = new List<GameObject>();

	public int numberOfCreature;
	public GameObject FragariaEncounter;
	public GameObject AquaphimEncounter;
	public GameObject SheriffEncounter;
	public GameObject PunchySnailEncounter;

	public BiomeObjects meadowsObjects;
	public BiomeObjects forestObjects;
	public BiomeObjects marshObjects;
	public BiomeObjects corruptionObjects;
	
	public GameObject Parent;
	
	private float timerStart;
	private float timerEnd;	

	[ContextMenu("Run Code")]
	public bool InitializeGenerator()
	{
		//Show Loading Screen
		LoadingUI.SetActive(true);
		fineVisitedCells = new List<Cell>();
		borderCells = new List<Cell>();
		encounterPositionFinder = EncounterPosFinderObj.GetComponent<EncounterPositionFinder>();
		// GetComponent<SpriteRenderer>().sprite = Sprite.Create((drawByDistance ? GetDiagramByDistance() : GetDiagram()), new Rect(0, 0, imageDim.x, imageDim.y), Vector2.one * 0.5f);
		StartCoroutine(GenerateLevel());
		
		return true;
	}
	
	//Generate the Voronoi Diagram, then the map based on it
	IEnumerator GenerateLevel()
	{
		progressBar.value = 0;
		terrain.gameObject.GetComponent<TerrainCollider>().enabled = true;
		
		//Get random seed and set it
		gameSeed = Random.Range(0,999999);
		ConnectionPoints connection = connectionPoints[Random.Range(0, connectionPoints.Count)];
		//gameSeed = connection.seeds[Random.Range(0, connections.seeds.count)];
		Random.InitState(gameSeed);
		Debug.Log("SEED INFO : " + connection.patternName + " " + gameSeed);
		yield return null;
		timerStart = Time.realtimeSinceStartup;
		
		Cell[] coarseCells = new Cell[coarseRegionAmount];
		Cell[] fineCells = new Cell[fineRegionAmount];
		//generate sets of random points for voronoi diagram
		for(int i = 0; i < coarseRegionAmount; i++)
		{
			coarseCells[i] = new Cell();
			coarseCells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
			coarseCells[i].index = i;
		}

		for(int i = 0; i < fineRegionAmount; i++)
		{
			fineCells[i] = new Cell();
			fineCells[i].center = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
			fineCells[i].index = i;
		}
		progressBar.value = 5;
		yield return null;
		//relax points to normalize center points, allowing for generally better borders
		relax(coarseCells);
		// relax(fineCells);

		//connect cells using set pattern
		connectCells(coarseCells, connection);

		progressBar.value = 10;
		yield return null;
		for(int i = 0; i < fineCells.Length; i++)
		{
			//every fine cell gets the biome of its closest coarse cell
			Biome b = coarseCells[GetClosestCellIndex((int)fineCells[i].center.x, (int)fineCells[i].center.y, coarseCells)].biome;
			fineCells[i].biome = b; 
			if(b != Biome.EMPTY)
			{
				fineVisitedCells.Add(fineCells[i]);
			}
		}
		progressBar.value = 20;
		yield return null;
		// Generate the borders for the fine cells
		foreach(Cell c in fineVisitedCells)
		{
			List<Cell> borderCells = GetClosestKCells(c.index, adjacencyCount, fineCells);
			//Debug.Log("Border Cells" + borderCells[0]);
			foreach(Cell b in borderCells)
			{
				if(b.biome == Biome.EMPTY)
				{
					switch(c.biome)
					{
						case Biome.FOREST:
							b.biome = Biome.FOREST_BORDER;
							break;
						case Biome.MEADOWS:
							b.biome = Biome.MEADOWS_BORDER;
							break;
						case Biome.MARSH:
							b.biome = Biome.MARSH_BORDER;
							break;
						case Biome.CORRUPTION:
							b.biome = Biome.CORRUPTION_BORDER;
							break;
						default:
							b.biome = Biome.CORRUPTION_BORDER;
							break;
					}
				}
			}
		}
		progressBar.value = 40;
		//Go through the map and set cell biomes, sizes, and pixels
		yield return null;

		Color[] pixelColors = new Color[imageDim.x * imageDim.y];
		
		TerrainData terrainData = terrain.terrainData;

		terrainData.treeInstances = new TreeInstance[terrainData.detailWidth * terrainData.detailHeight];
		float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        
		terrain.Flush();
		float[, ,] alphaMapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
		var detailMapData = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, 0);
		
		float tempDistance;

		for(int y = 0; y < imageDim.y; y++)
		{
			for(int x = 0; x < imageDim.x; x++)
			{
				int xSwap = y;
				int ySwap = x;

				int index = y * imageDim.y + x;
				int fineIndex = GetClosestCellIndex(x, y, fineCells);
				Biome b = fineCells[fineIndex].biome;
				fineCells[fineIndex].size++;
				fineCells[fineIndex].pixels.Add(new Vector2(xSwap,ySwap));

				coarseCells[GetClosestCellIndex(x, y, coarseCells)].size++;
				pixelColors[index] = GetBiomeColor(b);

				heights[xSwap,ySwap] = 0f;

				Color32 pixelColor = pixelColors[index];
				detailMapData[xSwap,ySwap] = 0;
				//Place trees on borders of biomes
				if(pixelColor.Equals(forestBorderColor) ||
				   pixelColor.Equals(meadowsBorderColor) ||
				   pixelColor.Equals(marshBorderColor) ||
				   pixelColor.Equals(corruptionBorderColor)
				)
				{
					alphaMapData[xSwap,ySwap,2] = 1;
					heights[xSwap,ySwap] = 0.1f;
					if(Random.Range(0,5) == 1){
                        TreeInstance treeTemp = new TreeInstance();
						
                        Vector3 position = new Vector3(xSwap , 0, ySwap);
                        float newX = linearMap(xSwap, 0, terrainData.detailWidth, 0, 1);
                        float newY = linearMap(ySwap, 0, terrainData.detailHeight, 0, 1);
                        treeTemp.position = new Vector3(newY,0,newX);
                        treeTemp.prototypeIndex = 0;
                        treeTemp.widthScale = 1f;
                        treeTemp.heightScale = 1f;
                        treeTemp.color = Color.white;
                        treeTemp.lightmapColor = Color.white;
                        terrain.AddTreeInstance(treeTemp);
                        terrain.Flush();
                    }
				} 
				else if(pixelColor.Equals(forestColor))
				{
					alphaMapData[xSwap,ySwap,0] = 1;
					tempDistance = Vector2.Distance(new Vector2(x,y), fineCells[fineIndex].center);
					
					if(Random.Range(0,100) < linearMap(tempDistance, 3, 15, 0, 80))
					{
						detailMapData[xSwap,ySwap] = 1;
					}
					heights[xSwap,ySwap] = 0.001f;
				} else if(pixelColor.Equals(corruptionColor))
				{
					alphaMapData[xSwap,ySwap,4] = 1;
					heights[xSwap,ySwap] = 0.001f;
					
				} else if(pixelColor.Equals(marshColor))
				{
					alphaMapData[xSwap,ySwap,1] = 1;
										
					tempDistance = Vector2.Distance(new Vector2(x,y), fineCells[fineIndex].center);
					
					if(Random.Range(0,100) < linearMap(tempDistance, 10, 25, 0, 90))
					{
						detailMapData[xSwap,ySwap] = 1;
					}
					heights[xSwap,ySwap] = 0f;
				} 
				else if(pixelColor.Equals(meadowsColor))
				{
					alphaMapData[xSwap,ySwap,3] = 1;

					tempDistance = Vector2.Distance(new Vector2(x,y), fineCells[fineIndex].center);
					
					if(Random.Range(0,100) < linearMap(tempDistance, 2, 10, 0, 100))
					{
						detailMapData[xSwap,ySwap] = 1;
					}
					heights[xSwap,ySwap] = 0.001f;
				}
				else 
				{
					heights[xSwap,ySwap] = 0.1f;
					
				}
			}
		}
		
		progressBar.value = 80;
		yield return null;

		terrainData.SetHeights(0, 0, heights);
		terrainData.SetAlphamaps(0, 0, alphaMapData);
		terrainData.SetDetailLayer(0, 0, 0, detailMapData);
		
		PlaceEncounters(fineCells);
		navMesh.BuildNavMesh();

		for(int x = 0; x < imageDim.x; x++)
		{
			for(int y = 0; y < imageDim.y; y++)
			{
				if(heights[x,y] == 0.1f)
				{
					heights[x,y] = 0.001f;
				}
			}
		}
	
		terrainData.SetHeights(0, 0, heights);
		

		Texture2D tex = GetImageFromColorArray(pixelColors);
		//Draw Coarse Center points
		// foreach(Cell c in coarseCells)
		// {
		// 	int index = (int)c.center.x * imageDim.x + (int)c.center.y;
		// 	pixelColors[index] = Color.red;
		// }

		progressBar.value = 100;
		//Debug.Log("Finished : " + (Time.realtimeSinceStartup - timerStart));
		LoadingUI.SetActive(false);
		//terrain.gameObject.GetComponent<TerrainCollider>().enabled = false;

		PersistentData.Instance.isGeneratorDone = true;
		yield return null;
	}


	//Draws between the start and end cell to get next cell on path
	Cell FindNextCell(Cell[] cells, Cell startingCell, Cell endingCell, int _increment)
	{
		Vector2 startPos = startingCell.center;
		startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
		
		Cell newCell = cells[GetClosestCellIndex((int)startPos.x, (int) startPos.y, cells)];
		while(newCell == startingCell)
		{
			startPos = Vector2.MoveTowards(startPos, endingCell.center, _increment);
			newCell = cells[GetClosestCellIndex((int)startPos.x, (int) startPos.y, cells)];
		}
		return newCell;
	}

	//Gets connection points and uses the FindNextCell function to draw lines between them
	public void connectCells(Cell[] cells, ConnectionPoints _connection )
	{
		//pick random set of connection points
		ConnectionPoints connection = _connection;
		
		//iterate through a set of points adding cells to a list and setting their biomes
		foreach(Vector2Pair v in connection.points)
		{
			
			Cell newCell = cells[GetClosestCellIndex((int) v.start.x, (int)v.start.y, cells)];
			newCell.biome = v.biome;
			coarseVisitedCells.Add(newCell);

			Cell endCell = cells[GetClosestCellIndex((int) v.end.x, (int)v.end.y, cells)];
			endCell.biome = v.biome;
			coarseVisitedCells.Add(newCell);

			while(newCell != endCell) 
			{
				newCell = FindNextCell(cells, newCell, endCell, increment);
				newCell.biome = v.biome;
				coarseVisitedCells.Add(newCell);
			}
		}
	}

	//Places spawnPoint, exit, creatures, and enemies
	void PlaceEncounters(Cell[] cells)
	{
		if(Parent != null)
		{
			//Destroy(Parent);
		}
		GameObject _parent = new GameObject();
		Parent = _parent;

		//Get Possible spawn points for encounters/environmental objects
		List<List<Vector2>> listOfList = encounterPositionFinder.GetPoints(new Vector3(0,37.5f,0), 512, 2);
		List<Vector2> possibleEncounterPositions = listOfList[0];
		List<Vector2> possibleEnviornmentalObjectLocations = listOfList[1];
		List<GameObject> placedEncounters = new List<GameObject>();
		//place player spawn point and level exit
		var spawnPoint = Instantiate(playerSpawnPoint, new Vector3(possibleEnviornmentalObjectLocations[0].x, 0, possibleEnviornmentalObjectLocations[0].y), Quaternion.identity, Parent.transform);
		possibleEnviornmentalObjectLocations.RemoveAt(0);
		var exit = Instantiate(levelExit, new Vector3(possibleEncounterPositions[possibleEncounterPositions.Count-1].x, 0, possibleEncounterPositions[possibleEncounterPositions.Count-1].y), Quaternion.identity, Parent.transform);
		possibleEncounterPositions.RemoveAt(possibleEncounterPositions.Count-1);
		placedEncounters.Add(spawnPoint);
		placedEncounters.Add(exit);


		//make sure shop keeper doesnt spawn too close to start or exit
		bool overlap = true;
		int encounterPositionsIndex;
		Vector2 randomPos;
		while(overlap)
		{				
			if(possibleEncounterPositions.Count < 1) break;

			encounterPositionsIndex = Random.Range(0, possibleEncounterPositions.Count);
			randomPos = possibleEncounterPositions[encounterPositionsIndex];
			overlap = false;
			foreach(GameObject e in placedEncounters){
				//If chosen cell is too close to another encounter, remove it from the possible encounter cells
				if(Vector3.Distance(e.transform.position, new Vector3(randomPos.x, 0, randomPos.y)) < 55)
				{
					possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
					overlap = true;
					break;
				}
				
			}

			if(overlap)
			{
				continue;
			}



			var shop = Instantiate(Shopkeeper, new Vector3(randomPos.x, 0,randomPos.y), Quaternion.Euler(new Vector3(0,45,0)), Parent.transform);
			possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
			placedEncounters.Add(Shopkeeper);
		}
		//place random encounters on centerpoints of coarse cells
		coarseVisitedCells.Sort((x,y)=> x.size.CompareTo(y.size));
		List<Cell> encounterCells = new List<Cell>(coarseVisitedCells);

		

		

		//Loop to place combat encounters
		for(int i = 0; i < numberOfCombat; i++)
		{
			overlap = true;
			if(possibleEncounterPositions.Count < 1) break;

			//Continue until we find a place to put encounter
			while(overlap)
			{				
				if(possibleEncounterPositions.Count < 1) break;

				encounterPositionsIndex = Random.Range(0, possibleEncounterPositions.Count);
				randomPos = possibleEncounterPositions[encounterPositionsIndex];
				overlap = false;
				foreach(GameObject e in placedEncounters){
					//If chosen cell is too close to another encounter, remove it from the possible encounter cells
					if(Vector3.Distance(e.transform.position, new Vector3(randomPos.x, 0, randomPos.y)) < 65)
					{
						possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
						overlap = true;
						break;
					}
					
				}

				if(overlap)
				{
					continue;
				}

				//Choose an encounter to spawn
				int encounterIndex = Random.Range(0, combatEncounters.Count);

				//Spawn the encounter
				GameObject enemyEncounter = Instantiate(
					combatEncounters[encounterIndex].encounter, 
					new Vector3(randomPos.x, 0, randomPos.y), 
					Quaternion.identity, Parent.transform
				);
				placedEncounters.Add(enemyEncounter);

				//Place indicators for the new encounter randomly in a circle
				for(int j = 0; j < numberOfCombatIndicators; j++)
				{
					Vector2 randomUnitCirclePoint = Random.insideUnitCircle;
					randomUnitCirclePoint *= Random.Range(40,70);
					Vector3 pos = new Vector3(randomUnitCirclePoint.x + randomPos.x, 0, randomUnitCirclePoint.y + randomPos.y);
					Instantiate(combatEncounters[encounterIndex].indicators[Random.Range(0,combatEncounters[encounterIndex].indicators.Count)], pos, Quaternion.identity, enemyEncounter.transform);
				}
				//Remove this cell from the list of options to avoid overlap
				possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
			}
		}

		//Loop to place creature encounters
		for(int i = 0; i < numberOfCreature; i++)
		{

			overlap = true;
			if(possibleEncounterPositions.Count < 1) break;

			//Continue until we find a place to put encounter
			while(overlap)
			{				
				if(possibleEncounterPositions.Count < 1) break;

				encounterPositionsIndex = Random.Range(0, possibleEncounterPositions.Count);
				randomPos = possibleEncounterPositions[encounterPositionsIndex];
				overlap = false;
				foreach(GameObject e in placedEncounters)
				{
					//If chosen cell is too close to another encounter, remove it from the possible encounter cells
					if(Vector3.Distance(e.transform.position, new Vector3(randomPos.x, 0, randomPos.y)) < 30)
					{
						possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
						overlap = true;
						break;
					}
				}

				if(overlap)
				{
					continue;
				}

				Biome b = cells[GetClosestCellIndex((int)randomPos.x,(int)randomPos.y, cells)].biome;

				GameObject toPlace; 

				float chance = Random.Range(0f, 1f);
				float angle;

				switch(b)
				{
					case Biome.FOREST:
						toPlace = PunchySnailEncounter;
						angle = Random.Range(0,360);
						break;
					case Biome.MEADOWS:
						toPlace = FragariaEncounter;
						angle = 125;
						break;
					case Biome.MARSH:
						toPlace = AquaphimEncounter;
						angle = 0;
						break;
					// case Biome.CORRUPTION:
						
					// 	break;
					default:
						toPlace = FragariaEncounter;
						angle = 125;
						break;
				}

				if(chance <= .3)
				{
					toPlace = SheriffEncounter;
					angle = 0;
				}

				//Spawn creature encounter
				GameObject creatureEncounter = Instantiate(
					toPlace, 
					new Vector3(randomPos.x, 0, randomPos.y), 
					Quaternion.Euler(0,angle,0) , Parent.transform
				);
				placedEncounters.Add(creatureEncounter);

				//Remove position from possible encounter locations
				possibleEncounterPositions.RemoveAt(encounterPositionsIndex);
			}
		}

		BiomeObjects currentBiomeObj;
		Biome currBiome;
		//Iterate through possible enviornmental object placement locations and decide what to place at each location
		for(int i = 0; i < possibleEnviornmentalObjectLocations.Count; i++)
		{
			overlap = false;
			randomPos = possibleEnviornmentalObjectLocations[i];
			foreach(GameObject e in placedEncounters){
				//If chosen cell is too close to another encounter, remove it from the possible encounter cells
				if(Vector3.Distance(e.transform.position, new Vector3(randomPos.x, 0, randomPos.y)) < 5)
				{
					possibleEnviornmentalObjectLocations.RemoveAt(i);
					overlap = true;
					break;
				}
			}
			if(overlap)
			{
				continue;
			}

			float randomNum = Random.Range(0f,100f);

			currBiome = cells[GetClosestCellIndex((int)randomPos.x, (int)randomPos.y, cells)].biome; 
			// Debug.Log("Curr Biome: " + currBiome);
			switch (currBiome)
			{
				case Biome.FOREST:
					currentBiomeObj = forestObjects;
					break;

				case Biome.MEADOWS:
					currentBiomeObj = meadowsObjects;
					break;

				case Biome.MARSH :
					currentBiomeObj = marshObjects;
					break;
				default:
					currentBiomeObj = null;
					break;
			}
			if(currentBiomeObj == null)
			{
				continue;
			}
			//each asset in the biomespecificassetlist has a weight that is being checked here to decide what to spawn
			float lastPercent = 0;
			foreach(BiomeSpecificAssetList b in currentBiomeObj.Assets)
			{
				if(lastPercent == 0)
				{
					if(randomNum < b.percentage + lastPercent)
					{
						float yoffset = 0;
						if(currentBiomeObj == marshObjects)
						{
							yoffset = -0.6f;
						}

						Instantiate(b.objects[Random.Range(0, b.objects.Count)],
							new Vector3(randomPos.x, yoffset, randomPos.y),
							Quaternion.Euler(0,Random.Range(0,360), 0), 
							Parent.transform);
						break;
					}
					
				} else if(lastPercent < randomNum && randomNum < b.percentage + lastPercent)
				{
					float yoffset = 0;
					if(currentBiomeObj == marshObjects)
					{
						yoffset = -0.6f;
					}
					Instantiate(b.objects[Random.Range(0, b.objects.Count)],
						new Vector3(randomPos.x, yoffset, randomPos.y),
						Quaternion.Euler(0,Random.Range(0,360), 0), 
						Parent.transform);
					break;
				}
				lastPercent += b.percentage;
			}
		}
	}
	
	//returns a color for the debug map based on biome
	Color GetBiomeColor(Biome b) 
	{
		switch(b)
		{
			case Biome.EMPTY :
				return Color.black;
			case Biome.FOREST :
				return forestColor;
			case Biome.MARSH :
				return marshColor;
			case Biome.MEADOWS :
				return meadowsColor;
			case Biome.CORRUPTION :
				return corruptionColor;
			case Biome.CORRUPTION_BORDER :
				return corruptionBorderColor;
			case Biome.MEADOWS_BORDER :
				return meadowsBorderColor;
			case Biome.FOREST_BORDER :
				return forestBorderColor;
			case Biome.MARSH_BORDER :
				return marshBorderColor;
		}
		return Color.black;
	}

	//iterates through all cells and finds the closest cell to a given x,y coordinate
	int GetClosestCellIndex(int x, int y, Cell[] cells)
	{
		float smallestDst = float.MaxValue;
		int index = 0;
		Vector2 coords = new Vector2(x, y);
		for(int i = 0; i < cells.Length; i++)
		{
			if (Vector2.Distance(coords, cells[i].center) < smallestDst)
			{
				smallestDst = Vector2.Distance(coords, cells[i].center);
				index = i;
			}
		}
		return index;
	}

	//finds the closest k cells to a given cell. Used for filling in borders and adjacencies
	List<Cell> GetClosestKCells(int myIndex, int k, Cell[] cells)
	{
		List<Cell> kClosestCells = new List<Cell>(k);

		Vector2 coords = new Vector2(cells[myIndex].center.x, cells[myIndex].center.y);
		for(int i = 0; i < cells.Length; i++)
		{
			if(myIndex != i)
			{
				float iDistance = Vector2.Distance(coords, cells[i].center);
				if(kClosestCells.Count < k)
				{	
					kClosestCells.Add(cells[i]);
					kClosestCells[kClosestCells.Count - 1].distance = iDistance;
					kClosestCells.Sort((x,y)=> x.distance.CompareTo(y.distance));
				}
				else 
				{
					if (iDistance < Vector2.Distance(coords, kClosestCells[kClosestCells.Count - 1].center))
					{
						
						kClosestCells[kClosestCells.Count - 1] = cells[i];
						kClosestCells[kClosestCells.Count - 1].distance = iDistance;
						kClosestCells.Sort((x,y)=> x.distance.CompareTo(y.distance));
					}
				}
			}
		}

		return kClosestCells;
	}
	
	//generates a texture2d that was used for debug processes from the array of pixels generated
	Texture2D GetImageFromColorArray(Color[] pixelColors)
	{
		Texture2D tex = new Texture2D(imageDim.x, imageDim.y);
		tex.filterMode = FilterMode.Point;
		tex.SetPixels(pixelColors);
		tex.Apply();
		return tex;
	}

	//translates a value between a min and max to a new value based on a new min and max
	public float linearMap(float value, float inputLow, float inputHigh, float outputLow, float outputHigh) 
    {
        return outputLow + (outputHigh - outputLow) * (value - inputLow) / (inputHigh - inputLow);
    }

	//basically k-means clustering, counts the amount of pixels ("hits") nearby to help recenter itself
	public void relax(Cell[] cells)
	{
		Vector2Int[] contributions = new Vector2Int[cells.Length];
		int[] hits = new int[cells.Length];
		Vector2Int V2here = new Vector2Int();
		for(int x = 0; x < imageDim.x; x++)
		{
			for(int y = 0; y < imageDim.y; y++)
			{
				V2here.x = x;
				V2here.y = y;

				int index = GetClosestCellIndex(V2here.x, V2here.y, cells);
				contributions[index] += V2here;
				hits[index]++;
			}
		}

		for(int i = 0; i < cells.Length; i++)
		{
			if(hits[i] > 0)
				cells[i].center = contributions[i] / hits[i];
		}
	}
}

//class used to help store voronoi "cells"
public class Cell 
{
	public int size = 0;
	public int index;
	public Vector2 center;
	public Biome biome = Biome.EMPTY;
	public float distance;
	public List<Vector2> pixels = new List<Vector2>();
}

public enum Biome 
{
	EMPTY,
	FOREST,
	FOREST_BORDER,
	MARSH,
	MARSH_BORDER,
	MEADOWS,
	MEADOWS_BORDER,
	CORRUPTION,
	CORRUPTION_BORDER,
}