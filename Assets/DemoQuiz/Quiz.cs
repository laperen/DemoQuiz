using System;
using System.Collections.Generic;

namespace DemoQuiz {
	[Serializable]
	public class Question {
		public int questiontextkey;
		public List<int> optionkeys;
		public List<int> answers;

		public Question(int textkey) {
			questiontextkey = textkey;
			optionkeys = new List<int>();
			answers = new List<int>();
		}
	}
	[Serializable]
	public class Quiz{
		public SavableDictionary<int, string> text;
		public int quiznamekey;
		public List<Question> questions;
		//public List<int> scorelevels;

		public int AddText(string txt) {
			int newkey = text.runningIndex;
			text.Add(newkey, txt);
			return newkey;
		}
		public Quiz(string quizname) {
			text = new SavableDictionary<int, string>();
			questions = new List<Question>();
			//scorelevels = new List<int>();
			quiznamekey = AddText(quizname);
		}
		public void NewQuestion(string questiontext) {
			questions.Add(new Question(AddText(questiontext)));
		}
		public void RemoveQuestion(int qindex) {
			Question q = questions[qindex];
			text.Remove(q.questiontextkey);
			for (int i = 0, max = q.optionkeys.Count; i < max; i++) {
				text.Remove(q.optionkeys[i]);
			}
			questions.RemoveAt(qindex);
		}
		public void AddQuestionOption(int qindex, string optiontext) {
			questions[qindex].optionkeys.Add(AddText(optiontext));
		}
		public void RemoveQuestionOption(int qindex, int oindex) {
			text.Remove(questions[qindex].optionkeys[oindex]);
			questions[qindex].optionkeys.RemoveAt(oindex);
		}
	}
}