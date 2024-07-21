using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Animator fade;
    public GameObject pause;

    public bool isPaused;
    public PlayerMovement playermove;
    public PlayerCam playercam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) //not playing
            {
                Gamepad.current.SetMotorSpeeds(0f, 0f);
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isPaused = true;
                playermove.isPaused = isPaused;
                playercam.isPaused = isPaused;
                pause.SetActive(true);
            }

            else if(isPaused) //play game
            {
                Time.timeScale = 1f;
                isPaused = false;
                playermove.isPaused = isPaused;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playermove.isPaused = isPaused;
                playercam.isPaused = isPaused;
                pause.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadScene());
        }
    }

    private IEnumerator ReloadScene()
    {
        fade.SetTrigger("Restart");
        yield return new WaitForSeconds(1);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
}
