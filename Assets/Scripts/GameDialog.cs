using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDialog : MonoBehaviour
{
	public Text titleText;
	public Text contentText;
	public void Show (bool isShow)
	{
		gameObject.SetActive(isShow);
	}
	public void UpdateD(string title, string content)
	{
		if (titleText)
		{
			titleText.text = title;
		}
		if (contentText)
		{
			contentText.text = content;
		}
	}

}
