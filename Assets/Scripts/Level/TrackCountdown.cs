using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackCountdown : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] lights;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private CarMovement player;
    [SerializeField] private EnemyCarAI[] enemies;
    void Start()
    {
        player.BlockMovement(true);
        countdownText.text = string.Empty;

        SetLightToStart();

        StartCoroutine(Countdown());
    }
    private void SetLightToStart()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].materials[0].color = Color.black;
            lights[i].materials[0].SetColor("_EmissionColor", Color.black);
        }
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);

        lights[0].materials[0].color = Color.red;
        lights[0].materials[0].SetColor("_EmissionColor", Color.red);
        countdownText.text = "3";

        yield return new WaitForSeconds(1);

        lights[1].materials[0].color = Color.red;
        lights[1].materials[0].SetColor("_EmissionColor", Color.red);
        countdownText.text = "2";

        yield return new WaitForSeconds(1);

        lights[2].materials[0].color = Color.red;
        lights[2].materials[0].SetColor("_EmissionColor", Color.red);
        countdownText.text = "1";

        yield return new WaitForSeconds(1);

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].materials[0].color = Color.green;
            lights[i].materials[0].SetColor("_EmissionColor", Color.green);
        }
        countdownText.text = "GO!";
        player.BlockMovement(false);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].StartMovement();
        }

        yield return new WaitForSeconds(1);

        countdownText.text = string.Empty;
    }
}
