using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] PathsPrototypes;

    protected Dictionary<GameObject, GameObject[]> _paths = new Dictionary<GameObject, GameObject[]>();

    public bool Active { get; protected set; }

    private void Awake()
    {
        PathsPrototypes.ToList().ForEach(path =>
        {
            var newPath = new List<GameObject>();
            foreach(Transform child in path.transform)
            {
                newPath.Add(child.gameObject);
            }
            _paths.Add(path, newPath.ToArray());
        });
    }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        Active = true;
        StartCoroutine(Run());
    }

    public void DeActivate()
    {
        Active = false;
    }

    protected IEnumerator Run()
    {
        while(true)
        {
            var spawnDelay = UnityEngine.Random.Range(3.0f, 5.0f);
            yield return new WaitForSeconds(spawnDelay);
            Spawn();
        }
    }

    protected void Spawn()
    {
        var pathIndex = UnityEngine.Random.Range(0, PathsPrototypes.Length);
        var prototype = PathsPrototypes[pathIndex];

        var enemyObject = GameObject.Instantiate(EnemyPrefab, prototype.transform.position, Quaternion.identity);
        var enemy = enemyObject.GetComponent<AIController>();
        enemy.Path = _paths[prototype];

        enemy.Run();
    }

}
