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

    bool fit;
    Vector3 startOne;
    Vector3 startTwo;

    public void SetDemoToModel(EducationModel one, EducationModel two)
    {
        fit = true;
        startOne = one.transform.position;
        startTwo = two.transform.position;
        StartCoroutine(StartSetInPlace(one, two));
    }

    private IEnumerator StartSetInPlace(EducationModel one, EducationModel two, float time = 30)
    {
      

        float elapsedTime = 0;

        RandomRotation[] demoRotations = one.GetComponentsInChildren<RandomRotation>();
        RandomMovement[] demoMovements = one.GetComponentsInChildren<RandomMovement>();

        RandomRotation[] demoRotationst = two.GetComponentsInChildren<RandomRotation>();
        RandomMovement[] demoMovementst = two.GetComponentsInChildren<RandomMovement>();
        
        for (int i = 0; i < demoRotations.Length; i++)
        {
            demoRotations[i].SetInPlace();
        }

        for (int i = 0; i < demoMovements.Length; i++)
        {
            demoMovements[i].SetBack();
        }

        for (int i = 0; i < demoRotationst.Length; i++)
        {
            demoRotationst[i].SetInPlace();
        }

        for (int i = 0; i < demoMovementst.Length; i++)
        {
            demoMovementst[i].SetBack();
        }

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
        

      

        
    }

    IEnumerator SetBack(EducationModel one, EducationModel two, float time = 30)
    {


        float elapsedTime = 0;

        one.transform.position = startOne;
        two.transform.position = startTwo;
        //while (elapsedTime < time)
        //{
        //one.transform.localPosition = Vector3.Lerp(one.transform.localPosition, startOne, ( elapsedTime / time ));
        //    two.transform.localPosition = Vector3.Lerp(two.transform.localPosition,startTwo, ( elapsedTime / time ));
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}

        RandomRotation[] demoRotations = one.GetComponentsInChildren<RandomRotation>();
        RandomMovement[] demoMovements = one.GetComponentsInChildren<RandomMovement>();

        RandomRotation[] demoRotationst = two.GetComponentsInChildren<RandomRotation>();
        RandomMovement[] demoMovementst = two.GetComponentsInChildren<RandomMovement>();

        for (int i = 0; i < demoRotations.Length; i++)
        {
            demoRotations[i].StartRandomRotation();
        }

        for (int i = 0; i < demoMovements.Length; i++)
        {
            demoMovements[i].SetRandom();
        }

        for (int i = 0; i < demoRotationst.Length; i++)
        {
            demoRotationst[i].StartRandomRotation();
        }

        for (int i = 0; i < demoMovementst.Length; i++)
        {
            demoMovementst[i].SetRandom();
        }
        fit = false;
        yield return null;
    }

   
    public void CompareEducationModelExit(EducationModel one, EducationModel two)
    {
        if (fit == false)
            return;
        StopAllCoroutines();


        one.transform.position = startOne;
        two.transform.position = startTwo;
   

        RandomRotation[] demoRotations = one.GetComponentsInChildren<RandomRotation>();
        RandomMovement[] demoMovements = one.GetComponentsInChildren<RandomMovement>();

        RandomRotation[] demoRotationst = two.GetComponentsInChildren<RandomRotation>();
        RandomMovement[] demoMovementst = two.GetComponentsInChildren<RandomMovement>();

        for (int i = 0; i < demoRotations.Length; i++)
        {
            demoRotations[i].StartRandomRotation();
        }

        for (int i = 0; i < demoMovements.Length; i++)
        {
            demoMovements[i].SetRandom();
        }

        for (int i = 0; i < demoRotationst.Length; i++)
        {
            demoRotationst[i].StartRandomRotation();
        }

        for (int i = 0; i < demoMovementst.Length; i++)
        {
            demoMovementst[i].SetRandom();
        }
        fit = false;



    }


	#endif
}
