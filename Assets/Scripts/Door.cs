
    using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                camMoveToNewRoom(nextRoom);
            else
                camMoveToNewRoom(previousRoom);
        }
    }

    private void camMoveToNewRoom(Transform newRoom) // Added parameter for the room to move to
    {
        if (cam != null)
        {
            cam.MoveToNewRoom(newRoom); // Call the MoveToNewRoom method on the CameraController
        }
         else
            camMoveToNewRoom(previousRoom);
       
    }
}