using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;


public class StudentArController : MonoBehaviour
{
	#if !UNITY_WEBGL
    [SerializeField]
    private List<Vuforia.ImageTargetBehaviour> imageTargets = new List<Vuforia.ImageTargetBehaviour>();

    [SerializeField] bool test;
    public EducationalModelData DataPrefab;
    private EducationalModelData data;
    [SerializeField]
    ArView arView;
    
   

   


    // Use this for initialization
    void Start ()
    {
        data = Instantiate(DataPrefab);

        if (test)
            StartCoroutine(testing());
        
        if (arView == null)
        {
            arView = FindObjectOfType<ArView>();
        }
        if (NetworkClassroomManager.sInstance)
        {
            NetworkClassroomManager.sInstance.OnPlayerModelsChange.AddListener(ChangeModel);
        }
     
    }

    IEnumerator testing()
    {
       yield return new WaitForSeconds(1);
      
            CreateModels();
        yield return null;
    }

    void ChangeModel(int[] modelKeys)
    {
        List<EducationModel> tempList = Enumerable.ToList(Enumerable.Where
                                                              (Enumerable.Select(modelKeys,
                                                              t => data.GetEducationModelById(t)),
                                                              model => model != null)); 
        CreateModels(tempList);
    }

    public void SetInstructionText(string value)
    {
        arView.SetInstructionText(value);
    }




    public void CreateModels()
    {
        
        
      CreateModels(data.EducationModels);
    }


    public void CreateModels(List<EducationModel> models )
    {

      
      

        for (int i = 0; i < models.Count; i++)
        {
            int imageTargetNumber = i % ( imageTargets.Count);
            Vuforia.ImageTargetBehaviour imageTarget = imageTargets[imageTargetNumber];

            OnCollisionEvents collisionEventTrigger = imageTarget.gameObject.GetComponentInChildren<OnCollisionEvents>();
            EducationModel model = imageTarget.GetComponentInChildren<EducationModel>();
            ShowChild childEnable = model.GetComponent<ShowChild>();
            model.Enzyme = models[i].Enzyme;
            model.Value = models[i].Value;
            model.CombinedValue = models[i].CombinedValue;
            if (childEnable)
            {
                childEnable.ChildToShow = models[i].Key - 1;
                childEnable.ShowChildIndex(models[i].Key - 1);
            }

            if (collisionEventTrigger != null)
            {
                print("Collition Events Created");
                collisionEventTrigger.OnCollisionEnterEvent.AddListener(CompareEducationModelEnter);
                collisionEventTrigger.OnCollisionExitEvent.AddListener(CompareEducationModelExit);
            }

        }
    }

    bool fit;
 
    public void StartAnimateModel(List<EducationModel> models)
    {
     

   
        fit = true;
        for (int i = 0; i < models.Count; i++)
        {
           
            LerpChildPosition childEffects = models[i].gameObject.GetComponentInChildren<LerpChildPosition>();
            childEffects.StartLerpTo(Vector3.forward * -4, 50);
        }
      
        StartCoroutine(StartSetInPlace(models, 50));
    }

    private IEnumerator StartSetInPlace(List<EducationModel> models, float time = 50)
    {
      

        float elapsedTime = 0;
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < models.Count; i++)
        {
            pos += models[i].transform.position;
        }
        pos = pos / models.Count;

        while (elapsedTime < time)
        {
            for (int i = 0; i < models.Count; i++)
            {
                models[i].transform.position = Vector3.Lerp(models[i].transform.position, pos, ( elapsedTime / time ));
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void CompareEducationModelEnter(List<EducationModel> models)
    {
        models.RemoveAll(mod => mod.Value != models[0].Value);
        if (fit)
            return;

            
            StartAnimateModel(models);
        fit = true;




    }

  

   
    public void CompareEducationModelExit(List<EducationModel> models)
    {
        models.RemoveAll(mod => mod.Value != models[0].Value);
        if (fit == false)
            return;
        StopAllCoroutines();

        for (int i = 0; i < models.Count; i++)
        {
            models[i].transform.localPosition = Vector3.zero;
            LerpChildPosition childEffects = models[i].gameObject.GetComponentInChildren<LerpChildPosition>();
            childEffects.LerpBackToStart(1);
        }
        
        fit = false;



    }
    
    


	#endif
}
