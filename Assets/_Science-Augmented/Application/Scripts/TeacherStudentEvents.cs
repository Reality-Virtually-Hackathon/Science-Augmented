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
#if PLATFORM_ANDROID || UNITY_ANDROID || UNITY_IOS
        isTeacher = false;
		isStudent = true;
        OnStudent.Invoke(isStudent);
#else
    isTeacher = true;
    isStudent = false;
   OnTeacher.Invoke (isTeacher);
#endif
        //



    }

    // Update is called once per frame
    void Update () {
		
	}
}
