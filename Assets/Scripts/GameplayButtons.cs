using System.Collections;
using TMPro;
using UnityEngine;

public class GameplayButtons : MonoBehaviour
{
    public TextMeshProUGUI scoreText, scoreTextInPause, invulCounterText, speedCounterText;
    public UnityEngine.UI.Text invulAmountText, speedAmountText;
    public PlayerStats playerStats;
    public GameObject player;
    public bool isCountingInvul, isCountingSpeed;
    public GameObject speedParticle;
    public void PauseGame()
    {
        scoreTextInPause.text = scoreText.text;
        Time.timeScale = 0;
    }

    private void Update()
    {
        speedAmountText.text = playerStats.speedMultiplier.ToString();
        invulAmountText.text = playerStats.Invulnerability.ToString();
    }


    public void UseInvulnerability()
    {
        if (playerStats.Invulnerability > 0)
        {
            invulCounterText.fontSize = 36;
            invulCounterText.gameObject.SetActive(true);

            playerStats.isInvulnerable = true;
            player.transform.Find("ForceField").gameObject.SetActive(true);
            LeanTween.scale(player.transform.Find("ForceField").gameObject, Vector3.one * 9.14f, .3f).setEase(LeanTweenType.easeOutBack);
            playerStats.invulnerabilityTime += playerStats.invulnerabilityBaseTime;

            if (!isCountingInvul)
                StartCoroutine(InvulnerabilityCountdown());

            playerStats.Invulnerability -= 1;
        }
    }

    public void UseSpeed()
    {
        if (playerStats.speedMultiplier > 0)
        {
            speedCounterText.fontSize = 36;
            speedCounterText.gameObject.SetActive(true);

            playerStats.isMulSpeed = true;

            
            playerStats.speedTime += playerStats.speedBaseTime;

            if (!isCountingSpeed)
            {
                speedParticle.SetActive(true);
                playerStats.speedMultiplierAmount *= 1.5f;
                playerStats.turningSpeed *= 1.5f;
                StartCoroutine(SpeedCountdown());
            }
                

            playerStats.speedMultiplier -= 1;
        }

    }



    IEnumerator InvulnerabilityCountdown()
    {
        isCountingInvul = true;

        while (playerStats.invulnerabilityTime > 0.1f)
        {

            while (playerStats.invulnerabilityTime > 3)
            {
                invulCounterText.text = playerStats.invulnerabilityTime.ToString("0");

                yield return new WaitForSeconds(1f);

                playerStats.invulnerabilityTime -= 1;

                invulCounterText.gameObject.GetComponent<Animator>().ResetTrigger("canSize");

            }

            while (playerStats.invulnerabilityTime <= 3 && playerStats.invulnerabilityTime > 0)
            {
                invulCounterText.text = playerStats.invulnerabilityTime.ToString("F");
                yield return new WaitForSeconds(0.05f);

                playerStats.invulnerabilityTime -= 0.05f;

                invulCounterText.gameObject.GetComponent<Animator>().SetTrigger("canSize");

                if (playerStats.invulnerabilityTime > 3)
                    break;

            }
        }



        invulCounterText.gameObject.GetComponent<Animator>().ResetTrigger("canSize");
        invulCounterText.gameObject.SetActive(false);

        playerStats.isInvulnerable = false;
        playerStats.invulnerabilityTime = 0;
        LeanTween.scale(player.transform.Find("ForceField").gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(destroyField);



        isCountingInvul = false;
    }


    IEnumerator SpeedCountdown()
    {
        isCountingSpeed = true;

        while (playerStats.speedTime > 0.1f)
        {

            while (playerStats.speedTime > 3)
            {
                speedCounterText.text = playerStats.speedTime.ToString("0");

                yield return new WaitForSeconds(1f);

                playerStats.speedTime -= 1;

                speedCounterText.gameObject.GetComponent<Animator>().ResetTrigger("canSize");

            }

            while (playerStats.speedTime <= 3 && playerStats.speedTime > 0)
            {
                speedCounterText.text = playerStats.speedTime.ToString("F");
                yield return new WaitForSeconds(0.05f);

                playerStats.speedTime -= 0.05f;

                speedCounterText.gameObject.GetComponent<Animator>().SetTrigger("canSize");

                if (playerStats.speedTime > 3)
                    break;

            }
        }



        speedCounterText.gameObject.GetComponent<Animator>().ResetTrigger("canSize");
        speedCounterText.gameObject.SetActive(false);
        speedParticle.SetActive(false);

        playerStats.isMulSpeed = false;
        playerStats.speedTime = 0;
        playerStats.speedMultiplierAmount /= 1.5f;
        playerStats.turningSpeed /= 1.5f;



        isCountingSpeed = false;
    }



    void destroyField()
    {
        player.transform.Find("ForceField").gameObject.SetActive(false);
    }

}
