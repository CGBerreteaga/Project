using Cinemachine;
using UnityEngine;

public class CameraTargetLock : MonoBehaviour
{
    public CinemachineVirtualCamera cineMachine;
    public GameObject originPosition;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnTargetLock += TargetCamera;
        
        cineMachine = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward = target.transform.position - transform.position;
        cineMachine.LookAt = target.transform;

    }

    public void TargetCamera(GameObject instigator) {
        target = instigator;
    }
}
