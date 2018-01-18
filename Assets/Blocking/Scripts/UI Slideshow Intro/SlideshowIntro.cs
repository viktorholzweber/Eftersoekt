using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SlideshowIntro : MonoBehaviour
{
	public FadeAnimation m_background;
	public FadeAnimation[] m_slides;
	public CanvasGroup m_canvasGroup;

	int m_currentSlide = -1;

	public void StartIntro ()
	{
		m_canvasGroup.interactable = true;

		m_currentSlide = -1;

		m_background.FadeIn(0);

		NextSlide();
	}

	public void NextSlide()
	{
		if (m_currentSlide >= 0)
			m_slides[m_currentSlide].FadeOut(0.5f);

		m_currentSlide++;

		if(m_currentSlide < m_slides.Length)
		{ 
			m_slides[m_currentSlide].FadeIn(0.5f);
		}
		else
		{
			m_background.FadeOut(0.5f);
			m_canvasGroup.interactable = false;
			Debug.Log("Intro finished");

			// implement what happens, when the intro is finished
			//GameManager.instance.IntroFinished();
		}
	}

	#region Accessing the object

	public static SlideshowIntro instance;

	void Awake()
	{
		SlideshowIntro.instance = this;
	}

	#endregion
}
