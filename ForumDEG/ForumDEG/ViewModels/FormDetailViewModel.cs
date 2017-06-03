using ForumDEG.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.ViewModels {
    public class FormDetailViewModel {
        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string Title { get; set; }

        public int QuestionsAmount {
            get { return (DiscursiveQuestions.Count + MultipleChoiceQuestions.Count); }
        }

        public List<Models.DiscursiveQuestion> DiscursiveQuestions { get; set; }
        public List<Models.MultipleAnswersQuestion> MultipleAnswersQuestions;
        public List<Models.SingleAnswerQuestion> SingleAnswerQuestions;

        public FormDetailViewModel(Models.Form form) {
            _form = form;

            Title = _form.Title;
            DiscursiveQuestions = _form.DiscursiveQuestions;
            MultipleAnswersQuestions = new List<Models.MultipleAnswersQuestion>();
            SingleAnswerQuestions = new List<Models.SingleAnswerQuestion>();

            SplitMultipleChoiceQuestions();
        }
        public void SplitMultipleChoiceQuestions() {
            var multipleChoiceQuestions = _form.MultipleChoiceQuestions;

            foreach (Models.MultipleChoiceQuestion multipleQuestion in multipleChoiceQuestions) {
                if (multipleQuestion.MultipleAnswers) {
                    var multipleAnswerQuestion = new Models.MultipleAnswersQuestion(multipleQuestion.Question);
                    foreach (Models.Option option in multipleQuestion) {
                        multipleAnswerQuestion.Add(option);
                    }
                    MultipleAnswersQuestions.Add(multipleAnswerQuestion);
                } else if (!multipleQuestion.MultipleAnswers) {
                    var singleAnswerQuestion = new Models.SingleAnswerQuestion {
                        Question = multipleQuestion.Question,
                        Options = new ObservableCollection<string>()
                    };
                    foreach (Models.Option option in multipleQuestion) {
                        singleAnswerQuestion.Options.Add(option.OptionText);
                    }
                    SingleAnswerQuestions.Add(singleAnswerQuestion);
                }
            }
        }
    }
}
