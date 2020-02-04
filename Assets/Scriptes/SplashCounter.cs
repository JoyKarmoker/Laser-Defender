using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SplashCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeStart = 11;
    public TextMeshProUGUI timerText;
    public LevelLoader levelLoder;
    public float playerSpeed = 10f;
    public GameObject player;
    [SerializeField] AudioClip launchSound;
    [SerializeField] AudioClip Counter;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(Counter, Camera.main.transform.position);
        levelLoder = FindObjectOfType<LevelLoader>();
        timerText.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        timeStart = timeStart - Time.deltaTime;
        timerText.text = Mathf.Round(timeStart).ToString();
        if (timeStart <= 0)
        {
            timerText.text = "0";
            StartCoroutine(Wait());
            StartCoroutine(Launch());
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        
    }

    IEnumerator Launch()
    {
        audioSource.PlayOneShot(launchSound);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerSpeed);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
