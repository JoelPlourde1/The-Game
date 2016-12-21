using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    //A quel distance le rayon peut se rendre
    public float Ray_Distance;

    public Inventory inventory; // Assign in inspector

    //Variable ou est stocker les caractéristique de l'objet pointé
    RaycastHit item_currently_pointed;

	void Update ()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * Ray_Distance, Color.blue );

        if(Physics.Raycast(this.transform.position, this.transform.forward, out item_currently_pointed, Ray_Distance))
        {
            Debug.Log(item_currently_pointed.collider.gameObject.tag);

            if (item_currently_pointed.collider.gameObject.tag.CompareTo("Plant") == 1)
            {
                Harvest(item_currently_pointed.collider.gameObject);
            }

            else if (item_currently_pointed.collider.gameObject.tag.CompareTo("Rock") == 1)
            {
                Mine(item_currently_pointed.collider.gameObject);
            }
            else
            {

            }
        }
    }


    void Harvest(GameObject plant)
    {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (plant.tag.CompareTo("Corn") == 1)
                {
                    //Ajout 1 corn dans l'inventaire
                    inventory.AddItem(4,1);

                    //Ajout 2 seeds de corns dans l'inventaire.
                    inventory.AddItem(3,2);

                    //Enlève le corn qui est dans le jeu
                    Destroy(plant);
                }
            }
    }

    void Mine(GameObject Rock)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Rock.tag.CompareTo("Iron") == 1)
            {
                //Ajout 1 Iron Ore dans l'inventaire
                inventory.AddItem(5, 1);

                //Enlève le corn qui est dans le jeu
                Destroy(Rock);
            }

            if (Rock.tag.CompareTo("Esperite") == 1)
            {
                //Ajout 1 Esperite Gem dans l'inventaire
                inventory.AddItem(6, 1);

                //Enlève le corn qui est dans le jeu
                Destroy(Rock);
            }
        }
    }
}
