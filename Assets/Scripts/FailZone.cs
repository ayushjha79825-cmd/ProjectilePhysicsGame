using UnityEngine;

public class FailZone : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Projectile")
        {
            gameManager.Fail();
        }
    }

    void Update()
    {
        if (GameObject.Find("Projectile") == null)
            return;

        GameObject projectile = GameObject.Find("Projectile");

        if (projectile.transform.position.y < -3f ||
            projectile.transform.position.x > 20f ||
            projectile.transform.position.x < -12f)
        {
            gameManager.Fail();
        }
    }
}
