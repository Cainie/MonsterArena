using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabSpawnOnArea : MonoBehaviour{

    private enum AreaType{
        CIRCLE = 1,
        RECTANGLE = 2
        
    }

    [SerializeField] private bool spawnInsideArea;
    [SerializeField] private AreaType areaType;
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private int numberOfSpawnsForEachPrefab;
    [SerializeField] private Transform center;
    [SerializeField] private float areaRadius;
    [SerializeField] private Transform spawnedObjectsParent;

    private Vector3 centerPosition;

    private void Awake(){
        centerPosition = center.position;
        
        switch (areaType){
            case AreaType.CIRCLE:
                if (spawnInsideArea){
                    SpawnInsideCircle(prefabsToSpawn);
                } else{
                    SpawnOnCircle(prefabsToSpawn);
                }
                break;
            
            case AreaType.RECTANGLE:
                if (spawnInsideArea){
                    SpawnInsideRectangle(prefabsToSpawn);
                } else{
                    SpawnOnRectangle(prefabsToSpawn);
                }
                
                break;
            
            default:
                Debug.LogError("There is no corresponding function for areaType " + areaType);
                break;
        }
    }

    private void SpawnInsideRectangle(List<GameObject> objectsToSpawn){
        foreach (var objectToSpawn in objectsToSpawn){
            for (var i = 0; i < numberOfSpawnsForEachPrefab; i++){
                SpawnInsideRectangle(objectToSpawn);
            }
        }
    }
    
    private void SpawnInsideRectangle(GameObject objectToSpawn){
        var xAxisDisplacement = Random.Range(-areaRadius, areaRadius);
        var spawnPositionXAxis = centerPosition.x + xAxisDisplacement;
        var yAxisDisplacement = Random.Range(-areaRadius, areaRadius);
        var spawnPositionYAxis = centerPosition.y + yAxisDisplacement;
        
        var spawnPosition = new Vector2(spawnPositionXAxis,spawnPositionYAxis);
        Instantiate(objectToSpawn, spawnPosition,Quaternion.identity,spawnedObjectsParent);
    }

    private void SpawnInsideCircle(List<GameObject> objectsToSpawn){
        foreach (var objectToSpawn in objectsToSpawn){
            for (var i = 0; i < numberOfSpawnsForEachPrefab; i++){
                SpawnInsideCircle(objectToSpawn);
            }
        }
    }

    private void SpawnInsideCircle(GameObject objectToSpawn){
        var spawnPosition = (Vector2)centerPosition + Random.insideUnitCircle * areaRadius;
        Instantiate(objectToSpawn, spawnPosition,Quaternion.identity,spawnedObjectsParent);
    }

    private void SpawnOnRectangle(List<GameObject> objectsToSpawn){
        foreach (var objectToSpawn in objectsToSpawn){
            for (var i = 0; i < numberOfSpawnsForEachPrefab; i++){
                SpawnOnRectangle(objectToSpawn);
            }
        }
    }
    
    private void SpawnOnRectangle(GameObject objectToSpawn){
        //todo: needs fixing
        var xAxisDisplacement = Random.Range(0, 2) == 0 ? -areaRadius : areaRadius;
        var spawnPositionXAxis = centerPosition.x + xAxisDisplacement;
        var yAxisDisplacement = Random.Range(0, 2) == 0 ? -areaRadius : areaRadius;
        var spawnPositionYAxis = centerPosition.y + yAxisDisplacement;
        
        var spawnPosition = new Vector2(spawnPositionXAxis,spawnPositionYAxis);
        Instantiate(objectToSpawn, spawnPosition,Quaternion.identity,spawnedObjectsParent);
    }

    private void SpawnOnCircle(List<GameObject> objectsToSpawn){
        foreach (var objectToSpawn in objectsToSpawn){
            for (var i = 0; i < numberOfSpawnsForEachPrefab; i++){
                SpawnOnCircle(objectToSpawn);
            }
        }
    }
    
    private void SpawnOnCircle(GameObject objectToSpawn){
        var spawnPosition = (Vector2)centerPosition + Random.insideUnitCircle.normalized * areaRadius;
        Instantiate(objectToSpawn, spawnPosition,Quaternion.identity,spawnedObjectsParent);
    }

    private void OnDrawGizmos(){
        centerPosition = center.position;
        Handles.color = Color.green;
        Gizmos.color = Color.green;
        switch (areaType){
            case AreaType.CIRCLE:
                Handles.DrawWireDisc(centerPosition, Vector3.forward, areaRadius);
                break;
            
            case AreaType.RECTANGLE:
                Gizmos.DrawWireCube(centerPosition, new Vector3(areaRadius * 2, areaRadius * 2, 1));
                break;
            
            default:
                Debug.LogError("There is no corresponding function for areaType " + areaType);
                break;
        }
        Handles.DrawWireDisc(centerPosition, Vector3.forward, areaRadius);
    }
}
