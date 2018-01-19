using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour {

	// Canvas group allows fading of a whole hierarchy of UI elements
	public CanvasGroup m_canvasGroup;

	public void FadeIn (float duration)
	{
		StartCoroutine(Fade(0, 1f, duration));
	}

	public void FadeOut (float duration)
	{
		StartCoroutine(Fade(1f, 0, duration));
	}

	IEnumerator Fade (float from, float to, float duration)
	{
		float initTime = Time.time;

		while(Time.time < initTime + duration)
		{
			m_canvasGroup.alpha = Mathf.Lerp(from, to, (Time.time - initTime) / duration);

			// wait for one frame
			yield return null;
		}

		m_canvasGroup.alpha = to;
	}
}
