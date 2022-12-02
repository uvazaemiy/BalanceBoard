using UnityEngine;

public class Board : MonoBehaviour
{
    float tiltAngle = 20f;
    float speed = 100f;

    Rigidbody rb;
    Vector3 acceleration;
    Vector3 move;
    private Camera _camera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }
 
    private void FixedUpdate()
    {
        Control();
    }

    void Control()
    {
#if UNITY_ANDROID || UNITY_IPHONE

        ControlBoard(Input.acceleration, -Input.acceleration.x, Input.acceleration.y);

#elif UNITY_STANDALONE_WIN

        ControlBoard(_camera.WorldToScreenPoint(Input.mousePosition), Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

#endif
    }

    void ControlBoard(Vector3 controlPlayer, float x, float y)
    {
        //if (controlPlayer != move)
       // {
            acceleration = new Vector3(y, 0f, x) * speed * Time.deltaTime;

            acceleration.x = BorderAngle(rb.rotation.x, acceleration.x);
            acceleration.z = BorderAngle(rb.rotation.z, acceleration.z);

            rb.rotation *= Quaternion.Euler(acceleration);
      //  }
      //  else rb.velocity = Vector3.zero;

      //  move = controlPlayer;
    }

    float BorderAngle(float rot, float pos)
    {
        if (((rot * 180 <= -tiltAngle && pos < 0) || (rot * 180 >= tiltAngle && pos > 0)))
        {
            return 0;
        }
        return pos;
    }


}
