using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DemoQuiz {
	public class Previewer : MonoBehaviour {
		public RectTransform optioncontent;
		public GameObject optionprefab;
		public GameObject nextbtn, previousbtn;

		public StringEvt QuestionTextEvt;
		public StringEvt ResultTextEvt;
		private int currquestion;
		private int[] answers;

		public void BackToPrevMenu() {
			SceneManager.LoadScene(DataHolder.currbackmenu);
		}
		private void Awake() {
			answers = new int[DataHolder.tempquiz.questions.Count];
			for (int i = 0, max = DataHolder.tempquiz.questions.Count; i < max; i++) {
				answers[i] = -1;
			}
			currquestion = 0;
			ShowQuestion(0);
		}
		private void ShowQuestion(int qindex) {
			Question q = DataHolder.tempquiz.questions[qindex];
			QuestionTextEvt.Invoke(GetText(q.questiontextkey));
			for (int i = 0, max = q.optionkeys.Count; i < max; i++) {
				string optiontext = GetText(q.optionkeys[i]);
				Transform child = null;
				if (optioncontent.childCount > i) {
					child = optioncontent.GetChild(i);
					child.gameObject.SetActive(true);
				}
				if (!child) {
					child = Instantiate(optionprefab, optioncontent).transform;
				}
				child.GetComponent<PreviewOption>().Init(i, optiontext, SelectAnswer);
			}
			SelectAnswer(answers[qindex]);
			if (optioncontent.childCount > q.optionkeys.Count) {
				for (int i = q.optionkeys.Count, max = optioncontent.childCount; i < max; i++) {
					optioncontent.GetChild(i).gameObject.SetActive(false);
				}
			}
			previousbtn.SetActive(qindex > 0);
			nextbtn.SetActive(qindex < (DataHolder.tempquiz.questions.Count - 1));
		}
		private void SelectAnswer(int index) {
			PreviewOption[] options = optioncontent.GetComponentsInChildren<PreviewOption>();
			for (int i = 0, max = options.Length ; i < max; i++) {
				options[i].SetAsAnswer(index == i);
			}
			answers[currquestion] = index;
		}
		public void ShowNextQuestion(int delta) {
			int c = currquestion + delta;
			if (c < 0 || c >= DataHolder.tempquiz.questions.Count) { return; }
			currquestion += delta;
			ShowQuestion(currquestion);
		}
		private string GetText(int key) {
			string text = DataHolder.tempquiz.text.GetValueByKey(key);
			//TODO localization handling;
			return text;
		}
		public void EvaluateResult() {
			int correct = 0;
			for (int i = 0, max = answers.Length; i < max; i++) {
				Question q = DataHolder.tempquiz.questions[i];
				if (q.answers.Contains(answers[i])) {
					correct++;
				}
			}
			ResultTextEvt.Invoke($"{correct}/{answers.Length}");
		}
	}
}