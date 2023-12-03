using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Points = 0,SwapLeft =2;
    [SerializeField] private TextMeshProUGUI points,swapAllow,Allow;
    [SerializeField] private GameObject Win;
    public bool SwapAlloed = false;
    private void Update()
    {
        points.text = "Point: " + Points;
        swapAllow.text = "Swap Left:" + SwapLeft;
        if (Points > 3 && SwapLeft >= 0)
        {
            SwapAlloed = true;
            
        }
        if(Points >= 3)
        {
            SwapAlloed = true;
            swapAllow.gameObject.SetActive(true);
            Allow.gameObject.SetActive(true);
        }
        if(Points == 8)
        {
            Win.SetActive(true);
        }
        if(SwapLeft <= 0)
        {
            SwapAlloed = false;
            SwapLeft = 0;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
