using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Projectile")
        {
            gameManager.Success();
        }
    }
}