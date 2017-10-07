using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TeacherStudentEvent : UnityEvent<bool>{}
public class TeacherStudentEvents : MonoBehaviour {


	public TeacherStudentEvent OnStudent;
	public TeacherStudentEvent OnTeacher;

	private bool isTeacher;
	private bool isStudent;

	// Use this for initialization
	void Start () {
		//Adrian
		isTeacher = false;
		isStudent = false;
		//

		OnStudent.Invoke (isStudent);
		OnTeacher.Invoke (isTeacher);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
