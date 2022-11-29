using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
    
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
    [SerializeField] private List<Transform> setList;
    [SerializeField] private Transform levelstart;
    [SerializeField] private GameObject player;
   
    private Vector3 lastEndPosition;
    private void Awake()
    {

        lastEndPosition = levelstart.Find("endposition").position;
        int startingLevelParts = 5;
        for (int i = 0; i <startingLevelParts; i++)
        {
            SpawnLevelPart();
        }
        StartCoroutine(Find());
    }
    IEnumerator Find()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (player!=null)
        {
            if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnLevelPart();
            }
        }
       
    
    }
    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = setList[Random.Range(0,setList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart,lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("endposition").position;
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelparttransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
       
        return levelparttransform;
    }
  
}
