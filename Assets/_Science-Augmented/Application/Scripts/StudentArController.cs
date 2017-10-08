using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;


public class StudentArController : MonoBehaviour
{
	#if !UNITY_WEBGL
   
    public List<Vuforia.ImageTargetBehaviour> imageTargets = new List<Vuforia.ImageTargetBehaviour>();
    [SerializeField] Transform imageTargetParent;
    [SerializeField]
    ArView arView;





    void Awake()
    {
        imageTargets = new List<Vuforia.ImageTargetBehaviour>(imageTargetParent.GetComponentsInChildren<Vuforia.ImageTargetBehaviour>(true));
    }
    // Use this for initialization
    void Start ()
    {
        ChangeModel(new[] {1, 2});



        if (arView == null)
        {
            arView = FindObjectOfType<ArView>();
        }
        
        if (NetworkClassroomManager.sInstance)
        {
            NetworkClassroomManager.sInstance.OnPlayerModelsChange.AddListener(ChangeModel);
            NetworkClassroomManager.sInstance.OnStartAnimation.AddListener(ShowAnimation);
            NetworkClassroomManager.sInstance.OnStopAnimation.AddListener(PauseAnimation);
        }
        
       
     
    }



    void ChangeModel(int[] modelKeys)
    {
        int currentKey = 0;
        List< int> modelKeyList = new List<int>();
        for (int i = 0; i < imageTargets.Count; i++)
        {
            if (currentKey < modelKeys.Length)
            {
                modelKeyList.Add((modelKeys[currentKey]));
                currentKey++;
            }
            else
            {
                currentKey = 0;
                modelKeyList.Add(( modelKeys[currentKey] ));
            }
        }  
         CreateModels(modelKeyList);
    }

    public void SetInstructionText(string value)
    {
        arView.SetInstructionText(value);
    }




    public void CreateModels(List<int> models )
    {


        for (int i = 0; i < imageTargets.Count; i++)
        {
            Vuforia.ImageTargetBehaviour imageTarget = imageTargets[i];

            OnCollisionEvents collisionEventTrigger = imageTarget.gameObject.GetComponentInChildren<OnCollisionEvents>();
            ModelGroup group = imageTarget.GetComponentInChildren<ModelGroup>(true);
            group.showModel(models[i]);

            if (collisionEventTrigger != null)
            {

                collisionEventTrigger.OnCollisionEnterEvent.AddListener(CompareEducationModelEnter);
                collisionEventTrigger.OnCollisionExitEvent.AddListener(CompareEducationModelExit);
            }
        }

      
    }

    void ShowAnimation()
    {
        for (int i = 0; i < imageTargets.Count; i++)
        {
            Vuforia.ImageTargetBehaviour imageTarget = imageTargets[i];
            Animator animator = imageTarget.GetComponentInChildren<Animator>(true);
            if (animator && animator.enabled)
            {
                animator.Rebind();
            }
            if(animator)
            animator.enabled = true;
        }

    }

    void PauseAnimation()
    {
        for (int i = 0; i < imageTargets.Count; i++)
        {
            Vuforia.ImageTargetBehaviour imageTarget = imageTargets[i];
            Animator animator = imageTarget.GetComponentInChildren<Animator>(true);
            if (animator)
            {
                animator.enabled = false;
            }
    
        }

    }

    bool fit;
 
    public void StartAnimateModel(List<ModelGroup> models)
    {
     

   
        fit = true;
        for (int i = 0; i < models.Count; i++)
        {
           
            LerpChildPosition childEffects = models[i].ActivatedActiveModel.gameObject.GetComponentInChildren<LerpChildPosition>();
            if(childEffects)
            childEffects.StartLerpTo(Vector3.zero, 1);
            
        }
      
        StartCoroutine(StartSetInPlace(models, 50));
    }

    private IEnumerator StartSetInPlace(List<ModelGroup> models, float time = 50)
    {
      

        float elapsedTime = 0;
        Vector3 pos = Vector3.zero;
        List<ChangeColor> changeColorList = new List<ChangeColor>();
        for (int i = 0; i < models.Count; i++)
        {
            changeColorList.Add(models[i].GetComponentInChildren<ChangeColor>());
            pos += models[i].transform.position;
        }
        pos = pos / models.Count;
        foreach (ChangeColor changeColor in changeColorList)
        {
            changeColor.ChangeToColor(Color.yellow, 20);
        }
        while (elapsedTime < time)
        {
            for (int i = 0; i < models.Count; i++)
            {
                models[i].transform.eulerAngles = models[0].transform.eulerAngles;
                models[i].transform.position = Vector3.Lerp(models[i].transform.position, pos, ( elapsedTime / time ));
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void CompareEducationModelEnter(List<ModelGroup> models)
    {
       
        List<ModelGroup> CompareModel = new List<ModelGroup>();
        bool enzyme = false;
        EducationModel enzymeModel = null;
        for (int i = 0; i < models.Count; i++)
        {
            if (enzyme == false && models[i].IsEnzyme())
            {
                enzymeModel = models[i].ActivatedActiveModel;
                CompareModel.Add(models[i]);
                enzyme = true;
            }
            if (!models[i].IsEnzyme())
            {
                CompareModel.Add(models[i]);
            }
        }
        if (enzymeModel == null)
            return;
        models.RemoveAll(mod => mod.ActivatedActiveModel.Value != enzymeModel.Value);
        if (fit)
            return;
        if (CompareModel.Count < 2)
            return;
      
            StartAnimateModel(CompareModel);
        if (NetworkClassroomManager.sInstance)
            NetworkClassroomManager.sInstance.StudentComplete();
        fit = true;




    }

  

   
    public void CompareEducationModelExit(List<ModelGroup> models)
    {
        models.RemoveAll(mod => mod.ActivatedActiveModel.Value != models[0].ActivatedActiveModel.Value);
        List<ModelGroup> CompareModel = new List<ModelGroup>();
        bool enzyme = false;
        for (int i = 0; i < models.Count; i++)
        {
            if (enzyme == false && models[i].IsEnzyme())
            {
                CompareModel.Add(models[i]);
                enzyme = true;
            }
            if (!models[i].IsEnzyme())
            {
                CompareModel.Add(models[i]);
            }
        }

        if (fit == false)
            return;
        
        StopAllCoroutines();
        List<ChangeColor> changeColorList = new List<ChangeColor>();
        for (int i = 0; i < CompareModel.Count; i++)
        {
            changeColorList.Add(models[i].GetComponentInChildren<ChangeColor>());
            models[i].transform.localPosition = CompareModel[i].StartLocal;
            models[i].transform. localEulerAngles = Vector3.zero;
            LerpChildPosition childEffects = CompareModel[i].ActivatedActiveModel.gameObject.GetComponentInChildren<LerpChildPosition>();
            childEffects.LerpBackToStart(1);
        }
        foreach (ChangeColor changeColor in changeColorList)
        {
            changeColor.SetBackToStartColor(1);
        }
        
        if (NetworkClassroomManager.sInstance)
        NetworkClassroomManager.sInstance.StudentDestroy();
        fit = false;



    }
    
    


	#endif
}
