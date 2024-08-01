using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] GameObject playerMenu;
    // Start is called before the first frame update

    void Start() {
        gameObject.SetActive(false);
    }
    public void Activate() {
        if (playerMenu != null) {
            Debug.Log("ButtonClicked - Activate");
            playerMenu.SetActive(true);

        }
    }

    public void Deactivate() {
        if (playerMenu != null) {
            Debug.Log("ButtonClicked - Deactivate");
            playerMenu.SetActive(false);
        }
    }
}
