using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoriSwipables : MonoBehaviour
{
    private Vector3 initialPos;
    public float moveSpeed;
    private bool moveLeft = false, moveRight = false, movetoInitial = false;
    private Vector2 touchStartPos;
    private float swipeThreshold = 5f;
    private Rigidbody rb;
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
    }

    private void Start()
    {
        rb.isKinematic = true;
    }
    


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchEndPos = Input.mousePosition;
            float swipeDistance = touchEndPos.x - touchStartPos.x;
            rb.isKinematic = false; 

            if (Mathf.Abs(swipeDistance) > swipeThreshold)
            {
                Ray ray = Camera.main.ScreenPointToRay(touchStartPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        if (swipeDistance > 0)
                        {
                            // Right swipe
                            moveRight = true;
                            moveLeft = false;
                           
                        }
                        else
                        {
                            // Left swipe
                            moveLeft = true;
                            moveRight = false;
                           
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile Red") || collision.gameObject.CompareTag("Tile Blue") || collision.gameObject.CompareTag("Tile Green") || collision.gameObject.CompareTag("Tile Yellow") || collision.gameObject.CompareTag("Swipable"))
        {
            movetoInitial = true;
        }
    }

    private void Move(Vector3 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            Move(Vector3.right);
        }

        if (moveLeft)
        {
            Move(Vector3.left);
        }

        if (movetoInitial)
        {
            Vector3 moveDirection = (initialPos - transform.position).normalized;
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            moveRight = false;
            moveLeft = false;
            if (Vector3.Distance(transform.position, initialPos) <= 0.05f)
            {
                movetoInitial = false;
                transform.position = initialPos;
            }

            rb.isKinematic = true;
        }
    }
   






}
