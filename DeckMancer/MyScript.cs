using DeckMancer.Core;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

class MyScript : Behaviour
{
    Scene scene;
    public JsonSerializer js;
    public override void Start()
    {
        List<int> numbers = new List<int> { 5, 7, 1, 9, 2, 4 };
        var foundNumber = numbers.FirstOrDefault(x => x == 1);

        js = new JsonSerializer();
        scene = new Scene("sdas");
        base.Start();
    }
    public override void Update()
    {
        SceneManager.LoadScene(scene);

        base.Update();
    }
}

