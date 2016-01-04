using UnityEngine;

public class PickUpController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 40 * Time.deltaTime);
    }
}
