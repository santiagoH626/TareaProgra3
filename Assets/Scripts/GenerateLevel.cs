using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public float noiseSpeed = 0.01f;
    [Header("Probabilities (0f-1f)")]
    public float actionsPerIteration;
    public float nothingProb;
    public float boxProb;
    public float panelProb;
    public float columnProb;
    public float rowProb;
    public float wallProb;
    public float donutProb;

    public float bonusProb;

    [Header("Multipliers (1 = no change)")]
    public float actionsMult = 1.0f;
    public float nothingProbMult = 1.0f;
    public float boxProbMult = 1.0f;
    public float panelProbMult = 1.0f;
    public float columnProbMult = 1.0f;
    public float rowProbMult = 1.0f;
    public float wallProbMult = 1.0f;
    public float donutProbMult = 1.0f;

    public float bonusProbMult = 1.0f;

    [Header("Increments (0 = no change)")]
    public float actionsAdd = 0.0f;
    public float nothingProbAdd = 0.0f;
    public float boxProbAdd = 0.0f;
    public float panelProbAdd = 0.0f;
    public float columnProbAdd = 0.0f;
    public float rowProbAdd = 0.0f;
    public float wallProbAdd = 0.0f;
    public float donutProbAdd = 0.0f;

    public float bonusProbAdd = 0.0f;

    [Header("Prefabs")]
    public GameObject prefabCubo;
    public GameObject prefabPoste;
    public GameObject prefabFloor;

    [Header("Bonus")]
    public GameObject prefabBonus;
    public float sinAmp;
    public float sinFreqMult;
    public GameObject[] prefabs;

    [Header("Level Details")]
    public Vector3 offset;
    public int generationAmount;
    public float levelWidth;
    public float levelHeight;


    [Header("Checkpoint")]
    public GameObject checkpoint;
    public float checkpointZ;
    public float createZ;

    // Start is called before the first frame update
    void Start()
    {        
        /*
        GameObject ff = Instantiate(prefabFloor);
        ff.transform.localScale = new Vector3(levelWidth*0.1f, 1f, 0.1f*generationAmount*offset.z);
        ff.transform.position = new Vector3(0f, 0f, ff.transform.localScale.z*5f);
        for (int i = 0; i < generationAmount; i++)
        {
            //GameObject o = Instantiate(prefabs[Random.Range(0,prefabs.Length)]);
            //o.transform.Translate(new Vector3 (Random.Range(-offset.x,offset.x),Random.Range(0.0f,offset.y),i*offset.z));

            //Color cc = new Color(240 + Random.Range(0, 15), 240 + Random.Range(0, 15), 240 + Random.Range(0, 15));
            //o.GetComponent<Material>().color = cc;
            for (float j = 0; j<actionsPerIteration;j++){
                float rr = Random.Range(0.0f,boxProb+panelProb+columnProb+rowProb+wallProb+donutProb+nothingProb);
                string ss = "";
                if (rr<boxProb){
                    GameObject o = CreateBox();
                    o.transform.position = new Vector3(o.transform.position.x,o.transform.position.y,i*offset.z);
                    CreateBonus(o);
                    ss = "box" + rr.ToString(); 

                } else if (rr< boxProb+panelProb){
                    GameObject o = CreatePanel();
                    o.transform.Translate(new Vector3(0f,0f,i*offset.z));
                    ss = "panel" + rr.ToString();

                } else if (rr< boxProb+panelProb+columnProb){
                    GameObject o = CreateColumn();
                    o.transform.Translate(new Vector3(0f,0f,i*offset.z));
                    ss = "column" + rr.ToString();

                } else if (rr<boxProb+panelProb+columnProb+rowProb){
                    GameObject o = CreateRow();
                    o.transform.Translate(new Vector3(0f,0f,i*offset.z));
                    j++;
                    ss = "row" + rr.ToString();

                } else if (rr<boxProb+panelProb+columnProb+rowProb+wallProb){
                    if (j==0){
                        if (Random.Range(0f,1f)>0.9f){
                            
                                GameObject o = CreateWall();
                                o.transform.Translate(new Vector3(0f,0f, createZ + i*offset.z));
                                ss = "wall" + rr.ToString();
                            
                        } else {
                            CreateDoubleWall(createZ + i*offset.z);
                        }
                        j = actionsPerIteration;
                    }

                } else if (rr<boxProb+panelProb+columnProb+rowProb+wallProb+donutProb){
                    if (j==0){
                        GameObject o = CreateDonut();
                        o.transform.Translate(new Vector3(0f,0f, createZ + i*offset.z));
                        ss = "donut" + rr.ToString();
                        j = actionsPerIteration;
                    }
                }
                Debug.Log(ss);
            }
        }
        checkpointZ = generationAmount*offset.z*0.5f;
        checkpoint.transform.position = new Vector3(0f,levelHeight,checkpointZ);
        */
        createZ = 0f;
        CreateChunk();
    }

    public void CreateChunk(){

        GameObject ff = Instantiate(prefabFloor);
        ff.transform.localScale = new Vector3(levelWidth*0.1f, 1f, 0.1f*generationAmount*offset.z);
        ff.transform.position = new Vector3(0f, 0f, createZ + ff.transform.localScale.z*5f);
        for (int i = 0; i < generationAmount; i++)
        {
            //GameObject o = Instantiate(prefabs[Random.Range(0,prefabs.Length)]);
            //o.transform.Translate(new Vector3 (Random.Range(-offset.x,offset.x),Random.Range(0.0f,offset.y),i*offset.z));

            //Color cc = new Color(240 + Random.Range(0, 15), 240 + Random.Range(0, 15), 240 + Random.Range(0, 15));
            //o.GetComponent<Material>().color = cc;
            for (float j = 0; j<actionsPerIteration;j++){
                float rr = Random.Range(0.0f,boxProb+panelProb+columnProb+rowProb+wallProb+donutProb+nothingProb);
                string ss = "";
                if (rr<boxProb){//                                                        box
                    GameObject o = CreateBox();
                    o.transform.position = new Vector3(o.transform.position.x,o.transform.position.y, createZ + i*offset.z);
                    CreateBonus(o);
                    ss = "box" + rr.ToString(); 

                } else if (rr< boxProb+panelProb){//                                     panel
                    GameObject o = CreatePanel();
                    o.transform.Translate(new Vector3(0f,0f, createZ + i*offset.z));
                    ss = "panel" + rr.ToString();

                } else if (rr< boxProb+panelProb+columnProb){//                          column
                    GameObject o = CreateColumn();
                    o.transform.Translate(new Vector3(0f,0f, createZ + i*offset.z));
                    ss = "column" + rr.ToString();

                } else if (rr<boxProb+panelProb+columnProb+rowProb){//                     row
                    if (j==0){
                        GameObject o = CreateRow();
                        o.transform.Translate(new Vector3(0f,0f, createZ + i*offset.z));
                        ss = "row" + rr.ToString();
                        j = actionsPerIteration+1;
                    }
                    //j++;

                } else if (rr<boxProb+panelProb+columnProb+rowProb+wallProb){//            wall
                    if (j==0){
                        if (Random.Range(0f,1f)>0.9f){
                            
                                GameObject o = CreateWall();
                                o.transform.Translate(new Vector3(0f,0f, createZ + i*offset.z));
                                ss = "wall" + rr.ToString();
                            
                        } else {
                            CreateDoubleWall(createZ + i*offset.z);
                        }
                        j = actionsPerIteration+1;
                    }

                } else if (rr<boxProb+panelProb+columnProb+rowProb+wallProb+donutProb){//  donut
                    if (j==0){
                        GameObject o = CreateDonut();
                        o.transform.Translate(new Vector3(0f,0f, createZ + i*offset.z));
                        ss = "donut" + rr.ToString();
                        j = actionsPerIteration+1;
                    }
                }
                Debug.Log(ss);
            }
        }


        createZ+=generationAmount*offset.z;
        checkpoint.transform.position = new Vector3(0f, levelHeight, checkpointZ);
        checkpointZ+= generationAmount*offset.z;

        ProbVariation();
    }
    void ProbVariation(){
        nothingProb = nothingProb * nothingProbMult; 
        boxProb     = boxProb     *     boxProbMult; 
        panelProb   = panelProb   *   panelProbMult; 
        columnProb  = columnProb  *  columnProbMult; 
        rowProb     = rowProb     *     rowProbMult; 
        wallProb    = wallProb    *    wallProbMult; 
        donutProb   = donutProb   *   donutProbMult;

        bonusProb   = bonusProb   *   bonusProbMult; 


        nothingProb = nothingProb + nothingProbAdd; 
        boxProb     = boxProb     +     boxProbAdd; 
        panelProb   = panelProb   +   panelProbAdd; 
        columnProb  = columnProb  +  columnProbAdd; 
        rowProb     = rowProb     +     rowProbAdd; 
        wallProb    = wallProb    +    wallProbAdd;
        donutProb   = donutProb   +   donutProbAdd;

        bonusProb   = bonusProb   +   bonusProbAdd; 
    }

    
    GameObject CreateBox(){
        float nx = 5f+10f*Random.Range(0f,1f);
        float ny = 5f+10f*Random.Range(0f,1f);
        float nz = offset.z*(0.25f+0.75f*Random.Range(0,3));
        GameObject o = Instantiate(prefabCubo);
        o.transform.localScale = new Vector3(nx,ny,nz);
        o.transform.position = new Vector3(Random.Range(-levelWidth*0.5f+nx*0.5f,levelWidth*0.5f-nx*0.5f),ny*0.5f,0f);
        return o;
    }
    GameObject CreatePanel(){
        float nx = 4f+16f*Random.Range(0f,1f);
        float ny = 4f+16f*Random.Range(0f,1f);
        float nz = 1f;//5f+20f*Random.Range(0f,1f);

        GameObject o = Instantiate(prefabCubo);
        o.transform.localScale = new Vector3(nx,ny,nz);
        o.transform.position = new Vector3(Random.Range(-levelWidth*0.5f+nx*0.5f,levelWidth*0.5f-nx*0.5f), levelHeight*0.5f+levelHeight*0.5f*Random.Range(0f,1f), 0f);

        GameObject ob = Instantiate(prefabPoste);
        ob.transform.localScale = new Vector3(nz*0.5f,o.transform.position.y*0.5f,nz*0.5f);
        ob.transform.position = o.transform.position;
        ob.transform.Translate(new Vector3(0f,-ob.transform.localScale.y,0f));
        ob.transform.parent = o.transform;

        return o;
    }
    GameObject CreateColumn(){
        float nx = 4f+16f*Random.Range(0f,1f);
        float ny = levelHeight+levelHeight*Random.Range(0f,1f);
        float nz = nx;//5f+20f*Random.Range(0f,1f);
        GameObject o = Instantiate(prefabCubo);
        o.transform.localScale = new Vector3(nx,ny,nz);
        o.transform.position = new Vector3(Random.Range(-levelWidth*0.5f+nx*0.5f,levelWidth*0.5f-nx*0.5f),ny*0.5f,0f);
        return o;
    }

    GameObject CreateRow(){
        float nx = levelWidth;
        float ny = 2f+5f*Random.Range(0f,1f);
        float nz = 1f;//5f+20f*Random.Range(0f,1f);
        GameObject o = Instantiate(prefabCubo);
        o.transform.localScale = new Vector3(nx,ny,nz);
        o.transform.position = new Vector3(0f,Random.Range(ny*0.5f,levelHeight-ny*0.5f),0f);
        return o;
    }
    GameObject CreateWall(){
        float nx = levelWidth * Random.Range(0.2f,0.4f);
        float ny = levelHeight * 1.2f;
        float nz = offset.z * (0.05f);
        bool side = Random.Range(0.0f,1.0f)>0.5f;

        GameObject o = Instantiate(prefabCubo);
        o.transform.localScale = new Vector3(nx,ny,nz);
        o.transform.position = new Vector3((side?1.0f:-1.0f)* (levelWidth*0.5f-nx*0.5f), ny*0.5f, 0f);
        return o;
    }
    void CreateDoubleWall(float z){
        //pared con punto medio entre 0.35 y 0.65 de levelWidth
        float nx = levelWidth;
        float ny = levelHeight * 1.2f;
        float nz = 1f;

        float wallMin = nx*0.15f;
        float dd = 0.2f*nx;

        //float dx = Random.Range(dd+wallMin,nx-dd-wallMin);
        float noise = Mathf.PerlinNoise(100f,z*noiseSpeed);
        float dx = wallMin+dd*0.5f+(nx-2f*wallMin-dd)*noise;//Random.Range(dd+wallMin,nx-dd-wallMin);

        float da = dx-dd*0.5f;
        float db = nx-(dx+dd*0.5f);
        //if (da+db+dd>nx){
            //Debug.Log(dx.ToString()+" : "+da.ToString()+", "+db.ToString()+", "+dd.ToString()+"/"+nx.ToString());
        //}
        GameObject oa = Instantiate(prefabCubo);
        oa.transform.localScale = new Vector3(da, ny, nz);
        oa.transform.position = new Vector3(-nx*0.5f+oa.transform.localScale.x*0.5f, ny*0.5f,z);
        
        GameObject ob = Instantiate(prefabCubo);
        ob.transform.localScale = new Vector3(db, ny, nz);
        ob.transform.position = new Vector3(nx*0.5f-ob.transform.localScale.x*0.5f, ny*0.5f,z);
        
    }

    GameObject CreateDonut(){
        //escala total
        float dd = 6f;//"diámetro"

        float nx = 2f*dd+2f*dd*Random.Range(0f,1f);
        float ny = 2f*dd+2f*dd*Random.Range(0f,1f);
        float nz = 1.0f;//levelWidth*0.1f;



        //posición central del agujero
        float dx = 1f+dd*0.5f+Random.Range(0f,nx-dd-2f);
        float dy = 1f+dd*0.5f+Random.Range(0f,ny-dd-2f);

        

        GameObject o = new GameObject();
        o.transform.position = new Vector3(Random.Range(-levelWidth*0.5f+nx*0.5f,levelWidth*0.5f-nx*0.5f),ny*0.5f+Random.Range(0.0f,levelHeight-ny*0.5f),0f);

        GameObject oa = Instantiate(prefabCubo);
        oa.transform.localScale = new Vector3(dx-dd*0.5f,dy+dd*0.5f,nz);

        GameObject ob = Instantiate(prefabCubo);
        ob.transform.localScale = new Vector3(dx+dd*0.5f,ny-dy-dd*0.5f,nz);

        GameObject oc = Instantiate(prefabCubo);
        oc.transform.localScale = new Vector3(nx-dx-dd*0.5f,ny-dy+dd*0.5f,nz);

        GameObject od = Instantiate(prefabCubo);
        od.transform.localScale = new Vector3(nx-dx+dd*0.5f,dy-dd*0.5f,nz);

        oa.transform.position = o.transform.position + new Vector3(-oa.transform.localScale.x*0.5f-dd*0.5f,-oa.transform.localScale.y*0.5f+dd*0.5f,0.0f);
        ob.transform.position = o.transform.position + new Vector3(-ob.transform.localScale.x*0.5f+dd*0.5f, ob.transform.localScale.y*0.5f+dd*0.5f,0.0f);
        oc.transform.position = o.transform.position + new Vector3( oc.transform.localScale.x*0.5f+dd*0.5f, oc.transform.localScale.y*0.5f-dd*0.5f,0.0f);
        od.transform.position = o.transform.position + new Vector3( od.transform.localScale.x*0.5f-dd*0.5f,-od.transform.localScale.y*0.5f-dd*0.5f,0.0f);

        oa.transform.parent = o.transform;
        ob.transform.parent = o.transform;
        oc.transform.parent = o.transform;
        od.transform.parent = o.transform;

        return o;
    }
    void CreateBonus(GameObject t){
        /*
        for (float i = 0; i<t.transform.localScale.z; i+=offset.z*0.2f){
            GameObject b = Instantiate(prefabBonus);
            float sinOffset= Mathf.Sin(i*sinFreqMult)*sinAmp; 
            b.transform.position = new Vector3(t.transform.position.x+sinOffset,t.transform.position.y+t.transform.localScale.y*0.5f+transform.localScale.y,t.transform.position.z-t.transform.localScale.z*0.5f+i);
        }
        */
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
