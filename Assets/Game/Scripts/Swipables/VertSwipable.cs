using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertSwipable : MonoBehaviour
{
    private Vector3 initialPos;
    public float moveSpeed;
    private bool moveBack = false, moveFwd = false, movetoInitial = false;
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
            float swipeDistance = touchEndPos.y - touchStartPos.y;
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
                            // Up swipe
                            moveFwd = true;
                            moveBack = false;

                        }
                        else
                        {
                            // Down swipe
                            moveBack = true;
                            moveFwd = false;

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
        if (moveFwd)
        {
            Move(Vector3.forward);
        }

        if (moveBack)
        {
            Move(Vector3.back);
        }

        if (movetoInitial)
        {
            Vector3 moveDirection = (initialPos - transform.position).normalized;
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            moveFwd = false;
            moveBack = false;
            if (Vector3.Distance(transform.position, initialPos) <= 0.05f)
            {
                movetoInitial = false;
                transform.position = initialPos;
            }

            rb.isKinematic = true;
        }
    }
}
