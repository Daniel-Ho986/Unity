using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;

    [SerializeField] GameObject platformPrefab;

    void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else if (Instance != this){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate (platformPrefab, new Vector2(-2.81f, 0.4f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2(-1.96f, 0.9499999f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2(-0.98f, 0.63f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2(0.03999996f, 0.9599999f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2(1.16f, 0.49f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2(2.1f, 0.9399999f), platformPrefab.transform.rotation);
    }

    IEnumerator SpawnPlatform(Vector2 spawnPosition){
        yield return new WaitForSeconds(3f);
        Instantiate (platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
