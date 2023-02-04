using System.Collections.Generic;
using UnityEngine;

public class FairyRingSampling
{

    public static List<Vector2> GeneratePoints(List<Vector2> spawnPositions, Vector2 rangeRadius,int nbMushroomsOnRing, bool isDoubleRing, Vector2 sampleRegionSize) {
        // Vector3 sampleRegionSize = planRegion.GetComponent<MeshRenderer>().bounds.size;
        List<Vector2> points = new List<Vector2>();
        float radiusZone = 360f / nbMushroomsOnRing / 360f;
        
        foreach (Vector2 spawnPosition in spawnPositions) {
            Vector2 secondRingPosition = GenerateSecondRing(rangeRadius.y, spawnPosition);
            points.AddRange(RandomPointsFromCenterPosition(spawnPosition, secondRingPosition, rangeRadius,
                nbMushroomsOnRing, sampleRegionSize, radiusZone, isDoubleRing));
            if (isDoubleRing) {
                points.AddRange(RandomPointsFromCenterPosition(secondRingPosition, spawnPosition, rangeRadius,
                    nbMushroomsOnRing, sampleRegionSize, radiusZone, isDoubleRing));
            }
        }
        return points;
    }

    private static List<Vector2> RandomPointsFromCenterPosition(Vector2 positionCenter, Vector2 positionOrbit, Vector2 radius, int nbMushroomsOnRing, Vector2 sampleRegionSize, float radiusZone, bool isDoubleRing) {
        List<Vector2> randomPoints = new List<Vector2>();
        for (int i = 0; i < nbMushroomsOnRing; i++) {
            float angle = radiusZone * i * Mathf.PI * 2;

            float randomAngle = Random.Range(angle, angle + radiusZone * Mathf.PI * 2);
            Vector2 randomDir = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));
            Vector2 candidate = positionCenter + randomDir * Random.Range(radius.x, radius.y);
            if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0
                && candidate.y < sampleRegionSize.y) {
                if (isDoubleRing) {
                    float sqrDst = (candidate - positionOrbit).sqrMagnitude; 
                    if (sqrDst > radius.x * radius.x) {
                        randomPoints.Add(candidate);
                    }
                }
                else {
                    randomPoints.Add(candidate);
                }
            }

        }
        return randomPoints;
    }

    private static Vector2 GenerateSecondRing(float radius, Vector2 spawnPosition) {
        float secondRingAngle = Random.value * Mathf.PI * 2;
        Vector2 dir = new Vector2(Mathf.Sin(secondRingAngle), Mathf.Cos(secondRingAngle));
        return spawnPosition + dir * radius;
    }

    private static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize) {
        if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 &&
            candidate.y < sampleRegionSize.y) {

            return true;
        }
        return false;
    }
    
}
