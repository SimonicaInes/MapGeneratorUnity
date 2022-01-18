using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoiseGenerator 
{
    const int TILE_SIZE = 32;




    //***************  noise generation variables  ***************


    // The origin of the sampled area in the plane.

    private float xOrg;
    private float yOrg;


    private Texture2D mapResult;
    private Texture2D noiseTex;



    public  Texture2D GeneratePerlinNoiseMap(int mapWidth, int mapHeight, float binThreshold, float scale)
    {
        float randomRangeMIN = -2000;
        float randomRangeMAX = 1000;




        //Create texture for the perlin noise map
        InitializePerlinNoiseMap(mapWidth, mapHeight);

        //Compute initial perlin noise map
        ComputeInitialNoise(randomRangeMIN, randomRangeMAX, scale);

        //Binarize generated perlin noise map and overwrite result
        BinarizeMap(binThreshold);
        //Draw frame by given width over the new map


        PixelateNoise(mapWidth, mapHeight);

        //normal water
        //DrawBorders(shallowWaterBorderWidth, new Color(0,0,0));
        //deep water
        //DrawBorders(deepWaterBorderWidth, new Color(0, 0, 1));

        return mapResult;
    }






    private void InitializePerlinNoiseMap(int mapWidth, int mapHeight)
    {

        // Set up the texture and a Color array to hold pixels during processing.

        noiseTex = new Texture2D(TILE_SIZE * mapWidth, TILE_SIZE * mapHeight);

    }

    private void ComputeInitialNoise(float randomRangeMIN, float randomRangeMAX, float scale )
    {
        // For each pixel in the texture...

        xOrg = Random.Range(randomRangeMIN, randomRangeMAX);

        yOrg = Random.Range(randomRangeMIN, randomRangeMAX);

        for (int i = 0; i < noiseTex.height; i++)
        {
            for (int j = 0; j < noiseTex.width; j++)
            {
                float xCoord = (float)i / noiseTex.width * scale + xOrg;
                float yCoord = (float)j / noiseTex.height * scale + yOrg;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                noiseTex.SetPixel(i, j, new Color(sample, sample, sample));
            }
        }


        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.Apply();
    }

    private void BinarizeMap(float threshold)
    {
        //Color t = new Color((float)threshold, (float)threshold, (float)threshold);
        for (int i = 0; i < noiseTex.height; i++)
        {
            for (int j = 0; j < noiseTex.width; j++)
            {
                if (noiseTex.GetPixel(i, j).r <= threshold)
                {
                    noiseTex.SetPixel(i, j, new Color(0, 0, 0));
                    //Debug.Log(noiseTex.GetPixel(i, j).grayscale);
                }
                else
                {
                    noiseTex.SetPixel(i, j, new Color(255, 255, 255));
                }
            }
        }

        noiseTex.Apply();

    }

    void DrawBorders(int thickness, Color tileColor)
    {


        for (int i = 0; i < mapResult.height; i++)
        {
            for (int j = 0; j < mapResult.width; j++)
            {

                if ((i <= thickness) ||
                    (i >= thickness  && (j <= thickness || j >= mapResult.width - thickness - 1)) ||
                    (i >= mapResult.height - thickness - 1)
                    )
                {
                    mapResult.SetPixel(i, j, tileColor);
                }
            
            }

        }
        mapResult.Apply();
    }

    private void PixelateNoise(int mapWidth, int mapHeight)
    {
        mapResult = new Texture2D(mapWidth, mapHeight);
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                mapResult.SetPixel(i, j, new Color(1, 0, 0));
            }
               
        }

        mapResult.Apply();

        int numberOfWhitePixels;
        for (int i = 0; i < mapWidth; i++)
        {
           for (int j = 0; j < mapHeight; j++)
            {
                numberOfWhitePixels = 0;
                
                for (int k = i * TILE_SIZE; k < (i + 1) * TILE_SIZE; k++)
                {
                    for (int l = j * TILE_SIZE; l < (j + 1) * TILE_SIZE; l++)
                    {
                        if(noiseTex.GetPixel(k, l).r == 1)
                        {
                            numberOfWhitePixels++;
                        }
                        
                    }
                }
                
                if(numberOfWhitePixels > TILE_SIZE * TILE_SIZE / 2)
                {
                    //more land pixels => turn tile white
                    mapResult.SetPixel(i, j, new Color(1, 1, 1));
                }
                else
                {
                    mapResult.SetPixel(i, j, new Color(0, 0, 0));
                }
            }
        }
        
        mapResult.Apply();
       
    }
}

