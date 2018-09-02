using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;

    int maxHealth;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        maxHealth = health;
    }

    private void Update()
    {
        if (health <= 0)
        {
            health = maxHealth;

            transform.position = new Vector2(0, 0);

            SceneManager.LoadScene(1);
        }
    }
}
