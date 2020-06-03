using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour
{
	[System.Serializable]
	public class Pool{
		public string tag;
		public GameObject prefab;
		public int size;
	}
	public static BloodPool Instance;
	private void Awake(){
		Instance = this;
	}

	public List<Pool> pools;
	public Dictionary<string,Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string,Queue<GameObject>>();

        // Loop through all pools, where each item is pool
        foreach (Pool pool in pools){
        	// Create a queue full of objects
        	Queue<GameObject> objectPool = new Queue<GameObject>();
        	// Fill up the queue
        	for (int i = 0; i < pool.size; i++){
        		GameObject obj = Instantiate(pool.prefab);
        		obj.SetActive(false);
        		objectPool.Enqueue(obj);
        	}
        	poolDictionary.Add(pool.tag, objectPool);
        }

    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation){

    	if (!poolDictionary.ContainsKey(tag)){
    		Debug.LogWarning(tag + "does not exist");
    		return null;
    	}

    	GameObject objectToSpawn = poolDictionary[tag].Dequeue();

    	objectToSpawn.SetActive(true);
    	objectToSpawn.transform.position = position;
    	objectToSpawn.transform.rotation = rotation;
    	poolDictionary[tag].Enqueue(objectToSpawn);

    	return objectToSpawn;
    }
}
