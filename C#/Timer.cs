// Ioannis Kalaitzidis, 2120067, Thesis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;                        // Χρόνος για την απάντηση στην ερώτηση
    [SerializeField] float timeToShowcorrectAnswer = 0.3f;                      // Χρόνος για την εμφάνιση της σωστής απάντησης/επιβράβευσης

    public bool loadNextQuestion;                                               // Έλεγχος για τη φόρτωση της επόμενης ερώτησης
    public float fillFraction;                                                  // Κλάσμα που χρησιμοποιείται για την ενδεικτική γραμμή χρονοδιαγράμματος
    public bool isAnsweringQuestion;                                            // Κατάσταση απάντησης ερώτησης
    float timerValue;                                                           // Τιμή του τρέχοντος χρονομέτρου

    void Update()
    {
        UpdateTimer();                                                          // Ενημέρωση του χρονομέτρου σε κάθε καρέ
    }

    public void CancelTimer()
    {
        timerValue = 0;                                                         // Μηδενίζει το χρονόμετρο
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;                                           // Μειώνει το χρονόμετρο βάσει του χρόνου που πέρασε

        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;             // Υπολογίζει το κλάσμα για την εμφάνιση
            }
            else
            {
                isAnsweringQuestion = false;                                    // Τερματίζει την περίοδο απάντησης
                timerValue = timeToShowcorrectAnswer;                           // Ορίζει τον χρόνο για την εμφάνιση της σωστής απάντησης
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowcorrectAnswer;            // Υπολογίζει το κλάσμα για την εμφάνιση της σωστής απάντησης
            }
            else
            {
                isAnsweringQuestion = true;                                     // Επανέρχεται στην περίοδο απάντησης
                timerValue = timeToCompleteQuestion;                            // Ορίζει τον χρόνο για την επόμενη ερώτηση
                loadNextQuestion = true;                                        // Ενεργοποιεί τη φόρτωση της επόμενης ερώτησης
            }
        }
    }
}
