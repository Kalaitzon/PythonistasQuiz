// Ioannis Kalaitzidis, 2120067, Thesis

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Προσθήκη για χρήση Coroutines


public class MainMenu : MonoBehaviour
{
    // Συστατικό AudioSource για το ηχητικό εφέ του κουμπιού
    [SerializeField] private AudioSource ButtonSound;
    
    // Φόρτωση της επόμενης σκηνής 
    public void PlayGame()
    {
        // Αν όλα τα δεδομένα είναι έγκυρα, συνεχίζουμε με τη φόρτωση της επόμενης σκηνής
        StartCoroutine(PlaySoundAndLoadScene(1, ButtonSound));
    }
    
    // Για να προλάβει να ολοκληρωθεί ο ήχος του εφέ και μετά να περάσει στην επόμενη σκηνή
    IEnumerator PlaySoundAndLoadScene(int sceneIndex, AudioSource audioSource)
    {
        audioSource.Play(); 
        yield return new WaitForSeconds(Mathf.Max(0, audioSource.clip.length - 2.3f));  // Μικρή μείωση για αποφυγή καθυστέρησης
        SceneManager.LoadSceneAsync(sceneIndex); 
    }
}
