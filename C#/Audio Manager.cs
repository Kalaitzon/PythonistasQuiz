// // Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;              // Η πηγή ήχου για την background μουσική
    [SerializeField] private List<AudioSource> buttonSoundSources; // Λίστα με τις πηγές ήχου για τα εφέ των κουμπιών

    void Start()
    {
        RestartMusic();                                            // Καλεί τη συνάρτηση για επανεκκίνηση της μουσικής όταν ξεκινάει η σκηνή
    }

    public void ToggleMute()
    {
        bool isMuted = musicSource.mute;                           // Καταγράφει αν η μουσική είναι ήδη σε κατάσταση mute
        musicSource.mute = !isMuted;                               // Εναλλάσσει την κατάσταση mute της μουσικής

        // Εναλλάσσει την κατάσταση mute για κάθε ήχο των κουμπιών
        foreach (AudioSource source in buttonSoundSources)
        {
            if (source != null)
            {
                source.mute = !isMuted;                            // Ρυθμίζει την κατάσταση mute σύμφωνα με την κατάσταση της μουσικής
            }
        }
    }

    public void RestartMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();                                    // Διακόπτει την τρέχουσα αναπαραγωγή
            musicSource.Play();                                    // Ξεκινά την αναπαραγωγή της μουσικής από την αρχή
        }
    }
}
