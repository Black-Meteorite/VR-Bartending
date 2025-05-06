using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BeerTapLever : MonoBehaviour
{

    public Transform pivot;

    public float minAngle = 0f;
    public float maxAngle = 60f;
    public float rotationSpeed = 100f;

    public ParticleSystem beerEffect;

    private XRBaseInteractor interactor;

    private bool isGrabbed = false;
    private float currentAngle= 0f;
    private Vector3 initialLocalHandPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrabbed || interactor == null) return;

        
        Vector3 worldHandPos = interactor.transform.position;
        Vector3 localHandPos = pivot.InverseTransformPoint(worldHandPos);

        float delta = localHandPos.x - initialLocalHandPos.x;
        float targetAngle = Mathf.Clamp(currentAngle + delta*rotationSpeed,minAngle,maxAngle);
        pivot.localRotation= Quaternion.Euler(currentAngle, 0f,0f);

        if(currentAngle >=30f)
        {
            if(!beerEffect.isPlaying){
                beerEffect.Play();
            }
        }
        else
        {
            if(beerEffect.isPlaying)
            {
                beerEffect.Stop();
            }
        }
    }

    void OnGrab(SelectEnterEventArgs args){
        isGrabbed = true;
        interactor = args.interactableObject.transform.GetComponent<XRBaseInteractor>();
    }

    void OnRelease(SelectExitEventArgs args){
        isGrabbed = false;
        interactor = null;
        beerEffect.Stop();
    }
}

