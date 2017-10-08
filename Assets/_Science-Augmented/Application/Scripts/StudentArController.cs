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
           
            if (collisionEventTrigger != null)
            {
                collisionEventTrigger.OnCollisionEnterEvent.AddListener(CompareEducationModelEnter);
                collisionEventTrigger.OnCollisionExitEvent.AddListener(CompareEducationModelExit);
            }

        }
    }

    bool fit;
 
    public void SetDemoToModel(EducationModel one, EducationModel two)
    {
        fit = true;
        LerpChildPosition childEffectsOne = one.gameObject.GetComponentInChildren<LerpChildPosition>();
        LerpChildPosition childEffectsTwo = two.gameObject.GetComponentInChildren<LerpChildPosition>();
        childEffectsOne.StartLerpTo(Vector3.forward * -4, 50);
        childEffectsTwo.StartLerpTo(Vector3.forward * -4, 50);
        StartCoroutine(StartSetInPlace(one, two, 50));
    }

    private IEnumerator StartSetInPlace(EducationModel one, EducationModel two, float time = 50)
    {
      

        float elapsedTime = 0;


        while (elapsedTime < time)
        {
            one.transform.position = Vector3.Lerp(one.transform.position, (one.transform.position+ two.transform.position)/2, ( elapsedTime / time ));
            two.transform.position = Vector3.Lerp(two.transform.position, ( one.transform.position + two.transform.position ) / 2, ( elapsedTime / time ));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void CompareEducationModelEnter(EducationModel one, EducationModel two)
    {
        if (fit)
            return;

            
            SetDemoToModel(one,two);
        fit = true;




    }

  

   
    public void CompareEducationModelExit(EducationModel one, EducationModel two)
    {
        if (fit == false)
            return;
        StopAllCoroutines();
        one.transform.localPosition = Vector3.zero;
        two.transform.localPosition = Vector3.zero;
        LerpChildPosition childEffectsOne = one.gameObject.GetComponentInChildren<LerpChildPosition>();
        LerpChildPosition childEffectsTwo = two.gameObject.GetComponentInChildren<LerpChildPosition>();
        childEffectsOne.LerpBackToStart( 1);
        childEffectsTwo.LerpBackToStart(1);
        fit = false;



    }


	#endif
}
