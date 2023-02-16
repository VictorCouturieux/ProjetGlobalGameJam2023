using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnVegetable : MonoBehaviour
{
    [SerializeField] private SeedGenerator seedGenerator;
    [SerializeField] private List<GameObject> seedPrefab;
    [SerializeField] private EndLifeVegeEvent endLifeVegetable;
    

    [SerializeField] private float _timerMinSpawnTime = 3;
    [SerializeField] private float _timerMaxSpawnTime = 7;
    
    [SerializeField] private int _maxPlantByGarden = 4;

    private float _timerCountDown;

    private List<Seed> _listSeedPlacement = new List<Seed>();

    struct Seed
    {
        public int indexPosition;
        public GameObject seedRef;
    }

    // Start is called before the first frame update
    void Start() {
        endLifeVegetable.AddCallback(RemoveSeed);
        
        _timerCountDown = Random.Range(_timerMinSpawnTime, _timerMaxSpawnTime);
        int index = Random.Range(0, seedGenerator.GetPoints.Count);
        AddSeed(seedGenerator.GetPoints[index], index);
        if (_maxPlantByGarden > seedGenerator.GetPoints.Count)
            _maxPlantByGarden = seedGenerator.GetPoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
        _timerCountDown -= Time.deltaTime;
        if(_timerCountDown < 0)
        {
            _timerCountDown = Random.Range(_timerMinSpawnTime, _timerMaxSpawnTime);
            if (_listSeedPlacement.Count <= _maxPlantByGarden) {
                int index = Random.Range(0, seedGenerator.GetPoints.Count);
                
                while (!CheckSeedStack(index)) {
                    index = Random.Range(0, seedGenerator.GetPoints.Count);
                }
                AddSeed(seedGenerator.GetPoints[index], index);
            }
        }
    }

    private void AddSeed(Vector3 position, int index) {
        GameObject newSeed = Instantiate(seedPrefab[Random.Range(0, seedPrefab.Count)], transform);
        newSeed.transform.position = transform.position + position;
        Seed seed = new Seed();
        seed.indexPosition = index;
        seed.seedRef = newSeed;
        _listSeedPlacement.Add(seed);
    }

    private void RemoveSeed(GameObject gameObject) {
        Seed selected = new Seed();
        foreach (Seed seed in _listSeedPlacement) {
            if (seed.seedRef == gameObject) {
                selected = seed;
            }
        }
        if (selected.seedRef != null) {
            _listSeedPlacement.Remove(selected);
        }
    }
    
    private bool CheckSeedStack(int index) {
        foreach (Seed seed in _listSeedPlacement) {
            if (index == seed.indexPosition) {
                return false;
            }
        }
        return true;
    }
    
}
