using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowShreader : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool rot = false;

    private void Start()
    {
        if (transform.position.z > 1f || transform.position.z < -1f)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
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
        if (collision.gameObject.CompareTag("Tile Yellow"))
        {
            Destroy(collision.gameObject);
            gameManager.Points++;
        }
        if (collision.gameObject.CompareTag("Tile Blue") && gameManager.Points >= 3)
        {
            StartCoroutine(ResetRotAfterDelay());
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
        Quaternion targetRotation = Quaternion.Euler(180, 0, 0);
        parentTransform.rotation = Quaternion.Lerp(parentTransform.rotation, targetRotation, Time.deltaTime * 5);
        parentTransform.position = new Vector3(parentTransform.position.x, 0.75f, parentTransform.position.z);
    }
}
