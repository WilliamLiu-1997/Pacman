using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    //private Tween activeTween;
    private Tween activeTween;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween != null){
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.05f)
                {
                    float fraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
                    activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fraction);
                }
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) <= 0.05f)
                {
                    activeTween.Target.position = activeTween.EndPos;
                    activeTween = null;
                }
        }
    }


    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (activeTween == null)
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        }
    }

    public void DeleteTween(){
        activeTween = null;
    }
}
