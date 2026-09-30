using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;

    public Rigidbody projectile;

    public GameObject successPanel;
    public GameObject failPanel;

    public void Success()
    {
        if (gameOver)
            return;

        gameOver = true;

        if (projectile != null)
        {
            projectile.linearVelocity = Vector3.zero;
            projectile.angularVelocity = Vector3.zero;
            projectile.isKinematic = true;
        }

        successPanel.SetActive(true);

        Debug.Log("Level Complete!");
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Fail()
    {
        if (gameOver)
            return;

        gameOver = true;

        if (projectile != null)
        {
            projectile.linearVelocity = Vector3.zero;
            projectile.angularVelocity = Vector3.zero;
            projectile.isKinematic = true;
        }

        failPanel.SetActive(true);

        Debug.Log("Shot Failed!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}