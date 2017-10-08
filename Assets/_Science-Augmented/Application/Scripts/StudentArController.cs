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
        
      CreateModels(data.EducationModels);
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
            int imageTargetNumber = i % ( imageTargetPrefabs.Count);
            Vuforia.ImageTargetBehaviour imageTarget = Instantiate(imageTargetPrefabs[imageTargetNumber]);

            OnCollisionEvents collisionEventTrigger = imageTarget.gameObject.GetComponentInChildren<OnCollisionEvents>();

            objectAnchors.Add(imageTarget.transform.GetChild(0).gameObject);
            EducationModel arEducationModel = Instantiate(models[i], objectAnchors[i].transform);
            arEducationModel.transform.localPosition = Vector3.zero;
            arEducationModel.transform.localScale = Vector3.one / 10;
            print(arEducationModel.transform.localScale);
            if (collisionEventTrigger != null)
            {
                collisionEventTrigger.OnCollisionEnterEvent.AddListener(CompareEducationModelEnter);
                collisionEventTrigger.OnCollisionExitEvent.AddListener(CompareEducationModelExit);
            }

        }
    }

    EducationModel demo;
    public void CompareEducationModelEnter(EducationModel one, EducationModel two)
    {
        if(demo != null)
            return;
        
        if (one.Value == two.Value)
        {
          demo = Instantiate(one, one.transform.position - two.transform.position,
                                              two.transform.parent.rotation);
            demo.gameObject.transform.localScale = Vector3.one/ 10;
            ShowChild demoChildren = demo.GetComponentInChildren<ShowChild>();
            demoChildren.ShowChildIndex(0);
            demoChildren.ShowChildIndex(1);
            RandomRotation demoRotation = demo.GetComponentInChildren<RandomRotation>();
            demoRotation.SetInPlace();

            for (int i = 0; i < one.transform.childCount; i++)
            {
                MeshRenderer render = one.transform.GetChild(i).GetComponent<MeshRenderer>();
                if (render)
                    render.enabled = false;
            }
            for (int i = 0; i < two.transform.childCount; i++)
            {
                MeshRenderer render = two.transform.GetChild(i).GetComponent<MeshRenderer>();
                if (render)
                    render.enabled = false;
            }

        }
    }

    public void CompareEducationModelExit(EducationModel one, EducationModel two)
    {
        if (demo == null)
            return;
            Destroy(demo.gameObject);
        for (int i = 0; i < one.transform.childCount; i++)
        {
            MeshRenderer render = one.transform.GetChild(i).GetComponent<MeshRenderer>();
            if (render)
                render.enabled = true;
        }
        for (int i = 0; i < two.transform.childCount; i++)
        {
            MeshRenderer render = two.transform.GetChild(i).GetComponent<MeshRenderer>();
            if (render)
                render.enabled = true;
        }
     
    }

    // Update is called once per frame
    void Update () {
		
	}
	#endif
}
