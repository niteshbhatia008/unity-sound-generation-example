using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveformGenerator : MonoBehaviour
{

    AudioSource audioSource;

    System.Random random;

    float[] samples = new float[2048];

    public enum WaveformType
    {
        Sine,
        Triangle,
        SawTooth,
        Square,
        WhiteNoise,
    }

    public WaveformType waveformType = WaveformType.WhiteNoise;

    [Range(0, 2)]
    public float multiplier = 1;

    public float shift = 0;

    [Range(1, 2048)]
    public int samplePeriod = 256;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        random = new System.Random();
    }

    void Update()
    {
        var height = 2 * Camera.main.orthographicSize;
        var width = Camera.main.aspect * height;

        Debug.DrawLine(transform.position, transform.position + Vector3.right * width, Color.gray);

        audioSource.GetOutputData(samples, 0);

        for (int i = 1; i < samples.Length; i++)
        {
            Debug.DrawLine(
                transform.position + Vector3.right * (i - 1) / samples.Length * width + Vector3.up * samples[i - 1] * height,
                transform.position + Vector3.right * i / samples.Length * width + Vector3.up * samples[i] * height
            );
        }


    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            switch (waveformType)
            {
                case WaveformType.WhiteNoise:
                    {
                        data[i] = (float)(random.NextDouble() * multiplier - multiplier / 2);
                        break;
                    }
                case WaveformType.Square:
                    {
                        data[i] = (((i + shift) % samplePeriod * 2) < samplePeriod ? -1 : 1) * multiplier / 2;
                        break;
                    }
                case WaveformType.Sine:
                    {
                        data[i] = Mathf.Sin(((i + shift) % samplePeriod) / samplePeriod * 2 * Mathf.PI) * multiplier / 2;
                        break;
                    }
                case WaveformType.SawTooth:
                    {
                        data[i] = ((i + shift) % samplePeriod) / samplePeriod * multiplier - multiplier / 2;
                        break;
                    }
                case WaveformType.Triangle:
                    {
                        var percent = ((i + shift) % samplePeriod) / samplePeriod;
                        if (percent < 0.5f)
                        {
                            data[i] = percent * 2 * multiplier - multiplier / 2;
                        }
                        else
                        {
                            data[i] = (1 - percent) * 2 * multiplier - multiplier / 2;
                        }
                        break;
                    }
            }
        }
    }
}
