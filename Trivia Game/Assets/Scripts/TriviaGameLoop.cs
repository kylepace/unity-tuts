using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaGameLoop : MonoBehaviour {

	public struct Question
	{
		public string questionText;
		public string[] answers;
		public int correctAnswerIndex;

		public Question(string questionText, string[] answers, int correctAnswerIndex)
		{
			this.questionText = questionText;
			this.answers = answers;
			this.correctAnswerIndex = correctAnswerIndex;
		}
	}

	public Question currentQuestion = new Question("What is your favorite Color?", new string[] { "Blue", "Red", "Yellow", "White", "Black" }, 0);
	public Button[] answerButtons;
	public Text questionText;

	private Question[] questions = new Question[3];
	private int currentQuestionIndex;
	private int[] questionNumbersChosen = new int[] { 0, 1, 2 };
	private int questionsFinished = 0;

	public GameObject[] TriviaPanels;
	public GameObject finalResultsPanel;
	public Text resultsText;
	public GameObject feedbackText;
	private int numOfCorrectAnswers = 0;
	private bool allowSelection = true;

	// Use this for initialization
	void Start () {
		questions[0] = new Question("What is your favorite Color?", new string[] { "Blue", "Red", "Yellow", "White", "Black" }, 0);;
		questions[1] = new Question("What is your favorite Band?", new string[] { "Tool", "Styx", "Iron Maiden", "Journey", "Mars Volta" }, 4);;
		questions[2] = new Question("What is your favorite City?", new string[] { "Los Angeles", "Buffalo", "NYC", "St Louis", "Wichita" }, 2);;

		chooseQuestion ();
		assignQuestion (0);
	}

	// Update is called once per frame
	void Update () {
		quitGame ();
	}

	void assignQuestion (int questionNumber) {
		currentQuestion = questions [questionNumber];
		questionText.text = currentQuestion.questionText;
		for (var i = 0; i < answerButtons.Length; i++) {
			answerButtons [i].GetComponentInChildren<Text> ().text = currentQuestion.answers [i];
		}
	}

	public void checkAnswer (int buttonNumber) {
		if (allowSelection) {
			if (buttonNumber == currentQuestion.correctAnswerIndex) {
				print ("Correct");
				numOfCorrectAnswers++;
				feedbackText.GetComponent<Text> ().text = "Correct";
				feedbackText.GetComponent<Text> ().color = Color.green;
			} else {
				print ("incorrect");
				feedbackText.GetComponent<Text> ().text = "Wrong";
				feedbackText.GetComponent<Text> ().color = Color.red;
			}

			StartCoroutine ("continueAfterFeedback");
		}
	}

	void chooseQuestion(){
		currentQuestionIndex = Random.Range (0, questions.Length);
	}

	void displayResults() {
		switch (numOfCorrectAnswers) {
		case 0: 
			resultsText.text = "No questions correct";
			break;
		case 1: 
			resultsText.text = "One Correct Question";
			break;
		case 2: 
			resultsText.text = "Two correct questions";
			break;
		case 3: 
			resultsText.text = "Everything correct!";
			break;
		default:
			resultsText.text = "Nothing to do here";
			break;
		}
	}

	IEnumerator continueAfterFeedback() {
		allowSelection = false;
		feedbackText.SetActive (true);
		yield return new WaitForSeconds (1f);

		if (questionsFinished < questionNumbersChosen.Length - 1) {
			moveToNextQuestion ();
			questionsFinished++;
		} else {
			foreach (var p in TriviaPanels) {
				p.SetActive (false);
			}
			finalResultsPanel.SetActive (true);
			displayResults ();
		}
		allowSelection = true;
		feedbackText.SetActive (false);
	}

	void quitGame() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void restartLevel(){
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void moveToNextQuestion() {
		assignQuestion(questionNumbersChosen[questionNumbersChosen.Length - 1 - questionsFinished]);
	}
}
