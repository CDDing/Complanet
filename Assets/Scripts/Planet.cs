using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int index;
    public bool CollisionCheck = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private async void OnCollisionEnter(Collision other)
    {
        Planet otherplanet = other.gameObject.GetComponent<Planet>();
        // Check if the other object is a planet and has the same index
        if (otherplanet != null && otherplanet.index == index)
        {
            // Ensure this block is executed only once for the colliding pair
            if (!otherplanet.CollisionCheck && !CollisionCheck)
            {
                CollisionCheck = true;
                otherplanet.CollisionCheck = true;

                Vector3 position = (this.gameObject.transform.position + other.gameObject.transform.position) / 2;
                int idx = gameObject.GetComponent<Planet>().index;

                // Return the colliding planets to the pool
                // ObjectPool.ReturnObject(other.gameObject, idx);
                // ObjectPool.ReturnObject(this.gameObject, idx);
                Destroy(this.gameObject);
                Destroy(other.gameObject);

                // Wait for the asynchronous task
                await AsyncFunc();

                // Create a new planet with the next index
                GameObject newPlanet = Shooter.GetObject(idx + 1);
                User.Renew_maxidx(idx+1);
                User.AddScore(idx + 1);
                newPlanet.transform.position = position;

                // Reset the CollisionCheck flag for both planets (optional if planets are being deactivated)
                CollisionCheck = false;
                otherplanet.CollisionCheck = false;
            }
        }
    }
    public void OnCollisionExit(Collision other){
        User.GameOver=true;
    }

    async Task AsyncFunc()
    {
        // Simulate asynchronous work (e.g., waiting for animations, physics updates, etc.)
        await Task.Delay(10); // Example delay, adjust as necessary
    }
}
