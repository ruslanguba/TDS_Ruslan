using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip playClip;
    [SerializeField] Animator anim;
    [SerializeField] GameObject darkFader;

    public void OnCPlay()
    {
        darkFader.SetActive(true);
        audioSource.PlayOneShot(playClip);
        anim.enabled = true;
        Invoke("LoadGameScene", 4);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
