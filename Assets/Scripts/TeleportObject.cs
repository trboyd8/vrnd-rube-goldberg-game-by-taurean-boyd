using UnityEngine;

public class TeleportObject : MonoBehaviour {

    public GameObject otherTeleporter;
    public bool canTeleport = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable")  && this.canTeleport)
        {
            otherTeleporter.GetComponent<TeleportObject>().canTeleport = false;
            other.transform.position = otherTeleporter.transform.position;
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            rigidbody.velocity = otherTeleporter.transform.forward * rigidbody.velocity.magnitude;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!this.canTeleport)
        {
            this.canTeleport = true;
        }
    }
}
