using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Manager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText,gameOverScoreText;
    [SerializeField]
    float playedTime;
    [SerializeField]
    gameSettings gameSettings;
    public float score;
    public Volume VolumeFX;
    [ColorUsage(true, true)]
    public Color redTint, blueTint, greenTint, purpleTint;
    public Color[] colors;
    [ColorUsage(true, true)]
    public Color currentColor;
    [ColorUsage(true, true)]
    public Color nextColor;
    [ColorUsage(true, true)]
    public Color appliedColor;
    ColorParameter asd;
    public float rgbSpeed;
    public float colorChangingTime;
    public PipeSpawner pipeSpawnerObject;
    public PlayerStats playerStats;
    public Collisions collisions;
    public GameObject gameOverMenu, gameplayButtons;
    public bool waitForit = true;
    [SerializeField]
    float timePassed, slowingSpeed;
    void Start()
    {
        colors[0] = redTint;
        colors[1] = blueTint;
        colors[2] = greenTint;
        colors[3] = purpleTint;
        currentColor = purpleTint;
        ChooseNextColor();
        StartCoroutine("RgbPipes");
    }
    void Update()
    {
        score += (2 * gameSettings.gameSpeed * gameSettings.gameDifficulty * playerStats.speedMultiplierAmount * playerStats.speedMultiplierAmount * Time.deltaTime);
        scoreText.text = score.ToString("0");

        if(collisions.gameOver)
        {
            StartCoroutine(SlowDown());
            if(!waitForit)
            {
                gameplayButtons.SetActive(false);
                gameOverMenu.SetActive(true);
                gameOverScoreText.text = scoreText.text;
                StartCoroutine(fadeInGameOver());
            }
        }
    }

    IEnumerator SlowDown()
    {
        gameSettings.gameSpeed = Mathf.Lerp(gameSettings.gameSpeed, 0, Time.deltaTime * slowingSpeed);
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.deltaTime * slowingSpeed);
        timePassed += Time.unscaledDeltaTime;
        if (timePassed > 1)
        {
            gameSettings.gameSpeed = 0;
            waitForit = false;
        }

        yield return new WaitForEndOfFrame();

    }


    IEnumerator fadeInGameOver()
    {
        gameOverMenu.GetComponent<CanvasGroup>().alpha += 0.01f;

        if (gameOverMenu.GetComponent<CanvasGroup>().alpha > 0.99f)
            yield return null;

        yield return new WaitForSeconds(0.01f);

    }
    IEnumerator RgbPipes()
    {
        for (; ; )
        {
            Bloom bloom;
            if (VolumeFX.profile.TryGet(out bloom))
            {
                while (!appliedColor.Compare(nextColor))
                {
                    appliedColor = Color.Lerp(currentColor, nextColor, colorChangingTime);

                    bloom.tint.Override(appliedColor);
                    colorChangingTime += Time.deltaTime / rgbSpeed;
                    yield return null;
                }
            }
            if (appliedColor.Compare(nextColor))
            {
                yield return new WaitForSeconds(10);
                ChooseNextColor();
                colorChangingTime = 0;
                currentColor = appliedColor;
            }
        }
    }
    void ChooseNextColor()
    {
        do
        {
            nextColor = colors[Random.Range(0, colors.Length)];
        } while (nextColor == appliedColor);
    }

    void changeNeonColor()
    {
        for (int i = 0; i < pipeSpawnerObject.GetComponent<PipeSpawner>().createdPipes.Count; i++)
        {
            //    pipeSpawnerObject.GetComponent<PipeSpawner>().createdPipes[i].transform.GetChild(0).GetChild(0).GetComponent<Material>().SetColor("NeonColor", appliedColor);

        }

    }


}
