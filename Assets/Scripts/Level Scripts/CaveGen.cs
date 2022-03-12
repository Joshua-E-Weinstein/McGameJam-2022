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

        float scrollPos = 0f;
        float speed = 0.1f;

        [SerializeField] SpriteShapeController[] topChunks;
        [SerializeField] SpriteShapeController[] bottomChunks;
        Spline[] topSplines;
        Spline[] bottomSplines;
        float[] slope = new float[2];
        float prevTop = 0f;
        float prevBottom = 0f;

        // Start is called before the first frame update
        void Start()
        {
            foreach (SpriteShapeController chunk in topChunks) prevTop = GenerateTop(chunk.spline, prevTop, new Vector2());
            foreach (SpriteShapeController chunk in bottomChunks) prevBottom = GenerateBottom(chunk.spline, prevBottom, new Vector2());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (scrollPos < -CHUNK_WIDTH){
                scrollPos += CHUNK_WIDTH;

                SpriteShapeController temp = topChunks[0];
                for (int i = 1; i < 6; i++){
                    topChunks[i - 1] = topChunks[i];
                }
                topChunks[5] = temp;

                temp = bottomChunks[0];
                for (int i = 1; i < 6; i++){
                    bottomChunks[i - 1] = bottomChunks[i]; 
                }
                bottomChunks[5] = temp;

                slope[0] = slope[1];
                slope[1] = UnityEngine.Random.Range(-2.5f, 2.5f);

                prevTop = GenerateTop(topChunks[5].spline, prevTop, new Vector2(slope[0], slope[1]));
                prevBottom = GenerateBottom(bottomChunks[5].spline, prevBottom, new Vector2(slope[0], slope[1]));

            }

            for (int i = 0; i < 6; i++){
                topChunks[i].gameObject.transform.position = new Vector3(scrollPos - (CHUNK_WIDTH * (3 - i)), 0, 0);
                bottomChunks[i].gameObject.transform.position = new Vector3(scrollPos - (CHUNK_WIDTH * (3 - i)), 0, 0);
            }

            scrollPos -= speed;
        }

        float lerpF(float a, float b, float t){
            return (a * (1 - t)) + (b * t);
        }

         float GenerateTop(Spline topSpline, float startHeight, Vector2 slope){

            topSpline.SetPosition(0, new Vector3(-CHUNK_WIDTH/2, 20, 0));
            topSpline.SetPosition(1, new Vector3(-CHUNK_WIDTH/2, startHeight, 0));
            topSpline.SetPosition(10, new Vector3(CHUNK_WIDTH/2, 20, 0));

            float[] xCoords = new float[7];
            for(int i = 0; i < 7; i++) xCoords[i] = UnityEngine.Random.Range(0.1f, 0.9f);
            foreach(float f in xCoords) print(f);
            Array.Sort(xCoords);
            

            for (int i = 0; i < 7; i++) {
                topSpline.SetPosition(i + 2, new Vector3(xCoords[i] * CHUNK_WIDTH - CHUNK_WIDTH/2, 
                UnityEngine.Random.Range(lerpF(slope[0], slope[1], xCoords[i]) + 1f, lerpF(slope[0], slope[1], xCoords[i]) + 1f + NOISE_AMPLITUDE), 
                0));
            }
            float endHeight = UnityEngine.Random.Range(slope[1] + 1f, slope[1] + 1f + NOISE_AMPLITUDE);
            topSpline.SetPosition(9, new Vector3(CHUNK_WIDTH/2, endHeight, 0));
            return endHeight;
        }

        float GenerateBottom(Spline bottomSpline, float startHeight, Vector2 slope){

            bottomSpline.SetPosition(0, new Vector3(-CHUNK_WIDTH/2, -20, 0));
            bottomSpline.SetPosition(1, new Vector3(-CHUNK_WIDTH/2, startHeight, 0));
            bottomSpline.SetPosition(10, new Vector3(CHUNK_WIDTH/2, -20, 0));

            float[] xCoords = new float[7];
            for(int i = 0; i < 7; i++) xCoords[i] = UnityEngine.Random.Range(0.1f, 0.9f);
            foreach(float f in xCoords) print(f);
            Array.Sort(xCoords);
            

            for (int i = 0; i < 7; i++) {
                bottomSpline.SetPosition(i + 2, new Vector3(xCoords[i] * CHUNK_WIDTH - CHUNK_WIDTH/2, 
                UnityEngine.Random.Range(lerpF(slope[0], slope[1], xCoords[i]) - 1f - NOISE_AMPLITUDE, lerpF(slope[0], slope[1], xCoords[i]) - 1f), 
                0));
            }
            float endHeight = UnityEngine.Random.Range(slope[1] - 1f - NOISE_AMPLITUDE, slope[1] - 1f);
            bottomSpline.SetPosition(9, new Vector3(CHUNK_WIDTH/2, endHeight, 0));
            return endHeight;
        }
    }
}
