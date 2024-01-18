using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Tile
{
    public bool ContainsWater;
    public List<Mapping> currentMappings;
    public PipeManager2 pipeManager;
    public List<Pipe> nearbyPipes;
    public bool IsEndPipe;
    public bool sourcePipe;
    public bool debug;
   

    private void Awake()
    {
        pipeManager = FindObjectOfType<PipeManager2>();
    }

    [ContextMenu("ConnectNext")]
    public void ConnectNext()
    {
        if (pipeManager.GameWon) return;
      //  Debug.Log("Connect from " + transform.name);
        nearbyPipes.Clear();
        foreach (Mapping mapping in currentMappings)
        {
            Pipe nearbyPipe = GetNearbyPipe(mapping);
            if (nearbyPipe != null)
            {
                foreach (Mapping mapping1 in nearbyPipe.currentMappings)
                {
                    if (mapping1.axis == mapping.axis && mapping1.id == -mapping.id)
                    {
                        nearbyPipe.ContainsWater = true;
                        if(nearbyPipe.nearbyPipes.Count > 0 && nearbyPipe.nearbyPipes.Contains(this))
                        {

                        }
                        else
                        {
                            nearbyPipes.Add(nearbyPipe);
                        }

                    }
                }
            }
        }
        Debug.Log(nearbyPipes.Count);
        if (nearbyPipes.Count > 0)
        {
            Pipe nearbyPipe = nearbyPipes[nearbyPipes.Count - 1];
            if (nearbyPipe.IsEndPipe)
            {
                Debug.Log("Game won!");
                pipeManager.gameplayCanvas.ShowGameWon();
                pipeManager.GameWon = true;
                return;
            }
            nearbyPipe.ConnectNext();
        }
        if (ContainsWater)
        {
            foreach (Mapping mapping in currentMappings)
            {
                Pipe nearbyPipe = GetNearbyPipe(mapping);
                bool hasConnection = false;
                if (nearbyPipe != null)
                {

                    foreach (Mapping mapping1 in nearbyPipe.currentMappings)
                    {
                        if (mapping1.axis == mapping.axis && mapping1.id == -mapping.id)
                        {
                            hasConnection = true;
                        }
                    }

                }
                else
                {
                    hasConnection = false;
                }
                mapping.effect.gameObject.SetActive(!hasConnection);
            }

        }
        else
        {
            foreach (Mapping mapping in currentMappings)
            {
                mapping.effect.gameObject.SetActive(false);
            }
        }
        
     
    }

    
    public void HideAllEffects()
    {
        foreach (Mapping mapping in currentMappings)
        {
            mapping.effect.SetActive(false);
        }
        }

  
    public Pipe GetNearbyPipe(Mapping mapping)
    {
        Pipe nearbyPipe = null;
        if (mapping.axis == "x")
        {
            nearbyPipe = pipeManager.GetNearbyTile(transform.position + Vector3.right * mapping.id);
        }
        if (mapping.axis == "y")
        {
            nearbyPipe = pipeManager.GetNearbyTile(transform.position + Vector3.up * mapping.id);
        }
        return nearbyPipe;
    }
    
}
[Serializable]
public class Mapping
{
    public string axis;
    public int id;
    public GameObject effect;
}
