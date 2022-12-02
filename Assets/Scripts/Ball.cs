using UnityEngine;

public class Ball : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -20f) 
        {
            if (gameObject.layer != 0) Counter.Overboard = true;
            OnDestroy();
        }
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
