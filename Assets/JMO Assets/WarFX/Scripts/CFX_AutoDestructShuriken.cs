using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;
	
	void OnEnable()
	{
        float dist = Mathf.Log(Vector3.Distance(GameObject.Find("Player").transform.position, transform.position));
        if (dist <= 6f)
        {
            this.GetComponent<AudioSource>().volume =
                Mathf.Min(100, Mathf.Max(0, 6 - dist) * 40)
            / 100.0f;
            this.GetComponent<AudioSource>().Play();
        }
        StartCoroutine("CheckIfAlive");
	}
	
	IEnumerator CheckIfAlive ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			if(!GetComponent<ParticleSystem>().IsAlive(true))
			{
				if(OnlyDeactivate)
				{
					#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
					#else
						this.gameObject.SetActive(false);
					#endif
				}
				else
					GameObject.Destroy(this.gameObject);
				break;
			}
		}
	}
}
