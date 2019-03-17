using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class FlashingText : MonoBehaviour
{
    private Text spacebarText;
    private RawImage titleImage;
    private RawImage beforeImage;
    private Text emailText;
    private InputField emailInput;
    private Text giveAnswer;
    private Text questionText;
    private InputField answerInput;
    private Button continueButton;
    private Text wrongAnswer;
    private Text wrongEmail;
    private bool spaceInput;
    public float timer;
    public float timer2;
    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    // Start is called before the first frame update
    void Start()
    {
        spaceInput = false;
        spacebarText = GameObject.FindWithTag("spacebar").GetComponent<Text>() as Text;
        titleImage = GameObject.FindWithTag("title").GetComponent<RawImage>() as RawImage;
        beforeImage = GameObject.FindWithTag("before").GetComponent<RawImage>() as RawImage;
        emailText = GameObject.FindWithTag("enteremailtext").GetComponent<Text>() as Text;
        emailInput = GameObject.FindWithTag("emailinput").GetComponent<InputField>() as InputField;
        giveAnswer = GameObject.FindWithTag("giveanswer").GetComponent<Text>() as Text;
        questionText = GameObject.FindWithTag("questiontext").GetComponent<Text>() as Text;
        answerInput = GameObject.FindWithTag("answerinput").GetComponent<InputField>() as InputField;
        continueButton = GameObject.FindWithTag("continue").GetComponent<Button>() as Button;
        wrongAnswer = GameObject.FindWithTag("wronganswer").GetComponent<Text>() as Text;
        wrongEmail = GameObject.FindWithTag("wrongemail").GetComponent<Text>() as Text;
        beforeImage.enabled = false;
        emailText.enabled = false;
        emailInput.gameObject.SetActive(false);
        giveAnswer.enabled = false;
        questionText.enabled = false;
        answerInput.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        wrongAnswer.enabled = false;
        wrongEmail.enabled = false;
        continueButton.onClick.AddListener(CheckAnswer);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5)
        {
            spacebarText.enabled = true;
        }

        if (timer >= 1)
        {
            spacebarText.enabled = false;
            timer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceInput = true;
            spacebarText.text = "Welcome! Please wait ...";
        }

        if (spaceInput == true)
        {
            timer2 += Time.deltaTime;
        }

        if (timer2 >= 3)
        {
            spacebarText.enabled = false;
            titleImage.enabled = false;
            beforeImage.enabled = true;
            emailText.enabled = true;
            emailInput.gameObject.SetActive(true);
            giveAnswer.enabled = true;
            questionText.enabled = true;
            answerInput.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
        }
    }

    void CheckAnswer()
    {
        if (answerInput.text != "medicine" && answerInput.text != "")
        {
            if (validateEmail(emailInput.text) == false)
            {
                wrongAnswer.enabled = false;
                wrongEmail.enabled = true;
            }
            else
            {
                wrongEmail.enabled = false;
                wrongAnswer.enabled = true;
            }
        }
        else
        {
            if (validateEmail(emailInput.text) == false)
            {
                wrongAnswer.enabled = false;
                wrongEmail.enabled = true;
            }
            else
            {
                wrongEmail.enabled = false;
                wrongAnswer.enabled = false;
                ScreenSwitcher();
            }
        }
    }

    public static bool validateEmail(string email)
    {
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }

    public void ScreenSwitcher()
    {
        SceneManager.LoadScene(1);
    }
}
