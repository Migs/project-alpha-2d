using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform playerCastleLocation;
    public Transform enemyCastleLocation;
    public float movementSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(playerCastleLocation.position.x, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (transform.position.x <= enemyCastleLocation.position.x) {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(movementSpeed, 0, 0);
            }
        }

        if (transform.position.x >= playerCastleLocation.position.x)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position -= new Vector3(movementSpeed, 0, 0);
            }
        }
    }
}
