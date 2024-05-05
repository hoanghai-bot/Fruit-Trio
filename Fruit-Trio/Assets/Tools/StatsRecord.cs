using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Profiling;
using UnityEngine;

public class StatsRecord : MonoBehaviour
{
    public TMP_Text statsText;
    public GameObject main;
    ProfilerRecorder drawCallsRecorder, verticesRecorder, mainThreadTimeRecorder, systemMemoryRecorder;
    private void Start()
    {

        StartCoroutine(Display());

    }

    void OnDisable()
    {
        drawCallsRecorder.Dispose();
        verticesRecorder.Dispose();
        mainThreadTimeRecorder.Dispose();
        systemMemoryRecorder.Dispose();
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            var sb = new StringBuilder();
            float count = (1 / Time.deltaTime);

            sb.Append($"FPS: {Math.Round(count, 1)}     ");

            if (drawCallsRecorder.Valid)
                sb.Append($"Batches: {drawCallsRecorder.LastValue}    ");
            if (verticesRecorder.Valid)
            {
                sb.Append($"Triangles: {verticesRecorder.LastValue}     ");
            }
            if (mainThreadTimeRecorder.Valid)
            {
                sb.Append($"CPU: Main {GetRecorderFrameAverage(mainThreadTimeRecorder) * (1e-6f):F1} ms     ");
            }
            if (systemMemoryRecorder.Valid)
            {
                sb.AppendLine($"Used Memory: {systemMemoryRecorder.LastValue / (1024 * 1024)} MB        ");
            }

            statsText.text = sb.ToString();
        }
    }

    static double GetRecorderFrameAverage(ProfilerRecorder recorder)
    {
        var samplesCount = recorder.Capacity;
        if (samplesCount == 0)
            return 0;

        double r = 0;


        var samples = new List<ProfilerRecorderSample>(samplesCount);
        recorder.CopyTo(samples);
        for (var i = 0; i < samplesCount; ++i)
            r += samples[i].Value;
        r /= samplesCount;

        return r;
    }
}
