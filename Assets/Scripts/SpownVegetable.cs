using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownVegetable : MonoBehaviour
{
    [SerializeField] private SeedGenerator seedGenerator;
    [SerializeField] private GameObject seedPrefab;

    [SerializeField] private float _timerMinSpownTime = 3;
    [SerializeField] private float _timerMaxSpownTime = 7;
    
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
        _timerCountDown = Random.Range(_timerMinSpownTime, _timerMaxSpownTime);
        Debug.Log("_timerCountDown : " +  _timerCountDown);
    }

    // Update is called once per frame
    void Update()
    {
        _timerCountDown -= Time.deltaTime;
        if(_timerCountDown < 0)
        {
            _timerCountDown = Random.Range(_timerMinSpownTime, _timerMaxSpownTime);
            Debug.Log("_timerCountDown : " +  _timerCountDown);
            if (_listSeedPlacement.Count <= _maxPlantByGarden) {
                int index = Random.Range(0, seedGenerator.GetPoints.Count);
                while (!CheckSeedStack(index)) {
                    index = Random.Range(0, seedGenerator.GetPoints.Count);
                }
                Debug.Log("spown at : x" + seedGenerator.GetPoints[index].x + " y" + seedGenerator.GetPoints[index].y + " z" + seedGenerator.GetPoints[index].z );
                GameObject gO = Instantiate(seedPrefab, transform.position + seedGenerator.GetPoints[index], Quaternion.identity, transform);
                Seed seed = new Seed();
                seed.indexPosition = index;
                seed.seedRef = gO;
                _listSeedPlacement.Add(seed);
            }
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
