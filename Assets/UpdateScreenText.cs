using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateScreenText : MonoBehaviour {

	public TextMesh textMesh;
	public float updateSpeed = 0.1f;

	void Start () {

		AddDisplayText("I'm sorry Dave, I can't let you do that. This is a really long piece of text that may take forever to display on the screen !!!!!!!!");

		StartCoroutine(UpdateComputerScreen());
	}
	string line = string.Empty;

	void AddDisplayText(string message)
	{
		textMesh.text = string.Empty;
		line = message;
	}

	IEnumerator UpdateComputerScreen()
	{
		if(textMesh.text.EndsWith("|"))
			textMesh.text = textMesh.text.TrimEnd('|');
		else
			textMesh.text += "|";

		if(line == null)
			line = string.Empty;

		if(line.Length > 0)
		{
			var letter = line[0].ToString();


			if(line.Length > 1)
				line = line.Substring(1);
			else
				line = string.Empty;
	
			bool hasCursor = textMesh.text.EndsWith("|");

			textMesh.text = textMesh.text.TrimEnd('|') + letter;

			if(textMesh.text.Length > 100)
				textMesh.text = textMesh.text.Substring(25).Trim();

			if( (textMesh.text.Length % 25) == 0){
				textMesh.text += Environment.NewLine;
				line = line.Trim();
			}

			if(hasCursor)
				textMesh.text += "|";
		}

		yield return new WaitForSeconds(updateSpeed);


		StartCoroutine(UpdateComputerScreen());
	}

}
