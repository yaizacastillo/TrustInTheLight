using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsOnTrigger : MonoSingleton<EventsOnTrigger>
{
    public enum SoundEventRequest { BirdEvent, BikeEvent }

    [Header("Eventos de sonido")]
    public SoundEvents birdEvent;
    public SoundEvents bikeEvent;

    public void SoundEvent(SoundEventRequest ser)
    {
        switch (ser)
        {
            case SoundEventRequest.BirdEvent:
                birdEvent.PlayEvent();
                break;
            case SoundEventRequest.BikeEvent:
                bikeEvent.PlayEvent();
                break;
        }
    }

    public void EnemyEvent(Vector3 enemySpawnPoint)
    {
        EnemyManager.Instance.GetEnemy(enemySpawnPoint);
    }
}
