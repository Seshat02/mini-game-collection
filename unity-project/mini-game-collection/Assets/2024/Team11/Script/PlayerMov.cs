using MiniGameCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float speed;
    Vector3 moveDir;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = ArcadeInput.Player1.AxisX;
        float verticalAxis = ArcadeInput.Player1.AxisY;

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (direction.magnitude != 0)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
