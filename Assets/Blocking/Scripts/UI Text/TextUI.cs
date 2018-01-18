using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// all UI classes are part of UnityEngine.UI namespace:
using UnityEngine.UI;

/// <summary>
/// Display some text, optionally together with sound.
/// Use TextUI.instance.ShowText(...) whenever a text-display is needed.
/// </summary>
public class TextUI : MonoBehaviour
{
	public Text m_text;
	public FadeAnimation m_fade;
	public AudioSource m_audioSource;

	public void ShowText (string text, float duration)
	{
		m_text.text = text;
		m_fade.FadeIn(0.25f);

		StartCoroutine(Close(duration));
	}

	public void ShowText(string text, AudioClip clip, float duration)
	{
		m_audioSource.clip = clip;
		m_audioSource.PlayDelayed(0.25f);

		ShowText(text, duration);
	}

	private IEnumerator Close (float closeAfter)
	{
		yield return new WaitForSeconds(closeAfter);

		m_fade.FadeOut(0.25f);
	}

	#region Accessing the object

	// Kind of like a Singleton. Easy way to access MonoBehaviors that only exist once in a game. 
	public static TextUI instance;

	// Awake() is like Start() but will be called earlier. See https://docs.unity3d.com/Manual/ExecutionOrder.html
	void Awake()
	{
		TextUI.instance = this;
	}

	#endregion
}
