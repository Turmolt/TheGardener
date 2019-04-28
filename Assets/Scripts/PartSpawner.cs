using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BackwardsCap
{
    public class PartSpawner : MonoBehaviour
    {
        
        [Inject] private DiContainer container;
        
        [Serializable]
        public class PartPrefab
        {
            public Part Part;
            public GameObject Prefab;
        }

        [SerializeField] private PartPrefab[] Parts;
        
        public Dictionary<Part, GameObject> PartPrefabs;

        void Awake()
        {
            PartPrefabs = new Dictionary<Part, GameObject>();
            for (int i = 0; i < Parts.Length; i++)
            {
                PartPrefabs.Add(Parts[i].Part,Parts[i].Prefab);
            }
        }
        

        public void SpawnParts(Part part, int count, Vector2 pos, bool addForce)
        {
            for (int i = 0; i < count; i++)
            {
                var g = Instantiate(PartPrefabs[part], pos, Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)));
                var limb = g.GetComponent<BodyPart>(); 
                container.Inject(limb);
                if(addForce) StartCoroutine(SpawnForce(g));
            }
        }


        IEnumerator SpawnForce(GameObject g)
        {
            Vector2 r = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float d = 0.1f;
            float t = 0f;
            while (t < 1f)
            {
                t = t.AddClamped(Time.deltaTime / d, 0f, 1f);

                g.transform.position += r.xyz() * (1f - t);
                yield return 0;
            }

            yield return 0f;
        }


    }
}