using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TeacherActivity : MonoBehaviour {

	public GameObject IntruductionPanel;
	public GameObject GamePanel;
	public GameObject ScorePanel;

	public virtual void StartInstruction(){
		IntruductionPanel.SetActive (true);
		GamePanel.SetActive (false);
		ScorePanel.SetActive (false);
	}

	public virtual void StartGame(){
		IntruductionPanel.SetActive (false);
		GamePanel.SetActive (true);
		ScorePanel.SetActive (false);
	}

	public virtual void EndGame(){
		IntruductionPanel.SetActive (false);
		GamePanel.SetActive (false);
		ScorePanel.SetActive (true);
	}
}
