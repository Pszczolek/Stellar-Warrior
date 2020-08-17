using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour {

    [SerializeField] ParticleSystem particleSystem;
    int debrisVariations;

	// Use this for initialization
	void Start () {
        var tex = particleSystem.textureSheetAnimation;
        debrisVariations = particleSystem.textureSheetAnimation.numTilesX * particleSystem.textureSheetAnimation.numTilesY;
        tex.useRandomRow = false;
        particleSystem.Stop();
        StartCoroutine(SpawnDebris());

    }

    private IEnumerator SpawnDebris()
    {
        var tex = particleSystem.textureSheetAnimation;
        for (int i = 0; i < debrisVariations; i++)
        {


            tex.startFrame = new ParticleSystem.MinMaxCurve(i);
            particleSystem.Emit(1);
            yield return new WaitForSeconds(.5f);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
