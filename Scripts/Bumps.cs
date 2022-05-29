using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumps : MonoBehaviour
{
    public static float[,] generateBumpsNoise(NoiseData noiseData, BumpsNoiseData bumpsNoiseData)
    {
        AnimationCurve noiseCurve = noiseData.useNoiseCurve ? noiseData.noiseCurve : null;
        float amplitude = noiseData.amplitude;
        float frequency = bumpsNoiseData.frequency;
        Vector2 offset = noiseData.offset;
        int octaves = bumpsNoiseData.octaves;
        Vector2 octaveRandomOffset = bumpsNoiseData.octaveRandomOffset;
        float xPersistance = bumpsNoiseData.xPersistance;
        float yPersistance = bumpsNoiseData.yPersistance;

        //float lowestNoiseHeight = 0f;
        //float highestNoiseHeight = 0f;

        int xSize = noiseData.mapSize.x;
        int ySize = noiseData.mapSize.y;

        float[,] bumpsNoise = new float[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                float octaveAmplitude = amplitude;
                //float octaveFrequency = frequency;
                float xOctave = x;
                float yOctave = y;

                for (int i = 0; i < octaves; i++)
                {
                    xOctave += Random.Range(octaveRandomOffset.x, octaveRandomOffset.y);
                    yOctave += Random.Range(octaveRandomOffset.x, octaveRandomOffset.y);
                    octaveAmplitude *= Random.Range(xPersistance, yPersistance);
                }

                float noiseHeight = (Mathf.Sin(5 * (xOctave + offset.x) * frequency) * Mathf.Cos(5 * (yOctave + offset.y) * frequency)) * octaveAmplitude;

                if (noiseCurve != null)
                    bumpsNoise[x, y] = noiseCurve.Evaluate(noiseHeight);
                else
                    bumpsNoise[x, y] = noiseHeight;

                //if (noiseHeight > highestNoiseHeight)
                //{
                //    highestNoiseHeight = noiseHeight;
                //}
                //else if (noiseHeight < lowestNoiseHeight)
                //{
                //    lowestNoiseHeight = noiseHeight;
                //}
            }
        }

        //for (int x = 0; x < xSize; x++)
        //{
        //    for (int y = 0; y < ySize; y++)
        //    {
        //        sineNoise[x, y] = Mathf.Sin(Mathf.InverseLerp(lowestNoiseHeight, highestNoiseHeight, sineNoise[x, y])) * amplitude;
        //    }
        //}

        return bumpsNoise;
    }
}