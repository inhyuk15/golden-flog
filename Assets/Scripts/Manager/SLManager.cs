using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


class People {
    string name;
    int age;

    public People(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}



public class SLManager : MonoBehaviour
{
    People peop = new People("?:dd", 121);

    private void Start()
    {
        string jString = JsonConvert.SerializeObject(peop);
    }

    

}
