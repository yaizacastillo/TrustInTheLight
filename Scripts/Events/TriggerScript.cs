using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public enum Events {SoundEvent, EnemyEvent}

    public Events evento;
    public List<Transform> enemySpawnPoint;

    public bool isChangeMusicEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (evento)
            {
                case Events.SoundEvent:
                    //EventsOnTrigger.Instance.SoundEvent(EventsOnTrigger.SoundEventRequest.BirdEvent);
                    //EventsOnTrigger.Instance.SoundEvent(EventsOnTrigger.SoundEventRequest.BikeEvent);
                    break;
                case Events.EnemyEvent:
                    if (isChangeMusicEvent)
                    {

                    }

                    foreach (Transform t in enemySpawnPoint)
                    {
                        EventsOnTrigger.Instance.EnemyEvent(t.position);
                    }
                    break;
            }

            gameObject.SetActive(false);
        }
    }

}
