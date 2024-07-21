using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public Animator fade;
    public GameObject PauseMenu;
    public GameObject CreditsPanel;
    public GameObject[] CreditSlides;
    public int CurrentInt = 0;
    public PlayerCam playercam;
    public PlayerMovement playermovement;

    private AudioSource[] allAudioSources;

    public bool isPaused;

    private void Awake()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }
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
            Pausing();         
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadScene());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pausing()
    {
        if (!isPaused) //pausing
        {
            PauseAllAudioSource();
            Time.timeScale = 0f;
            isPaused = true;
            playercam.isPaused = isPaused;
            playermovement.isPaused = isPaused;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseMenu.SetActive(true);
        }

        else if (isPaused)
        {
            UnpauseAllAudioSource();
            Time.timeScale = 1f;
            isPaused = false;
            playercam.isPaused = isPaused;
            playermovement.isPaused = isPaused;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PauseMenu.SetActive(false);
            CreditsPanel.SetActive(false);
        }

        
    }

    private IEnumerator ReloadScene()
    {
        fade.SetTrigger("Restart");
        yield return new WaitForSeconds(1);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }

    public void OpenCredits()
    {
        CurrentInt = 0;
        SetPage();
        CreditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        CurrentInt = 0;
        SetPage();
        CreditsPanel.SetActive(false);  
    }

    public void NextCredits()
    {
        if(CurrentInt == CreditSlides.Length - 1)
        {
            CurrentInt = 0;
        }
        else
        {
            CurrentInt++;   
        }

        SetPage();
    }

    private void SetPage()
    {
        for (int i = 0; i < CreditSlides.Length; i++)
        {
            if (i == CurrentInt)
            {
                CreditSlides[i].SetActive(true);
            }
            else
            {
                CreditSlides[i].SetActive(false);
            }
        }
    }

    private void PauseAllAudioSource()
    {
        foreach (AudioSource aus in allAudioSources) 
        {
            aus.Pause();
        }
    }

    private void UnpauseAllAudioSource()
    {
        foreach (AudioSource aus in allAudioSources)
        {
            aus.UnPause();
        }
    }
}
