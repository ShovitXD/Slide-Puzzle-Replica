using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementHandler : MonoBehaviour
{
    [Header("Tile Positions")]
    public Transform[] TilePos;
    [Header("Shreaders Locations")]
    public Transform[] ShreaderPos;
    [Header("Swipable Prefabs")]
    public GameObject[] SwipablesPrefab;
    [Header("Shreader Prefabs")]
    public GameObject[] ShreaderPrefab;
    [Header("Parent Objects")]
    public GameObject Tiles;
    public GameObject Shreaders;
    private void Awake()
    {
      
    }
    private void Start()
    {
        //Set Parent position (For Insurance)
        Tiles.transform.localPosition = Vector3.zero;
        Shreaders.transform.localPosition = Vector3.zero;

        // Shuffle TilePos array
        ShuffleArray(TilePos);

        // Set Swipable initial positions
        for (int i = 0; i < SwipablesPrefab.Length; i++)
        {
            // Randomize initial position
            Vector3 swipablePosition = new Vector3(TilePos[i].position.x, TilePos[i].position.y + 0.5f, TilePos[i].position.z);

            // Instantiate Swipables at randomized position
            GameObject swipableObject = Instantiate(SwipablesPrefab[i], swipablePosition, Quaternion.identity);
            swipableObject.transform.parent = Tiles.transform;
        }
    }

    // Fisher-Yates shuffle algorithm
    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randIndex];
            array[randIndex] = temp;
        }
    }
}
