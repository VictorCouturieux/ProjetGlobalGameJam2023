using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RandomAlgoName { PoissonDisc, Noise, FairyRing }
public enum InitialPointType { Center, Random, FromTransformReference, FromAlgorithmReference }

public class SeedGenerator : MonoBehaviour
{
	[SerializeField] [HideInInspector] private InitialPointType initialPointType;
	[SerializeField] [HideInInspector] private SeedGenerator initialReferencesPoints;
	[SerializeField] [HideInInspector] private Transform transformReference;
	
	[SerializeField] [HideInInspector] private RandomAlgoName algo;
	
	// PoissonDisc Values
	[SerializeField] [HideInInspector] [Range(1, 5)] private int rejectionSamples = 1;
	[SerializeField] [HideInInspector] private Vector2 range = new(5, 7);
	
	// Noise Values
	[SerializeField] [HideInInspector] [Range(1, 40)] private int xNbRegionSplit = 1;
	[SerializeField] [HideInInspector] [Range(1, 40)] private int zNbRegionSplit = 1;
	[SerializeField] [HideInInspector] private float radiusValidZone = 3f;
	
	// FairyRing Values
	[SerializeField] [HideInInspector] private Vector2 ringRangeRadius = new(5, 7);
	[SerializeField] [HideInInspector] [Range(1, 45)] private int nbMushroomsOnRing = 10;
	[SerializeField] [HideInInspector] private bool isDoubleRing;
	
	// Gizmos Values
	[SerializeField] [HideInInspector] private bool showGizmos = true;
	[SerializeField] [HideInInspector] private Color gizmosColor = Color.red;
	[SerializeField] [HideInInspector] private float gizmosRadius = 0.1f;

	[SerializeField] public bool autoUpdate;

	private List<Vector3> _initPositionIntoPlan = new List<Vector3>();
	private Vector3 _transformPos;
	
	private Vector3 _regionSize;
	private List<Vector2> _points;
	public List<Vector3> GetPoints {
		get {
			List<Vector3> pointsXZ = new List<Vector3>();
			if (_points != null && _points.Count != 0) {
				foreach (Vector3 point in _points) {
					pointsXZ.Add(new(point.x, 0, point.y));
				}
			}
			return pointsXZ;
		}
	}

	private void Awake() {
		GenerateSeed();
	}

	private void InitValues() {
		_regionSize = GetComponent<MeshRenderer>().bounds.size;
		_transformPos = transform.position;
		// SavePositionForGizmos();

		if (_initPositionIntoPlan.Count != 0) {
			_initPositionIntoPlan.Clear();
		}

		switch (initialPointType) {
			case InitialPointType.Center :
				_initPositionIntoPlan.Add(Vector3.zero);	
				break;
			case InitialPointType.Random :
				float borderRange = 0;
				if (algo == RandomAlgoName.PoissonDisc) {
					borderRange = range.y;
				}
				float rSpawnX = Random.Range(borderRange, _regionSize.x - borderRange);
				float rSpawnZ = Random.Range(borderRange, _regionSize.z - borderRange);
				Vector3 randomSpawnPos = new Vector3(rSpawnX, 0f, rSpawnZ);
				_initPositionIntoPlan.Add(randomSpawnPos - _regionSize/2);
				break;
			case InitialPointType.FromTransformReference :
				if (transformReference != null) {
					_initPositionIntoPlan.Add(transformReference.position - _transformPos);	
				}
				break;
			case InitialPointType.FromAlgorithmReference :
				if (initialReferencesPoints != null && initialReferencesPoints.GetPoints.Count != 0) {
					foreach (Vector3 refPoint in initialReferencesPoints.GetPoints) {
						_initPositionIntoPlan.Add(refPoint - _regionSize/2);
					}
				}
				break;
		}
	}
	
	public void GenerateSeed() {
		InitValues();
		Vector2 regionSize2d = new(_regionSize.x, _regionSize.z);
		
		List<Vector2> localSpawnPos2dList = new List<Vector2>();
		foreach (Vector3 initPosition in _initPositionIntoPlan) {
			Vector3 localSpawnPosition = _transformPos + initPosition - transform.position + _regionSize / 2;
			localSpawnPos2dList.Add(new(localSpawnPosition.x, localSpawnPosition.z));
		}
		switch (algo) {
		case RandomAlgoName.PoissonDisc :
			_points = PoissonDiscSampling.GeneratePoints(localSpawnPos2dList, range, regionSize2d, rejectionSamples);
			break;
		case RandomAlgoName.Noise :
			_points = NoisePosSampling.GeneratePoints(localSpawnPos2dList, xNbRegionSplit, zNbRegionSplit, radiusValidZone, regionSize2d);
			break;
		case RandomAlgoName.FairyRing :
			_points = FairyRingSampling.GeneratePoints(localSpawnPos2dList, ringRangeRadius, nbMushroomsOnRing, isDoubleRing, regionSize2d);
			break;
		}
	}

	void OnDrawGizmos() {
		if (showGizmos) {
			Gizmos.color = gizmosColor;
			Handles.color = gizmosColor;
			if (_points != null) {
				Vector3 actualTransformPos = transform.position;
				switch (algo) {
				case RandomAlgoName.PoissonDisc :
					foreach (Vector2 point in _points) {
						Vector3 pointXZ = new(point.x, 0, point.y);
						Gizmos.DrawSphere(pointXZ + actualTransformPos - _regionSize/2, gizmosRadius);
						Handles.DrawWireDisc(pointXZ + actualTransformPos - _regionSize/2, Vector3.up, range.x);  
						Handles.DrawWireDisc(pointXZ + actualTransformPos - _regionSize/2, Vector3.up, range.y);
					}
					break;
				case RandomAlgoName.Noise :
					Vector3 chunk = new Vector3(_regionSize.x / xNbRegionSplit, 0f, _regionSize.z / zNbRegionSplit);
					for (int i = 0; i < xNbRegionSplit * zNbRegionSplit; i++) {
						int diff = i / zNbRegionSplit;
						Vector3 nbPos = new Vector3(diff * chunk.x , 0, i % zNbRegionSplit * chunk.z );
						Gizmos.DrawWireCube(actualTransformPos - _regionSize/2 + chunk/2 + nbPos, chunk);
					}

					foreach (Vector3 initPosition in _initPositionIntoPlan) {
						Handles.DrawWireDisc(actualTransformPos + initPosition, Vector3.up, radiusValidZone);
					}

					foreach (Vector2 point in _points) {
						Vector3 pointXZ = new(point.x, 0, point.y);
						Gizmos.DrawSphere(pointXZ + actualTransformPos - _regionSize/2 , gizmosRadius);
					}
					break;
				case RandomAlgoName.FairyRing :
					float radiusZone = 360f / nbMushroomsOnRing / 360f;
					for (int i = 0; i < nbMushroomsOnRing; i++) {
						float angle = radiusZone * i * Mathf.PI * 2;
						Vector3 dir = new Vector3(Mathf.Sin(angle), 0,  Mathf.Cos(angle));

						foreach (Vector3 initPosition in _initPositionIntoPlan) {
							Vector3 radiusRange = actualTransformPos + initPosition + dir * ringRangeRadius.y;
							Gizmos.DrawLine(actualTransformPos + initPosition, radiusRange);
						}
					}
					foreach (Vector2 point in _points) {
						Vector3 pointXZ = new(point.x, 0, point.y);
						Gizmos.DrawSphere(pointXZ + actualTransformPos - _regionSize/2 , gizmosRadius);
					}
					foreach (Vector3 initPosition in _initPositionIntoPlan) {
						Handles.DrawWireDisc(actualTransformPos + initPosition, Vector3.up, ringRangeRadius.x);
						Handles.DrawWireDisc(actualTransformPos + initPosition, Vector3.up, ringRangeRadius.y);
					}
					break;
				}
			}
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(transform.position, _regionSize);
			
			if (initialPointType == InitialPointType.FromTransformReference && transformReference != null) {
				Gizmos.color = Color.black;
				Gizmos.DrawSphere(transformReference.position, gizmosRadius);
			}
		}
	}

}
