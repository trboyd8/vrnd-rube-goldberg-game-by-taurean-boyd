using UnityEngine;

public class TeleportObject : MonoBehaviour {

    public GameObject otherTeleporter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            other.transform.position = otherTeleporter.transform.position;
        }
    }
}
