using UnityEngine;

public class InstanciateNature : MonoBehaviour
{
    [SerializeField] private SeedGenerator seedGenerator;
    [SerializeField] private SeedGenerator seedGenerator2;
    [SerializeField] private GameObject naturePrefab;
    
    [SerializeField] private bool _isRotated = false;

    [SerializeField] private float _minsize = 1.0f;
    [SerializeField] private float _maxsize = 1.0f;
    
    // Start is called before the first frame update
    void Start() {
        if (seedGenerator2 != null) {
            seedGenerator2.GenerateSeed();
        }
        seedGenerator.GenerateSeed();
        foreach (Vector3 natureSeed in seedGenerator.GetPoints) {
            AddNature(natureSeed);
        }
    }
    
    private void AddNature(Vector3 position) {
        GameObject newSeed = Instantiate(naturePrefab, transform);
        newSeed.transform.position = transform.position + position;
        if (_isRotated) {
            var rotation = newSeed.transform.rotation;
            rotation = Quaternion.Euler(-90, rotation.y, Random.Range(0, 359));
            newSeed.transform.rotation = rotation;
        }
        newSeed.transform.localScale = Vector3.one * Random.Range(_minsize, _maxsize);
    }
}
