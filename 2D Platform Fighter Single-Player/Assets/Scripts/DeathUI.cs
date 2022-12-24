using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    [SerializeField] Button quitButton;
    [SerializeField] Button respawnButton;

    [SerializeField] GameObject GM;

    private void Awake() {
        respawnButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
            // Respawn player
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Respawn();
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
