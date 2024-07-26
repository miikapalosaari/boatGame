using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject innerCircle, outerCircle;
    private GameObject player;
    public Vector3 Offset;
    public float speed = 4.0f;
    public Rigidbody rb;
    public Vector3 movement;
    public Vector2 direction;
    public float rotationSpeed = 2.0f;

    private Vector2 startingPoint;
    private int leftTouch = 99;

    private void Start()
    {
        innerCircle.SetActive(false);
        outerCircle.SetActive(false);
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);

            Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    Debug.Log("Screen Touch Position is " + t.position);
                    Debug.Log("World Position is " + touchPos);
                    castAFishingRod();
                }
                else
                {
                    
                    Debug.Log("Screen Touch Position is " + t.position);
                    Debug.Log("World Position is " + touchPos);
                    outerCircle.transform.position = touchPos;
                    innerCircle.transform.position = touchPos;
                    outerCircle.SetActive(true);
                    innerCircle.SetActive(true);


                    leftTouch = t.fingerId;
                    startingPoint = touchPos;
                }
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 offset = touchPos - startingPoint;
                direction = Vector2.ClampMagnitude(offset, 1.0f).normalized;
                movement = direction;
                movement.Normalize();

                innerCircle.transform.position = new Vector2(outerCircle.transform.position.x + direction.x, outerCircle.transform.position.y + direction.y);

            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
                innerCircle.transform.position = new Vector2(outerCircle.transform.position.x, outerCircle.transform.position.y);
                outerCircle.SetActive(false);
                innerCircle.SetActive(false);
                movement = Vector3.zero;
            }
            ++i;
        }
        if(direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector2.up);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z * -1));
    }

    void moveCharacter(Vector2 direction)
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private void keepPlayerCenter()
    {
    }
    void castAFishingRod()
    {
        Debug.Log("Casting a Fishing Rod");
    }
}
