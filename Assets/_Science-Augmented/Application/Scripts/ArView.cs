using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArView : MonoBehaviour
{
    public CanvasGroup HelpPanelCanvasGroup;
    [SerializeField]
    private Text instructionText;
	// Use this for initialization
	void Start () {
		HelpPanelCanvasGroup.gameObject.SetActive(false);
	    HelpPanelCanvasGroup.alpha = 0;
	}

    public void SetInstructionText(string value)
    {
        instructionText.text = value;
    }

    // Update is called once per frame
	void Update () {
		
	}
}
