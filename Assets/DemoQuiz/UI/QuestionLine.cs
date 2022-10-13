using System;
using UnityEngine;

namespace DemoQuiz {
	public class QuestionLine : MonoBehaviour {
		public GameObject optionprefab;
		public RectTransform optionscontent;
		private Action<string> questiontextchange;
		private Action addoption;
		private Action deleteq;

		public StringEvt QuestionIndexEvt;
		public StringEvt QuestionNameEvt;

		public void ChangeQuestionText(string value) {
			if (null == questiontextchange) { return; }
			questiontextchange.Invoke(value);
		}
		public void AddQuestionOption() {
			if (null == addoption) { return; }
			addoption.Invoke();
		}
		public void DeleteQuestion() {
			if (null == deleteq) { return; }
			deleteq.Invoke();
		}
		public void RenderQuestion(int qindex) {
			Question q = DataHolder.tempquiz.questions[qindex];
			string qtext = DataHolder.tempquiz.text.GetValueByKey(q.questiontextkey);
			QuestionNameEvt.Invoke(qtext);
			questiontextchange = (string value) => {
				DataHolder.tempquiz.text.SetValue(q.questiontextkey, value);
			};
			addoption = () => {
				DataHolder.tempquiz.AddQuestionOption(qindex, "New Option");
				RenderOptions(qindex);
			};
			deleteq = () => {
				DataHolder.tempquiz.RemoveQuestion(qindex);
				QuizCreateHandler.instance.RenderQuestions();
			};
			RenderOptions(qindex);
			QuestionIndexEvt.Invoke($"{qindex + 1}");
		}
		public void RenderOptions(int qindex) {
			Question q = DataHolder.tempquiz.questions[qindex];
			for (int o = 0, omax = q.optionkeys.Count; o < omax; o++) {
				string otext = DataHolder.tempquiz.text.GetValueByKey(q.optionkeys[o]);
				Transform child = null;
				if (optionscontent.childCount > o) {
					child = optionscontent.GetChild(o);
					child.gameObject.SetActive(true);
				}
				if (!child) {
					child = Instantiate(optionprefab, optionscontent).transform;
				}
				int temp = o;
				child.GetComponent<QuestionOptionLine>().RendederQuestionOption(temp, qindex, () => {
					DataHolder.tempquiz.RemoveQuestionOption(qindex, temp);
					RenderOptions(qindex);
				});
			}
			if (optionscontent.childCount >= q.optionkeys.Count) {
				for (int i = q.optionkeys.Count, max = optionscontent.childCount; i < max; i++) {
					optionscontent.GetChild(i).gameObject.SetActive(false);
				}
			}
			QuizCreateHandler.instance.SettleLayout();
		}
	}
}