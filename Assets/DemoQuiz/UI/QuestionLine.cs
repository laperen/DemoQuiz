using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemoQuiz {
	public class QuestionLine : MonoBehaviour {
		public GameObject optionprefab;
		public RectTransform optionscontent;
		private Action<string> questiontextchange;
		private Action addoption;
		private Action deleteq;

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
		public void RenderQuestion(int qindex, Quiz tempquiz) {
			Question q = tempquiz.questions[qindex];
			string qtext = tempquiz.text.GetValueByKey(q.questiontextkey);
			QuestionNameEvt.Invoke(qtext);
			questiontextchange = (string value) => {
				tempquiz.text.SetValue(q.questiontextkey, value);
			};
			addoption = () => {
				tempquiz.AddQuestionOption(qindex, "New Option");
				RenderOptions(qindex, tempquiz);
			};
			deleteq = () => {
				tempquiz.RemoveQuestion(qindex);
				QuizCreateHandler.instance.RenderQuestions();
			};
			RenderOptions(qindex, tempquiz);
		}
		public void RenderOptions(int qindex, Quiz tempquiz) {
			Question q = tempquiz.questions[qindex];
			for (int o = 0, omax = q.optionkeys.Count; o < omax; o++) {
				string otext = tempquiz.text.GetValueByKey(q.optionkeys[o]);
				Transform child = null;
				if (optionscontent.childCount > o) {
					child = optionscontent.GetChild(o);
					child.gameObject.SetActive(true);
				}
				if (!child) {
					child = Instantiate(optionprefab, optionscontent).transform;
				}
				child.GetComponent<QuestionOptionLine>().RendederQuestionOption(o, qindex, tempquiz, () => {
					tempquiz.RemoveQuestionOption(qindex, o);
					RenderOptions(qindex, tempquiz);
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