using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SplashCounter : MonoBehaviour
{
    public int timeStart = 10;
    [SerializeField] Animator animator;
    public TextMeshProUGUI timerText;
    [SerializeField] float playerSpeed = 10f;
    public GameObject player;
    [SerializeField] AudioClip launchSound;
    [SerializeField] AudioClip Counter;
    AudioSource audioSource;
    bool isLaunched = false;


   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        timerText.text = timeStart.ToString();
        StartCoroutine(count());
    }
    private void Update()
    {
        if (isLaunched)
            StartCoroutine(Launch());
    }
    IEnumerator count()
    {

        yield return new WaitForSeconds(1);
        timeStart = timeStart - 1;
        timerText.text = Mathf.Round(timeStart).ToString();
        audioSource.PlayOneShot(Counter, 1F);

        if (timeStart <= 0)
        {
            timerText.text = "0";

            isLaunched = true;
            animator.SetTrigger("Blast");

        }
        else
            StartCoroutine(count());
     
    }


    IEnumerator Launch()
    {
        isLaunched = false;
        audioSource.PlayOneShot(launchSound, 0.2f);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerSpeed);
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(1);
    }

}
