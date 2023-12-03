using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Points = 0;
    [SerializeField] private TextMeshProUGUI points,swapAllow;
    [SerializeField] private GameObject Win; 
    private void Update()
    {
        points.text = "Point: " + Points;

        if(Points >= 3)
        {
            swapAllow.gameObject.SetActive(true);
        }
        if(Points == 8)
        {
            Win.SetActive(true);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
