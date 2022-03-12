using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.U2D;

namespace McgillTeam3
{
    public class CaveGen : MonoBehaviour
    {
        const float NOISE_AMPLITUDE = 2f;
        const int CHUNK_WIDTH = 8;

        [SerializeField] SpriteShapeController topChunk0;
        [SerializeField] SpriteShapeController topChunk1;
        [SerializeField] SpriteShapeController topChunk2;

        [SerializeField] SpriteShapeController bottomChunk0;
        [SerializeField] SpriteShapeController bottomChunk1;
        [SerializeField] SpriteShapeController bottomChunk2;

        Spline[] topSplines;
        Spline[] bottomSplines;


        // Start is called before the first frame update
        void Start()
        {
            topSplines = new Spline[] {topChunk0.spline, topChunk1.spline, topChunk2.spline};
            bottomSplines = new Spline[] {bottomChunk0.spline, bottomChunk1.spline, bottomChunk2.spline};
            GenerateTop(topSplines[2], GenerateTop(topSplines[1], GenerateTop(topSplines[0], -1f, new Vector2(0f, 1f)), new Vector2(0f, 1f)), new Vector2(0f, 1f));
            GenerateBottom(bottomSplines[2], GenerateBottom(bottomSplines[1], GenerateBottom(bottomSplines[0], 1f, new Vector2(0f, 1f)), new Vector2(0f, 1f)), new Vector2(0f, 1f));
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
        }

        float lerpF(float a, float b, float t){
            return (a * (1 - t)) + (b * t);
        }

         float GenerateTop(Spline topSpline, float startHeight, Vector2 slope){

            topSpline.SetPosition(0, new Vector3(-CHUNK_WIDTH/2, 5, 0));
            topSpline.SetPosition(1, new Vector3(-CHUNK_WIDTH/2, startHeight, 0));
            topSpline.SetPosition(10, new Vector3(CHUNK_WIDTH/2, 5, 0));

            float[] xCoords = new float[7];
            for(int i = 0; i < 7; i++) xCoords[i] = UnityEngine.Random.Range(0.1f, 0.9f);
            foreach(float f in xCoords) print(f);
            Array.Sort(xCoords);
            

            for (int i = 0; i < 7; i++) {
                topSpline.SetPosition(i + 2, new Vector3(xCoords[i] * CHUNK_WIDTH - CHUNK_WIDTH/2, 
                UnityEngine.Random.Range(lerpF(slope[0], slope[1], xCoords[i]) + 0.5f, lerpF(slope[0], slope[1], xCoords[i]) + 0.5f + NOISE_AMPLITUDE), 
                0));
            }
            float endHeight = UnityEngine.Random.Range(slope[1] + 0.5f, slope[1] + 0.5f + NOISE_AMPLITUDE);
            topSpline.SetPosition(9, new Vector3(CHUNK_WIDTH/2, endHeight, 0));
            return endHeight;
        }

        float GenerateBottom(Spline bottomSpline, float startHeight, Vector2 slope){

            bottomSpline.SetPosition(0, new Vector3(-CHUNK_WIDTH/2, -5, 0));
            bottomSpline.SetPosition(1, new Vector3(-CHUNK_WIDTH/2, startHeight, 0));
            bottomSpline.SetPosition(10, new Vector3(CHUNK_WIDTH/2, -5, 0));

            float[] xCoords = new float[7];
            for(int i = 0; i < 7; i++) xCoords[i] = UnityEngine.Random.Range(0.1f, 0.9f);
            foreach(float f in xCoords) print(f);
            Array.Sort(xCoords);
            

            for (int i = 0; i < 7; i++) {
                bottomSpline.SetPosition(i + 2, new Vector3(xCoords[i] * CHUNK_WIDTH - CHUNK_WIDTH/2, 
                UnityEngine.Random.Range(lerpF(slope[0], slope[1], xCoords[i]) - 0.5f - NOISE_AMPLITUDE, lerpF(slope[0], slope[1], xCoords[i]) - 0.5f), 
                0));
            }
            float endHeight = UnityEngine.Random.Range(slope[1] - 0.5f - NOISE_AMPLITUDE, slope[1] - 0.5f);
            bottomSpline.SetPosition(9, new Vector3(CHUNK_WIDTH/2, endHeight, 0));
            return endHeight;
        }
    }
}
