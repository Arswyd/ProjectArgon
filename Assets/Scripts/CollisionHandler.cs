using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    [SerializeField] ParticleSystem crashVFX;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        // TODO - Fix collision disabling
        GetComponent<Collider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene()
    {
        // TODO - Fix reloadning level
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
