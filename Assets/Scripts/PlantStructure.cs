using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantStructure
{
    public float height;
    public float budHeightRatio;

    public List<LeafPoint> leafPoints = new();
    public List<Branch> branches = new();

    public class LeafPoint
    {
        public float heightRatio;
    }

    public class Branch
    {
        //Where on the main spline this branch sprouts
        public float heightRatio; 

        //Rotation in degrees
        public float angle;     
        
        //Branch length
        public float length;      

    }
}