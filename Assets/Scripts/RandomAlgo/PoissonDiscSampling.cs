using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling
{
	public static List<Vector2> GeneratePoints(List<Vector2> spawnPositions, Vector2 radius, Vector2 sampleRegionSize,
	                                           int numSamplesBeforeRejection = 1) {
		if (radius.x == 0) radius.x = 0.1f;
		float cellSize = radius.x/Mathf.Sqrt(2);
		
		int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x/cellSize), Mathf.CeilToInt(sampleRegionSize.y/cellSize)];
		List<Vector2> points = new List<Vector2>();
		
		foreach (Vector2 spawnPosition in spawnPositions) {
			// Debug.Log(spawnPosition);
			
			FeedPointList(spawnPosition, radius, numSamplesBeforeRejection, grid, cellSize, sampleRegionSize, points);
		}

		return points;
	}

	private static void FeedPointList(Vector2 spawnPosition, Vector2 radius, int numSamplesBeforeRejection, 
	                                  int[,] grid, float cellSize, Vector2 sampleRegionSize, List<Vector2> points) {
		
		List<Vector2> spawnPoints = new List<Vector2>();
		spawnPoints.Add(spawnPosition);
		while (spawnPoints.Count > 0) {
			int spawnIndex = Random.Range(0,spawnPoints.Count);
			Vector2 spawnCentre = spawnPoints[spawnIndex];
			bool candidateAccepted = false;

			for (int i = 0; i < numSamplesBeforeRejection; i++)
			{
				float angle = Random.value * Mathf.PI * 2;
				Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
				Vector2 candidate = spawnCentre + dir * Random.Range(radius.x, radius.y);
				if (IsValid(candidate, cellSize, radius.x, points, grid, sampleRegionSize)) {
					points.Add(candidate);
					spawnPoints.Add(candidate);
					grid[(int)(candidate.x/cellSize),(int)(candidate.y/cellSize)] = points.Count;
					candidateAccepted = true;
					break;
				}
			}
			if (!candidateAccepted) {
				spawnPoints.RemoveAt(spawnIndex);
			}
		}
	}

	static bool IsValid(Vector2 candidate, float cellSize, float radius, List<Vector2> points, int[,] grid, Vector2 sampleRegionSize) {
		if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 
		    && candidate.y < sampleRegionSize.y) {
			int cellX = (int)(candidate.x/cellSize);
			int cellY = (int)(candidate.y/cellSize);
			int searchStartX = Mathf.Max(0,cellX -2);
			int searchEndX = Mathf.Min(cellX+2,grid.GetLength(0)-1);
			int searchStartY = Mathf.Max(0,cellY -2);
			int searchEndY = Mathf.Min(cellY+2,grid.GetLength(1)-1);

			for (int x = searchStartX; x <= searchEndX; x++) {
				for (int y = searchStartY; y <= searchEndY; y++) {
					int pointIndex = grid[x,y]-1;
					if (pointIndex != -1) {
						float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
						if (sqrDst < radius*radius) {
							return false;
						}
					}
				}
			}
			return true;
		}
		return false;
	}
	
}
