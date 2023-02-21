using Newtonsoft.Json;
using System.Dynamic;

namespace LommeregnerTest.DivideTest
{
  public class TestClass
  {
    
    public void Test()
    {
      List<ExpandoObject> expandoObjects = new List<ExpandoObject>();

      dynamic obj1 = new ExpandoObject();
      obj1.Name = "Name";
      obj1.Id = 1;

      dynamic obj2 = new ExpandoObject();
      obj1.Name = "Name";
      obj1.Id = 1;

      expandoObjects.Add(obj1);
      expandoObjects.Add(obj2);

      var test = expandoObjects.FirstOrDefault();
      var dict = new Dictionary<string, object>(test!);
      var matchingProperties = dict.Keys.ToList();

      var assembly = typeof(TestClass).Assembly;
      var types = assembly.GetTypes();
      
      var type = types.FirstOrDefault(type => type.GetProperties().Where(p => matchingProperties.Contains(p.Name)).Any());

      var listType = typeof(List<>);
      var constructedListType = listType.MakeGenericType(type!);

      var json = JsonConvert.SerializeObject(expandoObjects);
      var deserialize = JsonConvert.DeserializeObject(json, constructedListType!);

      Console.ReadLine();
    }
  }

  public class Obj
  {
    public int Id { get; set; }
    public string? Name { get; set; }
  }
}
