using System.Collections.Generic;
using UnityEngine;

public static class NoisePosSampling 
{

	public static List<Vector2> GeneratePoints(List<Vector2> spawnPositions, int xNbRegionSplit, int nbRegionSplit, float radiusValidZone, Vector2 sampleRegionSize) {
		// Vector3 sampleRegionSize = planRegion.GetComponent<MeshRenderer>().bounds.size;
		List<Vector2> points = new List<Vector2>();
		Vector2 chunk = new Vector2(sampleRegionSize.x / xNbRegionSplit, sampleRegionSize.y / nbRegionSplit);
		for (int i = 0; i < xNbRegionSplit * nbRegionSplit; i++) {
			int diff = i / nbRegionSplit;
			Vector2 chunkPos = new Vector2(diff * chunk.x , i % nbRegionSplit * chunk.y );
			Vector2 candidate = chunkPos + new Vector2(Random.Range(0.0f, chunk.x), Random.Range(0.0f, chunk.y));

			foreach (Vector2 spawnPosition in spawnPositions) {
				float sqrDst = (candidate - spawnPosition).sqrMagnitude; 
				if (sqrDst < radiusValidZone * radiusValidZone) {
					points.Add(candidate);
					break;
				}
			}
		}
		return points;
	}

}