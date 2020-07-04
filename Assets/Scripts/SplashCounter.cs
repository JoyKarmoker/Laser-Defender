using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SplashCounter : MonoBehaviour
{
    public float timeStart = 11;
    public TextMeshProUGUI timerText;
    [SerializeField] float playerSpeed = 10f;
    public GameObject player;
    [SerializeField] AudioClip launchSound;
    [SerializeField] AudioClip Counter;
    AudioSource audioSource;
    bool isLaunched = false;


    // Start is called before the first frame update

    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Counter;
        audioSource.Play();
        timerText.text = timeStart.ToString();

        yield return new WaitForSeconds(audioSource.clip.length - 1);
        //audioSource.clip = launchSound;
        audioSource.PlayOneShot(launchSound);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerSpeed);
        StartCoroutine(Launch());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaunched){
            timeStart = timeStart - Time.deltaTime;
            timerText.text = Mathf.Round(timeStart).ToString();
            if (timeStart <= 0)
            {
                timerText.text = "0";
                
                isLaunched = true;

            }
        }

    }

    IEnumerator Launch()
    {        
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(1);
    }
}
