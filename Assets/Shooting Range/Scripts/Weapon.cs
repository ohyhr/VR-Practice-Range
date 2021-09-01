using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : XRGrabInteractable
{
    public float breakDistance = 0.25f;
    public int recoilAmount = 25;
    
    private GripHold gripHold = null;
    private GuardHold guardHold = null;

    private new Rigidbody rigidbody = null;
    private Barrel barrel = null;

    private XRBaseInteractor gripHand = null;
    private XRBaseInteractor guardHand = null;

    private readonly Vector3 gripRotation = new Vector3(45, 0, 0);

    protected override void Awake()
    {
        base.Awake();
        SetupHolds();
        SetupExtras();
        onSelectEntered.AddListener(SetInitialRotation);
    }

    private void SetupHolds()
    {
        gripHold = GetComponentInChildren<GripHold>();
        gripHold.Setup(this);

        guardHold = GetComponentInChildren<GuardHold>();
        guardHold.Setup(this);
    }

    private void SetupExtras()
    {
        rigidbody = GetComponent<Rigidbody>();
        barrel = GetComponentInChildren<Barrel>();
        barrel.Setup(this);
    }

    private new void OnDestroy()
    {
        onSelectEntered.RemoveListener(SetInitialRotation);
    }

    private void SetInitialRotation(XRBaseInteractor interactor)
    {
        Quaternion newRotation = Quaternion.Euler(gripRotation);
        interactor.attachTransform.localRotation = newRotation;
    }

    public void SetGripHand(XRBaseInteractor interactor)
    {
        gripHand = interactor;
        OnSelectEntering(gripHand);
    }

    public void ClearGripHand(XRBaseInteractor interactor)
    {
        gripHand = null;
        OnSelectExiting(interactor);
    }

    public void SetGuardHand(XRBaseInteractor interactor)
    {
        guardHand = interactor;
    }

    public void ClearGuardHand(XRBaseInteractor interactor)
    {
        guardHand = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(gripHand && guardHand)
            SetGripRotation();

        CheckDistance(gripHand, gripHold);
        CheckDistance(guardHand, guardHold);
    }

    private void SetGripRotation()
    {
        Vector3 target = guardHand.transform.position - gripHold.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(target);

        Vector3 gripRotation = Vector3.zero;
        gripRotation.z = gripHand.transform.eulerAngles.z;

        lookRotation *= Quaternion.Euler(gripRotation);
        gripHand.attachTransform.rotation = lookRotation;
    }

    private void CheckDistance(XRBaseInteractor interactor, HandHold handHold)
    {
        if(interactor) 
        {
            float distanceSqr = GetDistanceSqrToInteractor(interactor);

            if(distanceSqr > breakDistance)
            {
                handHold.BreakHold(interactor);
            }
        }
    }

    public void PullTrigger()
    {
        barrel.StartFiring();
    }

    public void ReleaseTrigger()
    {
        barrel.StopFiring();
    }

    public void ApplyRecoil()
    {
        rigidbody.AddRelativeForce(Vector3.back * recoilAmount, ForceMode.Impulse);
    }
}
