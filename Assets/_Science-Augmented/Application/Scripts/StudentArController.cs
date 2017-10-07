using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StudentArController : MonoBehaviour
{
	#if !UNITY_WEBGL
    [SerializeField]
    private List<Vuforia.ImageTargetBehaviour> imageTargetPrefabs = new List<Vuforia.ImageTargetBehaviour>();

    public EducationalModelData DataPrefab;
    private EducationalModelData data;
    [SerializeField]
    ArView arView;

   
    private List<GameObject> objectAnchors = new List<GameObject>();
   


    // Use this for initialization
    void Start ()
    {
        data = Instantiate(DataPrefab);
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
        for (int i = 0; i < objectAnchors.Count; i++)
        {
            Destroy(objectAnchors[i].transform.parent.gameObject);
        }
        objectAnchors = new List<GameObject>();
        
        for (int i = 0; i < data.EducationModels.Count; i++)
        {
            int imageTargetNumber = i % (imageTargetPrefabs.Count);
            Vuforia.ImageTargetBehaviour imageTarget = Instantiate(imageTargetPrefabs[imageTargetNumber]);

            OnCollisionEvents collisionEventTrigger = imageTarget.gameObject.GetComponentInChildren<OnCollisionEvents>();

            objectAnchors.Add(imageTarget.transform.GetChild(0).gameObject);
            EducationModel arEducationModel = Instantiate(data.EducationModels[i], objectAnchors[i].transform);
            arEducationModel.transform.localPosition = Vector3.zero;
            arEducationModel.transform.localScale = Vector3.one / 2;

            if (collisionEventTrigger != null)
            {
                collisionEventTrigger.OnCollisionEnterEvent.AddListener(CompareEducationModelEnter);
                collisionEventTrigger.OnCollisionExitEvent.AddListener(CompareEducationModelExit);
            }

        }
    }


    public void CreateModels(List<EducationModel> models )
    {
        for (int i = 0; i < objectAnchors.Count; i++)
        {
            Destroy(objectAnchors[i].transform.parent.gameObject);
        }
        objectAnchors = new List<GameObject>();
        
        for (int i = 0; i < models.Count; i++)
        {
            int imageTargetNumber = i % ( imageTargetPrefabs.Count - 1 );
            Vuforia.ImageTargetBehaviour imageTarget = Instantiate(imageTargetPrefabs[imageTargetNumber]);

            OnCollisionEvents collisionEventTrigger = imageTarget.gameObject.GetComponent<OnCollisionEvents>();

            objectAnchors.Add(imageTarget.transform.GetChild(0).gameObject);
            EducationModel arEducationModel = Instantiate(models[i], objectAnchors[i].transform);
            arEducationModel.transform.localPosition = Vector3.zero;
            arEducationModel.transform.localScale = Vector3.one / 2;

            if (collisionEventTrigger != null)
            {
                collisionEventTrigger.OnCollisionEnterEvent.AddListener(CompareEducationModelEnter);
                collisionEventTrigger.OnCollisionExitEvent.AddListener(CompareEducationModelExit);
            }

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
	#endif
}
