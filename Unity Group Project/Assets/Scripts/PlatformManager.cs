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
        Instantiate (platformPrefab, new Vector2(-2.0f, 1.5f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2(0f, 0f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2(3.5f, 1.5f), platformPrefab.transform.rotation);
    }

    IEnumerator SpawnPlatform(Vector2 spawnPosition){
        yield return new WaitForSeconds(2f);
        Instantiate (platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
