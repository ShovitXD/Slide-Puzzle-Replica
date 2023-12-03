using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedShreader : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool rot = false;
    
    private void Start()
    {
        if (transform.position.z > 1f || transform.position.z < -1f)
        {
            transform.rotation = Quaternion.Euler(0f,90f,0f);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
    private void Update()
    {
        if (rot)
        {
            Rotate();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tile Red"))
        {
            Destroy(collision.gameObject);
            gameManager.Points++;
        }
        if (collision.gameObject.CompareTag("Tile Green") && gameManager.SwapAlloed == true)
        {
            StartCoroutine(ResetRotAfterDelay());
            gameManager.SwapLeft--;
        }
    }
    IEnumerator ResetRotAfterDelay()
    {
        rot = true;
        yield return new WaitForSeconds(2.0f);
        rot = false;
    }
    private void Rotate()
    {
        Transform parentTransform = transform.parent;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 180);
        parentTransform.rotation = Quaternion.Lerp(parentTransform.rotation, targetRotation, Time.deltaTime * 5);
        parentTransform.position = new Vector3(parentTransform.position.x, 0.75f, parentTransform.position.z);
    }
}
