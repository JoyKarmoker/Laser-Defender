using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class DialougeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDisplay;
    [Header("@ is equal to line break")]
    [SerializeField] string[] sentences;
    [SerializeField] float typingSpeed;
    [SerializeField] GameObject continueText;
    int index;
    bool isTapEnabled = false;

    int TapCount;
    public float MaxDubbleTapTime;
    float NewTime;
    private void Start()
    {
        continueText.SetActive(false);

        TapCount = 0;        
    }

    private void Update()
    {
        if(isTapEnabled)
            CountAction();
    }
    public IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            if (letter == '@')
                textDisplay.text += "\n";
            else
                textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(1);
        continueText.SetActive(true);
        isTapEnabled = true;
    }
    private void CountAction()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {

                NewTime = Time.time + MaxDubbleTapTime;
            }
            else if (TapCount == 2 && Time.time <= NewTime)
            {

                //Whatever you want after a dubble tap    
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


                TapCount = 0;
            }

        }
        if (Time.time > NewTime)
        {
            TapCount = 0;
        }
    }

}
