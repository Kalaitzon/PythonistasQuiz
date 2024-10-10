// Ioannis Kalaitzidis, 2120067, Thesis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;                                   // Μετρητής για τις σωστές απαντήσεις
    int questionsSeen = 0;                                    // Μετρητής για το σύνολο των ερωτήσεων που έχουν εμφανιστεί

    public int GetCorrectAnswer()
    {
        return correctAnswers;                                // Επιστρέφει τον αριθμό των σωστών απαντήσεων
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;                                     // Αυξάνει τον μετρητή των σωστών απαντήσεων κατά ένα
    }

    public int GetQuestionSeen()
    {
        return questionsSeen;                                 // Επιστρέφει τον αριθμό των ερωτήσεων που έχουν εμφανιστεί

    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;                                      // Αυξάνει τον μετρητή των ερωτήσεων που έχουν εμφανιστεί κατά ένα
    }

    // Υπολογίζει και επιστρέφει το ποσοστό επιτυχίας βάσει των σωστών απαντήσεων και του συνόλου των ερωτήσεων
    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
