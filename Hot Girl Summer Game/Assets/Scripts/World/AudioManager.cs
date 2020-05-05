using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] partyTracks;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.bubblyPoints && GameController.partyDeck.victoryPoints.hypePoints > GameController.partyDeck.victoryPoints.calmPoints)
        {
            source.Pause();
            source.clip = partyTracks[0];
            source.Play();
        }
        else if (GameController.partyDeck.victoryPoints.calmPoints > GameController.partyDeck.victoryPoints.bubblyPoints)
        {
            source.Pause();
            source.clip = partyTracks[1];
            source.Play();
        }
    }
}
