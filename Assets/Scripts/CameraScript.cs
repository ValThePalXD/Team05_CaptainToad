using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform AnchorPointTransform;
    public Transform VertAnchorTransform;
    public Transform CamZoomOut;
    public Transform CamZoomIn;
    public Transform CamTransform;

    public float RotationSpeed;
    public float ZoomParameter;

    private float _camRotation = 0;
    private Vector2 _camClamp = new Vector2(-50, 0);

    private bool IsZoomedIn;

	// Use this for initialization
	void Start () {
        AnchorPointTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        RotateCam();
        Zoom();
	}

    void RotateCam()
    {
        AnchorPointTransform.Rotate(new Vector3(0,Input.GetAxis("Horizontal"),0) *RotationSpeed*Time.deltaTime);
        //VertAnchorTransform.Rotate(new Vector3(Input.GetAxis("Vertical"),0,0)*RotationSpeed*Time.deltaTime);

        //vertical rotation
        _camRotation -= Input.GetAxis("Vertical") * RotationSpeed * Time.deltaTime; //calculate vertical rotation
                                                                                   //clamp cam
        _camRotation = Mathf.Clamp(_camRotation, _camClamp.x, _camClamp.y); //clamp vertical rotation
                                                                            //set values
        VertAnchorTransform.eulerAngles = new Vector3(-_camRotation, VertAnchorTransform.eulerAngles.y, VertAnchorTransform.eulerAngles.z); //apply vertical rotation to camera transform

    }

    void Zoom()
    {
        if (Input.GetButtonDown("Jump"))
        {
                IsZoomedIn = !IsZoomedIn;
        }
        if (IsZoomedIn)
        {
            CamTransform.position = Vector3.Lerp(CamTransform.position, CamZoomIn.position, ZoomParameter);
        }
        else
        {
            CamTransform.position = Vector3.Lerp(CamTransform.position, CamZoomOut.position, ZoomParameter);
        }
    }
}
