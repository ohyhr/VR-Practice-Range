using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour {
    public GameObject targetPrefab;
    public Transform spawnPoint;

    public GameObject targetPrefab2;
    public Transform spawnPoint2;

    public void Target()
    {
        Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    public void Target2()
    {
        Instantiate(targetPrefab2, spawnPoint2.position, spawnPoint2.rotation);
    }
}
