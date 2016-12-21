using UnityEngine;
using System.Collections;

public class MenuAppearScript : MonoBehaviour
{
    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Start()
    {
        isShowing = false;
        menu.SetActive(isShowing);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}