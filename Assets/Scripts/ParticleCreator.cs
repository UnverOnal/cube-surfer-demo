using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCreator : MonoBehaviour
{
    ///<summary>
    ///Creates particles using object pooling method.
    ///</summary>
    public void CreateParticle(Vector3 position, Pool particlePool)
    {  
        GameObject particle = particlePool.GetPooledObject(); 
        particle.SetActive(true);
        
        particle.transform.position = position;
        particle.GetComponent<ParticleSystem>().Play();

        float disableTime = particle.GetComponent<ParticleSystem>().main.duration;
        StartCoroutine(DisableParticle(particle, disableTime));
    }

    ///<summary>
    ///Creates particles using Instantiation method.
    ///</summary>
    public void CreateParticle(Vector3 position, string name)
    {
        GameObject particle = Instantiate(Resources.Load<GameObject>("particles/" + name));

        particle.transform.position = position;
        particle.GetComponent<ParticleSystem>().Play();
    }

    private IEnumerator DisableParticle(GameObject particle, float time)
    {
        yield return new WaitForSeconds(time);

        particle.SetActive(false);
    }
}
