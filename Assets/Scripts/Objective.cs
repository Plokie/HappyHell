using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum ObjectiveType
{
    None, NearestObjectOfType
}

public class ObjectiveQuest
{
    public ObjectiveType objectiveType = ObjectiveType.None;
    public Transform targetTransform;
    public Vector3 targetPosition;
    public string text;
    public string id;
    public GameObject instantiatedPointer = null;

    public Type typeArg;
    public System.Func<bool> isCompletedQuery;

    public ObjectiveQuest(string id, Transform targetTransform, string text, Func<bool> isCompletedQuery)
    {
        this.targetTransform = targetTransform;
        this.text = text;
        this.id = id;
        this.isCompletedQuery = isCompletedQuery;
    }

    public ObjectiveQuest(string id, Type typeArg, string text,  Func<bool> isCompletedQuery)
    {
        this.targetTransform = null;
        this.targetPosition = Vector3.zero;
        this.text = text;
        this.id = id;
        this.objectiveType = ObjectiveType.NearestObjectOfType;
        this.typeArg = typeArg;
        this.isCompletedQuery = isCompletedQuery;
    }

    public ObjectiveQuest(string id, Vector3 targetPosition, string text, Func<bool> isCompletedQuery)
    {
        this.targetTransform = null;
        this.targetPosition = targetPosition;
        this.text = text;
        this.id = id;
        this.isCompletedQuery = isCompletedQuery;
    }


}


public class Objective : MonoBehaviour
{

    public static Objective Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    List<ObjectiveQuest> currentQuests = new List<ObjectiveQuest>();
    List<ObjectiveQuest> queuedQuests = new List<ObjectiveQuest>();

    public static ObjectiveQuest AddObjective(string id, string text, Transform target, System.Func<bool> isCompletedQuery) {
        ObjectiveQuest newObjective = new ObjectiveQuest(id, target, text, isCompletedQuery);
        Instance.currentQuests.Add(newObjective);
        return newObjective;
    }

    public static ObjectiveQuest AddObjective(string id, string text, Vector3 target, System.Func<bool> isCompletedQuery)
    {
        ObjectiveQuest newObjective = new ObjectiveQuest(id, target, text, isCompletedQuery);
        Instance.currentQuests.Add(newObjective);
        return newObjective;
    }

    public static ObjectiveQuest AddObjective(string id, string text, System.Type typeArg, System.Func<bool> isCompletedQuery)
    {
        ObjectiveQuest newObjective = new ObjectiveQuest(id, typeArg, text, isCompletedQuery);
        Instance.currentQuests.Add(newObjective);
        return newObjective;
    }

    public static ObjectiveQuest QueueObjective(string id, string text, Transform target, System.Func<bool> isCompletedQuery) {
        ObjectiveQuest newObjective = new ObjectiveQuest(id, target, text, isCompletedQuery);
        Instance.queuedQuests.Add(newObjective);
        return newObjective;
    }

    public static ObjectiveQuest QueueObjective(string id, string text, Vector3 target, System.Func<bool> isCompletedQuery)
    {
        ObjectiveQuest newObjective = new ObjectiveQuest(id, target, text, isCompletedQuery);
        Instance.queuedQuests.Add(newObjective);
        return newObjective;
    }

    public static ObjectiveQuest QueueObjective(string id, string text, System.Type typeArg, System.Func<bool> isCompletedQuery)
    {
        ObjectiveQuest newObjective = new ObjectiveQuest(id, typeArg, text, isCompletedQuery);
        Instance.queuedQuests.Add(newObjective);
        return newObjective;
    }

    private void Start()
    {
        
    }

    void Update() {
        if(currentQuests.Count == 0 && queuedQuests.Count > 0) {
            currentQuests.Add(queuedQuests[0]);
            queuedQuests.RemoveAt(0);
        }
        
        foreach(ObjectiveQuest quest in Instance.currentQuests)
        {
            if(quest.instantiatedPointer==null)
            {
                //Instantiate the pointer ui
            }


            if(quest.isCompletedQuery())
            {
                //Destroy the pointer ui
                Instance.currentQuests.Remove(quest);
            }
            else
            {
                Vector3 targetPosition = Vector3.zero;
                
                switch(quest.objectiveType)
                {
                    case ObjectiveType.None: targetPosition = (quest.targetTransform == null) ? quest.targetPosition : quest.targetTransform.position; break;
                    case ObjectiveType.NearestObjectOfType:

                        float nearestDist = Mathf.Infinity;
                        UnityEngine.Object nearestObj = null;

                        var arr = GameObject.FindObjectsOfType(quest.typeArg); //slow and bad but MEH game jam innit
                        foreach (var obj in arr) {
                            Transform objTransform = obj.GetComponent<Transform>();
                            if (objTransform != null)
                            {
                                float dist = Vector3.Distance(objTransform.position, )

                            }

                        }

                        break;
                }
                
            }
        }
        
    }
}
