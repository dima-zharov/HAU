using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    void Update()
    {
        transform.position = _ball.position;
        transform.rotation = Quaternion.identity;
        if (gameObject.tag == "part")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 4);
        }
    }
}