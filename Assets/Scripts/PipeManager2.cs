using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager2 : MonoBehaviour
{
    public List<Pipe> allPipes;
    public Pipe sourcePipe;
    private bool Initialized;
    public GameplayCanvasController gameplayCanvas;
    public bool GameWon;
    // Start is called before the first frame update
    public void Init()
    {
        Debug.Log("Init");
        foreach(Tile pipe in FindObjectsOfType<Tile>())
        {
            if(pipe is Pipe)
            {
                Pipe pipeInst = pipe as Pipe;
                if(pipeInst.sourcePipe == true)
                {
                    sourcePipe = pipeInst;
                }
                allPipes.Add(pipeInst);
            }
            
        }
        Initialized = true;
    }

    public Pipe GetNearbyTile(Vector3 position)
    {
        foreach (Pipe pipe in allPipes)
        {
            if (pipe.transform.position == position)
            {
                return pipe;
            }
        }
        return null;

    }
    //public Pipe GetNearbyPipe(Mapping mapping,Vector3 sourcePipe)
    //{
    //    Pipe nearbyPipe = null;
    //    if (mapping.axis == "x")
    //    {
    //        nearbyPipe = GetNearbyTile(sourcePipe + Vector3.right * mapping.id);
    //    }
    //    if (mapping.axis == "y")
    //    {
    //        nearbyPipe = GetNearbyTile(sourcePipe + Vector3.up * mapping.id);
    //    }
    //    return nearbyPipe;
    //}


    void Start()
    {
      //  Init();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    foreach (Pipe pipe in allPipes)
        //    {

        //        pipe.nearbyPipes.Clear();
        //        pipe.HideAllEffects();
        //    }
        //    sourcePipe.ConnectNext();
        //}


        if (Initialized && Time.frameCount % 20 == 0)
        {
            foreach (Pipe pipe in allPipes)
            {

                pipe.nearbyPipes.Clear();
                pipe.HideAllEffects();
            }
            sourcePipe.ConnectNext();
            Debug.Log("Connect next");
        }

    }

 
}
