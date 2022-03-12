using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.U2D;

namespace McgillTeam3
{
    public class CaveGen : MonoBehaviour
    {
        const float NOISE_AMPLITUDE = 2f; // Controls how big the stalagmites and stalactites are
        const int CHUNK_WIDTH = 8; // Controls the width of the cave chunks
        const float MIN_RADIUS = 0.5f; // How broad the tunnel must be at minimum
        const float MAX_RADIUS = 1.5f; // How broad the tunnel must be at maximum

        float scrollPos = 0f;
        float speed = 0.1f;

        [SerializeField] SpriteShapeController[] topChunks;
        [SerializeField] SpriteShapeController[] bottomChunks;
        
        float[] slope = new float[2]; // Stores two y-values that form the slope of a chunk;
        float[] caveRadius = new float[] {1f, 1f};
        float prevTop = 0f; // The ending y-value of the last top chunk generated
        float prevBottom = 0f; // The ending y-value of the last bottom chunk generated

        // Start is called before the first frame update
        void Start()
        {
            // Generates 6 chunks with no slope which comprise the starting area
            foreach (SpriteShapeController chunk in topChunks) prevTop = GenerateTop(chunk.spline, prevTop, new Vector2(), new Vector2(caveRadius[0], caveRadius[1]));
            foreach (SpriteShapeController chunk in bottomChunks) prevBottom = GenerateBottom(chunk.spline, prevBottom, new Vector2(), new Vector2(caveRadius[0], caveRadius[1]));
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Moves the chunks in a treadmill-fashion in order to create a constantly-scrolling, procedural level

            // Moves the back chunk to the front, and forces it to generate a new chunk in order to keep the level scrolling infinitely
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

                caveRadius[0] = caveRadius[1];
                caveRadius[1] = UnityEngine.Random.Range(MIN_RADIUS, MAX_RADIUS);

                prevTop = GenerateTop(topChunks[5].spline, prevTop, new Vector2(slope[0], slope[1]), new Vector2(caveRadius[0], caveRadius[1]));
                prevBottom = GenerateBottom(bottomChunks[5].spline, prevBottom, new Vector2(slope[0], slope[1]), new Vector2(caveRadius[0], caveRadius[1]));

            }

            // Moves every chunk to the correct position based on the scrolling distance
            for (int i = 0; i < 6; i++){
                topChunks[i].gameObject.transform.position = new Vector3(scrollPos - (CHUNK_WIDTH * (2 - i)), 0, 0);
                bottomChunks[i].gameObject.transform.position = new Vector3(scrollPos - (CHUNK_WIDTH * (2 - i)), 0, 0);
            }

            // Scrolls left
            scrollPos -= speed;
        }

        float lerpF(float a, float b, float t){
            // Interpolates between two floats by t
            return (a * (1 - t)) + (b * t);
        }

         float GenerateTop(Spline topSpline, float startHeight, Vector2 slope, Vector2 radius){
            // Modifies topSpline in-place in order to generate a new chunk.
            // The startHeight should generally be the endHeight of the last chunk, so the two are flush.
            // The slope is two y-coordinates. This represents a line from the start of the chunk at the first coordinate, ending at the end of the chunk at the second coordinate.
            // The cave always generates near the slope, leaving enough room for the player to navigate.
            
            // Sets the top-left three corners
            topSpline.SetPosition(0, new Vector3(-CHUNK_WIDTH/2, 20, 0));
            topSpline.SetPosition(1, new Vector3(-CHUNK_WIDTH/2, startHeight, 0));
            topSpline.SetPosition(10, new Vector3(CHUNK_WIDTH/2, 20, 0));

            // Generates random values between 0.1 and 0.9 to be used to generate the x-coordinate of each point
            float[] xCoords = new float[7];
            for(int i = 0; i < 7; i++) xCoords[i] = UnityEngine.Random.Range(0.1f, 0.9f);
            // foreach(float f in xCoords) print(f);
            Array.Sort(xCoords);
            
            // Assigns each random x-value a random y-value that is no more than NOISE_AMPLITUDE away from the slope
            for (int i = 0; i < 7; i++) {
                topSpline.SetPosition(i + 2, new Vector3(xCoords[i] * CHUNK_WIDTH - CHUNK_WIDTH/2, 
                UnityEngine.Random.Range(lerpF(slope[0], slope[1], xCoords[i]) + lerpF(radius[0], radius[1], xCoords[i]), lerpF(slope[0], slope[1], xCoords[i]) + lerpF(radius[0], radius[1], xCoords[i]) + NOISE_AMPLITUDE), 
                0));
            }

            // Generates the last point, always fully on the right so it is flush with the next chunk
            float endHeight = UnityEngine.Random.Range(slope[1] + radius[1], slope[1] + radius[1] + NOISE_AMPLITUDE);
            topSpline.SetPosition(9, new Vector3(CHUNK_WIDTH/2, endHeight, 0));
            // The end height is used by the next chunk in the sequence so that the two chunks are flush
            return endHeight;
        }

        float GenerateBottom(Spline bottomSpline, float startHeight, Vector2 slope, Vector2 radius){
            // Most of this is sloppily copied from GenerateTop. They're different functions because... because it was past midnight when I wrote this.

            // Sets the bottom-left three corners
            bottomSpline.SetPosition(0, new Vector3(-CHUNK_WIDTH/2, -20, 0));
            bottomSpline.SetPosition(1, new Vector3(-CHUNK_WIDTH/2, startHeight, 0));
            bottomSpline.SetPosition(10, new Vector3(CHUNK_WIDTH/2, -20, 0));

            // Generates random values between 0.1 and 0.9 to be used to generate the x-coordinate of each point
            float[] xCoords = new float[7];
            for(int i = 0; i < 7; i++) xCoords[i] = UnityEngine.Random.Range(0.1f, 0.9f);

            // Separates the x-values a bit so there aren't any issues
            Array.Sort(xCoords);
            for(int i = 0; i < 6; i++) if (xCoords[i + 1] - xCoords[i] < 0.1f){
                xCoords[i] -= 0.05f;
                xCoords[i + 1] += 0.05f;
            }
            
            // Assigns each random x-value a random y-value that is no more than NOISE_AMPLITUDE away from the slope
            for (int i = 0; i < 7; i++) {
                bottomSpline.SetPosition(i + 2, new Vector3(xCoords[i] * CHUNK_WIDTH - CHUNK_WIDTH/2, 
                UnityEngine.Random.Range(lerpF(slope[0], slope[1], xCoords[i]) - lerpF(radius[0], radius[1], xCoords[i]) - NOISE_AMPLITUDE, lerpF(slope[0], slope[1], xCoords[i]) - lerpF(radius[0], radius[1], xCoords[i])), 
                0));
            }

            // Generates the last point, always fully on the right so it is flush with the next chunk
            float endHeight = UnityEngine.Random.Range(slope[1] - radius[1] - NOISE_AMPLITUDE, slope[1] - radius[1]);
            bottomSpline.SetPosition(9, new Vector3(CHUNK_WIDTH/2, endHeight, 0));
            // The end height is used by the next chunk in the sequence so that the two chunks are flush
            return endHeight;
        }
    }
}
