using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadingBar : MonoBehaviour
{
	[SerializeField] Animator animator;
	public GameObject loadingSceneCanvas;
	public TextMeshProUGUI loadingText;
	[SerializeField] string[] sentences;
	float typingSpeed;
	int index;

	/* Load Asyschronously. 
	 * works well for heavy scene..
	 * 
	 void Start()
	{
		loadLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void loadLevel(int sceneIndex)
	{
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}
	IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		loadingSceneCanvas.SetActive(true);
		while(!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);

			slider.value = progress;
			yield return null;
		}
		
	}*/

	/* Load fake. 
	 * works well for light scene..
	 */
	void Start()
	{
		loadLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void loadLevel(int sceneIndex)
	{
		StartCoroutine(LoadFake(sceneIndex));
	}
	IEnumerator LoadFake(int sceneIndex)
	{
		foreach (char letter in sentences[index].ToCharArray())
		{
			typingSpeed = Random.Range(0.15f,0.4f);
			loadingText.text += letter;
			yield return new WaitForSecondsRealtime(typingSpeed);
		}

		animator.SetTrigger("Fade");

		StartCoroutine(WaitAndLoadStartMenu(sceneIndex));
		AudioManager.instance.play(AllStringConstants.BG_SOUND, true, false);


	}
	IEnumerator WaitAndLoadStartMenu(int sceneIndex)
	{
		yield return new WaitForSeconds(4f);
		LevelLoader.instance.loadSelectedLevel(SceneManager.GetActiveScene().buildIndex + 1, false);

	}

}

