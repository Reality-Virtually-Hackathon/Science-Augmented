using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentArController : MonoBehaviour
{
    [SerializeField]
    private List<Vuforia.ImageTargetBehaviour> imageTargetPrefabs = new List<Vuforia.ImageTargetBehaviour>();
   [SerializeField]
   private ArModels Models = new ArModels();
   [SerializeField]
    ArView arView;

   
    private List<GameObject> objectAnchors = new List<GameObject>();
   


    // Use this for initialization
    void Start () {
        CreateModels();
        if (arView == null)
        {
            arView = FindObjectOfType<ArView>();
        }
    }

    public void SetInstructionText(string value)
    {
        arView.SetInstructionText(value);
    }
    



    public void CreateModels()
    {
        int i = 0;
        foreach (KeyValuePair<string, EducationModel> keyValuePair in Models.arModelDictionary)
        {
            int imageTargetNumber = i % ( imageTargetPrefabs.Count - 1 );
            Vuforia.ImageTargetBehaviour imageTarget = Instantiate(imageTargetPrefabs[imageTargetNumber]);
            
            OnCollisionEvents collisionEventTrigger = imageTarget.gameObject.GetComponent<OnCollisionEvents>();
            if (collisionEventTrigger != null)
            {
                collisionEventTrigger.OnCollisionEnterEvent.AddListener(CompareEducationModelEnter);
                collisionEventTrigger.OnCollisionExitEvent.AddListener(CompareEducationModelExit);
            }
            objectAnchors.Add(imageTarget.transform.GetChild(0).gameObject);
            EducationModel  arEducationModel = Instantiate(keyValuePair.Value, objectAnchors[i].transform);
            arEducationModel.transform.localPosition = Vector3.zero;
            arEducationModel.transform.localScale = Vector3.one / 2;
            arEducationModel.SetKey(keyValuePair.Key);
            i++;
        }
    
        
    }

    public void CompareEducationModelEnter(EducationModel one, EducationModel two)
    {
        
    }

    public void CompareEducationModelExit(EducationModel one, EducationModel two)
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
