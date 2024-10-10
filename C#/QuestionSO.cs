// Ioannis Kalaitzidis, 2120067, Thesis

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Δημιουργία ενός νέου menu στον Editor για την προσθήκη Quiz Questions
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string Question = "Enter new question text here";      // Πεδίο για την εισαγωγή του κειμένου της ερώτησης

    [SerializeField] string[] Answers = new string[4];                      // Πίνακας με τις επιλογές απαντήσεων (4)
    [SerializeField] int CorrectAnswerIndex;                                // Ο δείκτης της σωστής απάντησης στον πίνακα με τις 4 πιθανές απαντήσεις

    public string GetQuestion()
    {
        return Question;                                                    // Επιστρέφει το κείμενο της ερώτησης στη σκηνή
    }

    public string GetAnswer(int index)
    {
        return Answers[index];                                              // Επιστρέφει την απάντηση στον δείκτη (από τον χρήστη)
    }

    public int GetCorrectAnswerIndex()
    {
        return CorrectAnswerIndex;                                          // Επιστρέφει τον δείκτη της σωστής απάντησης σε περίπτωση που ο χρήστης απαντήσει λάθος
    }

    // internal string GetQuestionText()
    // {
    //     throw new NotImplementedException();
    // }
}
