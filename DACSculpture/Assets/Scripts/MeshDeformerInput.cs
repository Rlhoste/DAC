using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MeshDeformerInput : MonoBehaviour {
    //Mode 1
    public float force = 10f;
    public UnityEngine.UI.Text Tforce;
    public float forceOffset = 0.1f;
    public UnityEngine.UI.Text TforceOffset;
    //Mode2
    public float force2 = 10f;
    public UnityEngine.UI.Text Tforce1;
    public float forceOffset2 = 0.1f;
    public UnityEngine.UI.Text TforceOffset1;
    //Mode 3
    public float force3 = 10f;
    public UnityEngine.UI.Text Tforce2;
    public float forceOffset3 = 0.1f;
    public UnityEngine.UI.Text TforceOffset2;
    //Mode 4
    public float force4 = 10f;
    public UnityEngine.UI.Text Tforce3;
    public float forceOffset4 = 0.1f;
    public UnityEngine.UI.Text TforceOffset3;

    public UnityEngine.UI.Text TSizePlane;
    public float SizePlane;

    public UnityEngine.UI.Text Selectmode;
    public Material capMaterial;
    public Camera cam;
    public Vector3 point;
    private bool[] Mode = new bool[6];
   
    public GameObject[] ModeUI = new GameObject[6];
    private bool Ppressed = false;

    private GameObject victim;
    public GameObject slicer;
    


    // Use this for initialization
    void Start () {
       
    }
	
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            if (Ppressed == true) Ppressed = false;
            else Ppressed = true;
        }
        if(Ppressed == true)
        {
            if (Mode[0] == true)
            {
                Tforce.text = force.ToString();
                TforceOffset.text = forceOffset.ToString();
            }
            if (Mode[1] == true)
            {
                Tforce1.text = force2.ToString();
                TforceOffset1.text = forceOffset2.ToString();
            }
            if (Mode[2] == true)
            {
                Tforce2.text = force3.ToString();
                TforceOffset2.text = forceOffset3.ToString();
            }
            if (Mode[3] == true)
            {
                Tforce3.text = force4.ToString();
                TforceOffset3.text = forceOffset4.ToString();
            }
            if (Mode[4]== true)
            {
                TSizePlane.text = SizePlane.ToString();
                
            }
            if (Mode[5]==true)
            {
                Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(inputRay, out hit))
                {

                    MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
                    if (deformer)
                    {

                        if (Input.GetKey("up")) { deformer.transform.Translate(Vector3.up * Time.deltaTime); }
                        if (Input.GetKey("right")) { deformer.transform.Translate(Vector3.right * Time.deltaTime); }
                        if (Input.GetKey("left")) { deformer.transform.Translate(Vector3.left * Time.deltaTime); }
                        if (Input.GetKey("down")) { deformer.transform.Translate(Vector3.down * Time.deltaTime); }
                    }


                }
            }

            for (int i = 0; i < Mode.Length; i++)
            {
                if (Mode[i] == true)
                {
                    ModeUI[i].SetActive(true);

                }
                else
                {
                    ModeUI[i].SetActive(false);
                }
            }
            
        }
        //Deform 
        else
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                ModeUI[i].SetActive(false); 
            }
            if ((Input.GetMouseButton(0)) && (Mode[0]))
            {
                HandleInput();
            }
            if ((Input.GetMouseButton(0)) && (Mode[1]))
            {
                HandleInput2();
            }
            if ((Input.GetMouseButton(0)) && (Mode[2]))
            {
                HandleInput3();
            }
            if ((Input.GetMouseButton(0)) && (Mode[3]))
            {
                HandleInput4();
            }
            if (Mode[4])
            {
                if (Input.GetKey("up")) { slicer.transform.Rotate(Vector3.back * Time.deltaTime * 10f); }
                if (Input.GetKey("right")) { slicer.transform.Rotate(Vector3.right * Time.deltaTime * 10f); }
                if (Input.GetKey("left")) { slicer.transform.Rotate(Vector3.left * Time.deltaTime * 10f); }
                if (Input.GetKey("down")) { slicer.transform.Rotate(Vector3.forward * Time.deltaTime * 10f); }
                if (Input.GetMouseButton(0))
                {
                    HandleInput5();
                }

            }
            if  (Mode[5])
            {
                HandleInput6();

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                Mode[i] = false;
            }
            slicer.SetActive(false);
            Mode[0] = true;
            Selectmode.text = "Creuser normalisé";
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                Mode[i] = false;
            }
            slicer.SetActive(false);
            Mode[1] = true;
            Selectmode.text = "Extruder normalisé";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                Mode[i] = false;
            }
            slicer.SetActive(false);
            Mode[2] = true;
            Selectmode.text = "Extruder";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                Mode[i] = false;
            }
            Mode[3] = true;
            slicer.SetActive(false);
            Selectmode.text = "Creuser";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                Mode[i] = false;
            }
            slicer.SetActive(true);
            Mode[4] = true;
            Selectmode.text = "Couper";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                Mode[i] = false;
            }
            slicer.SetActive(false);
            Mode[5] = true;
            Selectmode.text = "Deplacer";
        }



    }

    void HandleInput()
    {
        Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);
       
        RaycastHit hit;
        
        if (Physics.Raycast(inputRay, out hit))
        {
            
            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                
                Vector3 point = hit.point;
                
                point += hit.normal * forceOffset;
                
                
                deformer.AddDeformingForce(point, force);

            }


        }

    }

    void HandleInput2()
    {
        Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {

            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                
                Vector3 point = hit.point;
                
                point -= hit.normal * forceOffset2;
                
                deformer.AddDeformingForce2(point, force2);

            }


        }

    }

    void HandleInput3()
    {
        Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {

            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                
                Vector3 point = hit.point;

                point += hit.normal * forceOffset3;

                deformer.AddDeformingForce3(point, force3,dir);

            }


        }

    }

    void HandleInput4()
    {
        Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {

            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
               
                Vector3 point = hit.point;

                point += hit.normal * forceOffset4;

                deformer.AddDeformingForce4(point, force4, dir);

            }


        }

    }
    //Slicer
    void HandleInput5()
    {


        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            victim = hit.collider.gameObject;

            GameObject[] pieces = MeshCut.Cut(victim, transform.position, transform.right, capMaterial);
            

        }

    }
    //Move
    void HandleInput6()
    {
        
            Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {

                MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
                if (deformer)
                {

                    if (Input.GetKey("up")) { deformer.transform.Translate(Vector3.up * Time.deltaTime * 10f); }
                    if (Input.GetKey("right")) { deformer.transform.Translate(Vector3.right * Time.deltaTime * 10f); }
                    if (Input.GetKey("left")) { deformer.transform.Translate(Vector3.left * Time.deltaTime * 10f); }
                    if (Input.GetKey("down")) { deformer.transform.Translate(Vector3.down * Time.deltaTime * 10f); }
                    
                    deformer.init();
                    
                }


            }
        
    }
    //Mode 1
    public void ajdustforce(float newForce)
    {
        force = newForce;

    }
    public void ajdustforceOffset(float newForceOffset)
    {
        forceOffset = newForceOffset;
    }
    //Mode 2
    public void ajdustforce1(float newForce)
    {
        force2 = newForce;

    }
    public void ajdustforceOffset1(float newForceOffset)
    {
        forceOffset2 = newForceOffset;
    }
    //Mode 3
    public void ajdustforce2(float newForce)
    {
        force3 = newForce;

    }
    public void ajdustforceOffset2(float newForceOffset)
    {
        forceOffset3 = newForceOffset;
    }
    //Mode 4
    public void ajdustforce3(float newForce)
    {
        force4 = newForce;

    }
    public void ajdustforceOffset3(float newForceOffset)
    {
        forceOffset4 = newForceOffset;
    }

}
