using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        //Lit toute les objects situé dans le fichier json.
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));

        //Construit la database à partir du fichier .json
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for(int i = 0; i <database.Count; i++)
        {
            if(database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }


    //Construit la Item Database à partir des informations du fichier json.
    void ConstructItemDatabase()
    {
        //Pour tout les objects contenu dans la database:
        for (int i = 0; i < itemData.Count; i++)
        {
            //D'après les informations du premier objet dans le .json file, on crée un objet et on le stocke dans
            //la DataBase.
            database.Add(new Item((int) itemData[i]["id"],
                itemData[i]["title"].ToString(),
                (int) itemData[i]["value"],
                (int) itemData[i]["stats"]["damage"],
                (int) itemData[i]["stats"]["defence"],
                (int) itemData[i]["stats"]["health"],
                      itemData[i]["description"].ToString(),
                (bool)itemData[i]["stackable"],
                      itemData[i]["slug"].ToString()));
        }
    }
}

//On définit une structure de donnée Item
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Damage { get; set; }
    public int Defence { get; set; }
    public int Health { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int ID, string Title, int value, int damage, int defence, int health, string description, bool stackable, string slug)
    {
        this.ID = ID;
        this.Title = Title;
        this.Value = value;
        this.Damage = damage;
        this.Defence = defence;
        this.Health = health;
        this.Description = description;
        this.Stackable = stackable;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public Item(int ID, string Title, int value, bool stackable,string description, string slug)
    {
        this.ID = ID;
        this.Title = Title;
        this.Value = value;
        this.Description = description;
        this.Stackable = stackable;
        this.Slug = slug;
    }

    public Item()
    {
        this.ID = -1;
    }
}
